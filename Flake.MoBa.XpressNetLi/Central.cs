using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using i18n = Flake.MoBa.XpressNetLi.Resources;
using logme = Flake.MoBa.Log.FlakeLog;
using Flake.MoBa.XpressNetLi.Comunication;
using Flake.MoBa.ComPort;
using Flake.MoBa.XpressNetLi.Comunication.Answers;
using Flake.MoBa.XpressNetLi.Comunication.Commands;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XPressNetLi.Configuration;

namespace Flake.MoBa.XpressNetLi
{
    /// <summary>
    /// Central Class representing a digital central and an interface
    /// </summary>
    public class Central : IDisposable, ICentral
    {
        #region class data warehouse

        /// <summary>
        /// All registered entities like locomotives or others with addresses
        /// </summary>
        List<ILiEntity> _RigisteredEntities;

        /// <summary>
        /// Registers an entity to the central
        /// </summary>
        /// <param name="item">entity item</param>
        /// <example>locomotive</example>
        public void RegisterEntity(ILiEntity item)
        {
            _RigisteredEntities.Add(item);
            item.RegisterCentral(this);
        }

        /// <summary>
        /// Configuration for central
        /// </summary>
        public ConfigurationSet Config { get; private set; }

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
                Connected = true;
            }
            catch (System.UnauthorizedAccessException notavail)
            {
                // COM Port in use
                logme.Log(i18n.ErrorMessages.ComPortNotAvailable, logme.LogLevel.error);
                notavail.ToString(); // foo
                Connected = false;
                return;
            }
            catch (IOException ioex)
            {
                logme.Log(ioex);
                Connected = false;
                return;
            }
            catch (Exception ex)
            {
                logme.Log(ex);
                Connected = false;
                return;
            }
        }

        /// <summary>
        /// Initializes a new connection to the interface
        /// </summary>
        /// <remarks>uses the given com port connection</remarks>
        private void InitConnection(IComPort serialPortConnection)
        {
            SerialPortConnection = serialPortConnection;
            try
            {
                SerialPortConnection.Open();
                Connected = true;
            }
            catch (System.UnauthorizedAccessException notavail)
            {
                // COM Port in use
                logme.Log(i18n.ErrorMessages.ComPortNotAvailable, logme.LogLevel.error);
                notavail.ToString(); // foo
                Connected = false;
                return;
            }
            catch (IOException ioex)
            {
                logme.Log(ioex);
                Connected = false;
                return;
            }
            catch (Exception ex)
            {
                logme.Log(ex);
                Connected = false;
                return;
            }
        }

        /// <summary>
        /// Close serial connection
        /// </summary>
        private void CloseConnection()
        {
            SerialPortConnection.Close();
            Connected = false;
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

        /// <summary>
        /// indicates whether the central is connected via comport or not
        /// </summary>
        public bool Connected { get; private set; }

        #endregion Serial port connection

        #region ExpressNet communication

        /// <summary>
        /// Last answer of the central
        /// </summary>
        private ILiCommunication LastAnswer { get; set; }

        /// <summary>
        /// Last command to the central
        /// </summary>
        private ILiCommunication LastCommand { get; set; }

        /// <summary>
        /// Indicates the errors received in a row
        /// </summary>
        private int _ErrorInARow;

        /// <summary>
        /// Enqueue a new command to the central command queue
        /// </summary>
        /// <param name="command">bytearray-format command</param>
        public void QueueNewCommand(ILiCommandAndAnswer command)
        {
            SendCommandAndReceiveAnswer(command);
        }

        /// <summary>
        /// Send command to central and receive answer
        /// </summary>
        /// <param name="command"></param>
        private void SendCommandAndReceiveAnswer(ILiCommandAndAnswer command)
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
                        logme.Log(i18n.ErrorMessages.LITimeout, logme.LogLevel.error, command.Command.ByteArray);
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
                        logme.Log(string.Format(i18n.ErrorMessages.ErrorSendingCommad, Config.Data.AllowedCentralErrorsInARow.ToString()), logme.LogLevel.error, command.Command.ByteArray);
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
                logme.Log(i18n.XpressNetLiMessages.EmergencyOffToggle, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCAllOn))
            {
                EmergencyOff = false;
                EmergencyStop = false;
                ProgramMode = false;
                logme.Log(i18n.XpressNetLiMessages.AllOnSet, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCAllLocosOff))
            {
                EmergencyStop = true;
                logme.Log(i18n.XpressNetLiMessages.EmergencyStopToggle, logme.LogLevel.info);
                return;
            }
            if (LastAnswer.GetType() == typeof(BCProgramMode))
            {
                ProgramMode = true;
                logme.Log(i18n.XpressNetLiMessages.ProgrammodeToggle, logme.LogLevel.info);
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
                    LiCommandAndAnswer liVersion = new LiCommandAndAnswer(new GetLIUSBVersion());
                    LiCommandAndAnswer liAddress = new LiCommandAndAnswer(new GetLIUSBAddress());
                    QueueNewCommand(liVersion);
                    QueueNewCommand(liAddress);
                    LIVersionInfo livi = liVersion.Answer as LIVersionInfo;
                    LIAddressInfo liadd = liAddress.Answer as LIAddressInfo;
                    logme.Log(string.Format(i18n.XpressNetLiMessages.LIInfoandAddress, livi.LIVersion.ToString("0.0"), livi.LICodenumber.ToString(), liadd.LIAddress.ToString()), logme.LogLevel.limsg);

                    // get the central version info
                    LiCommandAndAnswer centralVersion = new LiCommandAndAnswer(new GetCentralVersion());
                    QueueNewCommand(centralVersion);
                    CentralVersionInfo cvi = centralVersion.Answer as CentralVersionInfo;
                    logme.Log(string.Format(i18n.XpressNetLiMessages.CentralVersionInfo, cvi.CentralVersion.ToString("0.0"), cvi.CentralTypeName), logme.LogLevel.limsg);

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
                logme.Log(i18n.ErrorMessages.TooManyTriesFetchCentralInfo, logme.LogLevel.warning);
            }
        }

        /// <summary>
        /// Fetch central state
        /// </summary>
        private void GetCentralState()
        {
            // get the central state info
            LiCommandAndAnswer centralState = new LiCommandAndAnswer(new GetCentralStateInfo());
            QueueNewCommand(centralState);
            CentralStateInfo csi = centralState.Answer as CentralStateInfo;

            this.EmergencyOff = csi.EmergencyOff;
            this.EmergencyStop = csi.EmergencyStop;
            this.StartMode = csi.StartModeString;
            this.ProgramMode = csi.ProgramMode;
            this.ColdReset = csi.ColdReset;
            this.RamCheckError = csi.RamCheckError;

            string msg =
                string.Format(i18n.XpressNetLiMessages.CentralStateInfo, ((EmergencyStop) ? (i18n.GrammarBase.yes) : (i18n.GrammarBase.no)),
                ((EmergencyOff) ? (i18n.GrammarBase.yes) : (i18n.GrammarBase.no)), StartMode,
                ((ProgramMode) ? (i18n.GrammarBase.yes) : (i18n.GrammarBase.no)),
                ((ColdReset) ? (i18n.GrammarBase.yes) : (i18n.GrammarBase.no)),
                ((RamCheckError) ? (i18n.GrammarBase.yes) : (i18n.GrammarBase.no)));

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
            var serialPortConnection = new ComPortConnection(port,baudRate,parityBits,dataBits,stopBits);
            Initialize(serialPortConnection);
        }


        /// <summary>
        /// Creates a new XpressNet LI central
        /// </summary>
        /// <param name="serialPortConnection">an existing serialport connection</param>
        public Central(IComPort serialPortConnection)
        {
            Initialize(serialPortConnection);
        }

        /// <summary>
        /// Creates a new XpressNet LI central
        /// </summary>
        private void Initialize(IComPort serialPortConnection)
        {
            Config = new ConfigurationSet();
            Connected = false;

            _RigisteredEntities = new List<ILiEntity>();
            _ErrorInARow = Config.Data.AllowedCentralErrorsInARow;

            // Open connection to interface
            InitConnection(serialPortConnection);

            logme.Log(string.Format(i18n.XpressNetLiMessages.StartLIListener, Port), logme.LogLevel.info);
            SerialPortConnection.ComDataReceived += new ComPortEventHandler(DataReceivedHandler);
            GetInterfaceAndCentralInfo();
        }
    }
}