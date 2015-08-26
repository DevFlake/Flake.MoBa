using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using i18n = Flake.MoBa.XpressNetLi.Resources;
using logme = Flake.MoBa.Log.FlakeLog;
using Flake.MoBa.XpressNetLi.Comunication;
using Flake.MoBa.ComPort;
using Flake.MoBa.XpressNetLi.Comunication.Answers;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using Flake.MoBa.XpressNetLi.Comunication.Commands;

namespace Flake.MoBa.XpressNetLi
{
    /// <summary>
    /// Central Class representing a digital central and an interface
    /// </summary>
    public class Central : IDisposable
    {
        #region class data warehouse

        /// <summary>
        /// All registered entities like locomotives or others with addresses
        /// </summary>
        List<ILIEntity> _RigisteredEntities;

        /// <summary>
        /// Registers an entity to the central
        /// </summary>
        /// <param name="item">entity item</param>
        /// <example>locomotive</example>
        public void RegisterEntity(ILIEntity item)
        {
            _RigisteredEntities.Add(item);
            item.RegisterCentral(this);
        }

        /// <summary>
        /// Configuration for central
        /// </summary>
        public FlakeLIConfiguration Config { get; private set; }

        #endregion class data warehouse

        #region Serial port connection

        /// <summary>
        /// The connection to the interface via serial port
        /// </summary>
        public IComPort SerialPortConnection { get; private set; }

        /// <summary>
        /// Initializes a new connection to the interface
        /// </summary>
        /// <remarks>Uses the credentials Port, StopBites, aso.</remarks>
        private void InitConnection(string port = "COM2", int baudRate = 57600, ComPortParity parityBits = ComPortParity.None, int dataBits = 8, ComPortStopBits stopBits = ComPortStopBits.One)
        {
            SerialPortConnection = new ComPortConnection(port, baudRate, parityBits, dataBits, stopBits);
            try
            {
                SerialPortConnection.Open();
            }
            catch (System.UnauthorizedAccessException notavail)
            {
                // COM Port in use
                logme.Log(i18n.FlakeLIErrors.ComPortNotAvailable, logme.LogLevel.error);
                notavail.ToString(); // foo
                return;
            }
            catch (IOException ioex)
            {
                logme.Log(ioex);
                return;
            }
            catch (Exception ex)
            {
                logme.Log(ex);
                return;
            }
        }

        /// <summary>
        /// Close serial connection
        /// </summary>
        private void CloseConnection()
        {
            SerialPortConnection.Close();
        }

        /// <summary>
        /// Serial port for connetion to interface
        /// </summary>
        public string Port
        {
            get
            {
                return SerialPortConnection.PortName;
            }
            private set
            {
                SerialPortConnection.PortName = value;
            }
        }

        /// <summary>
        /// Baudrate of serial port to interface
        /// </summary>
        public int BaudRate
        {
            get
            {
                return SerialPortConnection.BaudRate;
            }
            private set
            {
                SerialPortConnection.BaudRate = value;
            }
        }

        /// <summary>
        /// ParityBits of serial port to interface
        /// </summary>
        public ComPortParity ParityBits
        {
            get
            {
                return SerialPortConnection.Parity;
            }
            private set
            {
                SerialPortConnection.Parity = value;
            }
        }

        /// <summary>
        /// DataBits of serial port to interface
        /// </summary>
        public int DataBits
        {
            get
            {
                return SerialPortConnection.DataBits;
            }
            private set
            {
                SerialPortConnection.DataBits = value;
            }
        }

        /// <summary>
        /// StopBits of serial port to interface
        /// </summary>
        public ComPortStopBits StopBits { get; private set; }

        /// <summary>
        /// Handler for reading bytes from connection
        /// </summary>
        private void DataReceivedHandler()
        {
            int readlenght = SerialPortConnection.BytesToRead;
            byte[] read = new byte[readlenght];
            if (readlenght > 0)
            {
                SerialPortConnection.Read(ref read, 0, readlenght);
                LastAnswer = AnswerFactory.CreateNew(read);
                if (LastAnswer != null && (LastAnswer as AnswerBase).IsBroadCast) ProcessBroadCast();
            }
        }

        #endregion Serial port connection

        #region ExpressNet communication

        /// <summary>
        /// Last answer of the central
        /// </summary>
        private ILICommunication LastAnswer { get; set; }

        /// <summary>
        /// Last command to the central
        /// </summary>
        private ILICommunication LastCommand { get; set; }

        /// <summary>
        /// Indicates the errors received in a row
        /// </summary>
        private int _ErrorInARow;

        /// <summary>
        /// Enqueue a new command to the central command queue
        /// </summary>
        /// <param name="command">bytearray-format command</param>
        public void QueueNewCommand(LICommandAndAnswer command)
        {
            SendCommandAndReceiveAnswer(command);
        }

        /// <summary>
        /// Send command to central and receive answer
        /// </summary>
        /// <param name="command"></param>
        private void SendCommandAndReceiveAnswer(LICommandAndAnswer command)
        {
            bool retry = true;
            while (retry)
            {
                SerialPortConnection.Write(command.Command.ByteArray, 0, command.Command.Length);
                logme.Log(command.Command.LogMessage, logme.LogLevel.limsg, command.Command.ByteArray);
                logme.AddLine();

                Thread.Sleep(Config.Data.TimeToWaitForLIAnswer_ms); // wait for central
                DateTime tmp = DateTime.Now;

                while (LastAnswer == null || EmergencyOff)
                {
                    // wait for answer
                    if (DateTime.Now > tmp.AddSeconds(Config.Data.TimeoutForLIResponse_s))
                    {
                        // timeout
                        logme.Log(Resources.FlakeLIErrors.LITimeout, logme.LogLevel.error, command.Command.ByteArray);
                        return;
                    }
                }
                if (LastAnswer.GetType() == typeof(ErrorUnknown))
                {
                    if (_ErrorInARow < Config.Data.AllowedCentralErrorsInARow)
                    {
                        // unknown error, send again, if error sum < n
                        _ErrorInARow++;
                    }
                    else
                    {
                        // Too much errors in a row
                        retry = false;
                        logme.Log(string.Format(Resources.FlakeLIErrors.ErrorSendingCommad, Config.Data.AllowedCentralErrorsInARow.ToString()), logme.LogLevel.error, command.Command.ByteArray);
                        return;
                    }
                }
                else
                {
                    _ErrorInARow = 0;
                    retry = false;
                }
            }

            command.Answer = LastAnswer; // store answer to command for external use
            LastAnswer = null;

            logme.Log(command.Answer.LogMessage, logme.LogLevel.limsg, command.Answer.ByteArray);
            logme.AddLine('=');
        }

        /// <summary>
        /// gets triggered by LIListener when broadcast appears
        /// </summary>
        internal void ProcessBroadCast()
        {
            if (LastAnswer.GetType() == typeof(BCAllOff))
            {
                EmergencyOff = true;
                logme.Log(i18n.FlakeLIMsgs.EmergencyOffToggle, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCAllOn))
            {
                EmergencyOff = false;
                EmergencyStop = false;
                ProgramMode = false;
                logme.Log(i18n.FlakeLIMsgs.AllOnSet, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCAllLocosOff))
            {
                EmergencyStop = true;
                logme.Log(i18n.FlakeLIMsgs.EmergencyStopToggle, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCProgramMode))
            {
                ProgramMode = true;
                logme.Log(i18n.FlakeLIMsgs.ProgrammodeToggle, logme.LogLevel.info);
                return;
            }
        }

        /// <summary>
        /// Fetch infos from central an dinterface
        /// </summary>summary>
        private void GetInterfaceAndCentralInfo()
        {
            bool noError = false;
            int tries = 0;
            while (noError == false && tries <= Config.Data.CentralFetchInfoTries)
            {
                try
                {
                    tries++;
                    // get the interface info
                    LICommandAndAnswer liVersion = new LICommandAndAnswer(new GetLIUSBVersion());
                    LICommandAndAnswer liAddress = new LICommandAndAnswer(new GetLIUSBAddress());
                    QueueNewCommand(liVersion);
                    QueueNewCommand(liAddress);
                    LIVersionInfo livi = liVersion.Answer as LIVersionInfo;
                    LIAddressInfo liadd = liAddress.Answer as LIAddressInfo;
                    logme.Log(string.Format(i18n.FlakeLIMsgs.LIInfoandAddress, livi.LIVersion.ToString("0.0"), livi.LICodenumber.ToString(), liadd.LIAddress.ToString()), logme.LogLevel.limsg);

                    // get the central version info
                    LICommandAndAnswer centralVersion = new LICommandAndAnswer(new GetCentralVersion());
                    QueueNewCommand(centralVersion);
                    CentralVersionInfo cvi = centralVersion.Answer as CentralVersionInfo;
                    logme.Log(string.Format(i18n.FlakeLIMsgs.CentralVersionInfo, cvi.CentralVersion.ToString("0.0"), cvi.CentralTypeName), logme.LogLevel.limsg);

                    GetCentralState();
                    noError = true;
                }
                catch (Exception ex)
                {
                    logme.Log(ex);
                }
            }
            if (noError == false && tries > Config.Data.CentralFetchInfoTries)
            {
                logme.Log(i18n.FlakeLIWarnings.TooManyTriesFetchCentralInfo, logme.LogLevel.warning);
            }
        }

        /// <summary>
        /// Fetch central state
        /// </summary>
        private void GetCentralState()
        {
            // get the central state info
            LICommandAndAnswer centralState = new LICommandAndAnswer(new GetCentralStateInfo());
            QueueNewCommand(centralState);
            CentralStateInfo csi = centralState.Answer as CentralStateInfo;

            this.EmergencyOff = csi.EmergencyOff;
            this.EmergencyStop = csi.EmergencyStop;
            this.StartMode = csi.StartModeString;
            this.ProgramMode = csi.ProgramMode;
            this.ColdReset = csi.ColdReset;
            this.RamCheckError = csi.RamCheckError;

            string msg =
                string.Format(i18n.FlakeLIMsgs.CentralStateInfo, ((EmergencyStop) ? (i18n.FlakeLIBase.yes) : (i18n.FlakeLIBase.no)),
                ((EmergencyOff) ? (i18n.FlakeLIBase.yes) : (i18n.FlakeLIBase.no)), StartMode,
                ((ProgramMode) ? (i18n.FlakeLIBase.yes) : (i18n.FlakeLIBase.no)),
                ((ColdReset) ? (i18n.FlakeLIBase.yes) : (i18n.FlakeLIBase.no)),
                ((RamCheckError) ? (i18n.FlakeLIBase.yes) : (i18n.FlakeLIBase.no)));

            logme.Log(msg, logme.LogLevel.limsg);
        }

        #endregion ExpressNet communication

        #region central state

        /// <summary>
        /// Indicator for emergency stop
        /// </summary>
        public bool EmergencyStop { get; private set; }

        /// <summary>
        /// Indicator for emergency off
        /// </summary>
        public bool EmergencyOff { get; private set; }

        /// <summary>
        /// Start type of the central as Text
        /// </summary>
        public string StartMode { get; private set; }

        /// <summary>
        /// Indicator of programmode
        /// </summary>
        public bool ProgramMode { get; private set; }

        /// <summary>
        /// Indicator of a cold start or reset
        /// </summary>
        public bool ColdReset { get; private set; }

        /// <summary>
        /// Indicator of a ram error in central
        /// </summary>
        public bool RamCheckError { get; private set; }

        #endregion central state

        #region iDisposable implementation

        /// <summary>
        /// Dispose
        /// </summary>
        void IDisposable.Dispose()
        {
            CloseConnection();
        }

        #endregion iDisposable implementation

        /// <summary>
        /// Creates a new insance of a central class
        /// </summary>
        /// <param name="port">Serial port for connetion to interface (eg. 'COM2')</param>
        /// <param name="baudRate">Baudrate of serial port to interface</param>
        /// <param name="parityBits">ParityBits of serial port to interface</param>
        /// <param name="dataBits">DataBits of serial port to interface</param>
        /// <param name="stopBits">StopBits of serial port to interface</param>
        public Central(string port = "COM2", int baudRate = 57600, ComPortParity parityBits = ComPortParity.None, int dataBits = 8, ComPortStopBits stopBits = ComPortStopBits.One)
        {
            Config = new FlakeLIConfiguration();

            _RigisteredEntities = new List<ILIEntity>();
            _ErrorInARow = 0;

            // Open connection to interface
            InitConnection(port, baudRate, parityBits, dataBits, stopBits);

            // start command queue worker process
            //System.Threading.ThreadPool.QueueUserWorkItem(delegate { _AnswerListener = new LIListener(this); }, null);
            logme.Log(string.Format(i18n.FlakeLIMsgs.StartLIListener, Port), logme.LogLevel.info);
            SerialPortConnection.ComDataReceived += new ComPortEventHandler(DataReceivedHandler);

            GetInterfaceAndCentralInfo();
        }


        /// <summary>
        /// Creates a new XpressNet LI central
        /// </summary>
        /// <param name="serialPortConnection">an existing serialport connection</param>
        public Central(IComPort serialPortConnection)
        {
            Config = new FlakeLIConfiguration();

            _RigisteredEntities = new List<ILIEntity>();
            _ErrorInARow = 0;

            // Open connection to interface
            SerialPortConnection = serialPortConnection;
            SerialPortConnection.Open();

            // start command queue worker process
            //System.Threading.ThreadPool.QueueUserWorkItem(delegate { _AnswerListener = new LIListener(this); }, null);
            logme.Log(string.Format(i18n.FlakeLIMsgs.StartLIListener, Port), logme.LogLevel.info);
            SerialPortConnection.ComDataReceived += new ComPortEventHandler(DataReceivedHandler);

            GetInterfaceAndCentralInfo();
        }
    }
}