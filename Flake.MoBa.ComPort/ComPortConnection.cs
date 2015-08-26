using System.IO.Ports;

namespace Flake.MoBa.ComPort
{
    /// <summary>
    /// comport proxy
    /// </summary>
    public class ComPortConnection : IComPort
    {
        /// <summary>
        /// the real serial port
        /// </summary>
        private SerialPort _SerialPort;

        /// <summary>
        /// constructor
        /// </summary>
        public ComPortConnection(string portName, int baudRate, ComPortParity parity, int dataBits, ComPortStopBits stopBits)
            : base()
        {
            _SerialPort = new SerialPort(portName, baudRate, Translate(parity), dataBits, Translate(stopBits));
            _SerialPort.DataReceived += new SerialDataReceivedEventHandler(OnDataReceive);
        }

        /// <summary>
        /// event for receiving data
        /// </summary>
        public event Flake.MoBa.ComPort.ComPortEventHandler ComDataReceived;

        /// <summary>
        /// receiving data from serial port
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">eventargs</param>
        protected virtual void OnDataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            ComDataReceived();
        }

        /// <summary>
        /// write a bytearray to the comport
        /// </summary>
        /// <param name="buffer">bytearray to write</param>
        /// <param name="offset">start point</param>
        /// <param name="count">lenght to write</param>
        void IComPort.Write(byte[] buffer, int offset, int count)
        {
            _SerialPort.Write(buffer, offset, count);
        }

        /// <summary>
        /// read a bytearray from the comport
        /// </summary>
        /// <param name="buffer">bytearray to write</param>
        /// <param name="offset">start point</param>
        /// <param name="count">lenght to write</param>
        void IComPort.Read(ref byte[] buffer, int offset, int count)
        {
            _SerialPort.Read(buffer, offset, count);
        }

        /// <summary>
        /// number of waiting bytes to be read
        /// </summary>
        int IComPort.BytesToRead
        {
            get { return _SerialPort.BytesToRead; }
        }

        /// <summary>
        /// Port property BaudRate
        /// </summary>
        int IComPort.BaudRate
        {
            get
            {
                return _SerialPort.BaudRate;
            }
            set
            {
                _SerialPort.BaudRate = value;
            }
        }

        /// <summary>
        /// Port property Parity
        /// </summary>
        ComPortParity IComPort.Parity
        {
            get
            {
                return Translate(_SerialPort.Parity);
            }
            set
            {
                _SerialPort.Parity = Translate(value);
            }
        }

        /// <summary>
        /// Port property Handshake
        /// </summary>
        ComPortHandshake IComPort.Handshake
        {
            get
            {
                switch (_SerialPort.Handshake)
                {
                    case Handshake.RequestToSend: return ComPortHandshake.RequestToSend;
                    case Handshake.RequestToSendXOnXOff: return ComPortHandshake.RequestToSendXOnXOff;
                    case Handshake.XOnXOff: return ComPortHandshake.XOnXOff;
                    default: return ComPortHandshake.None;
                }
            }
            set
            {
                switch (value)
                {
                    case ComPortHandshake.RequestToSend: _SerialPort.Handshake = Handshake.RequestToSend; break;
                    case ComPortHandshake.RequestToSendXOnXOff: _SerialPort.Handshake = Handshake.RequestToSendXOnXOff; break;
                    case ComPortHandshake.XOnXOff: _SerialPort.Handshake = Handshake.XOnXOff; break;
                    default: _SerialPort.Handshake = Handshake.None; break;
                }
            }
        }

        /// <summary>
        /// Port property DataBits
        /// </summary>
        int IComPort.DataBits
        {
            get
            {
                return _SerialPort.DataBits;
            }
            set
            {
                _SerialPort.DataBits = value;
            }
        }

        /// <summary>
        /// Port property StopBits
        /// </summary>
        ComPortStopBits IComPort.StopBits
        {
            get
            {
                return Translate(_SerialPort.StopBits);
            }
            set
            {
                _SerialPort.StopBits = Translate(value);
            }
        }

        /// <summary>
        /// Port property PortName
        /// </summary>
        string IComPort.PortName
        {
            get
            {
                return _SerialPort.PortName;
            }
            set
            {
                _SerialPort.PortName = value;
            }
        }

        /// <summary>
        /// opens the connection to the comport
        /// </summary>
        void IComPort.Open()
        {
            _SerialPort.Open();
        }

        /// <summary>
        /// close comport connection
        /// </summary>
        void IComPort.Close()
        {
            _SerialPort.Close();
        }

        /// <summary>
        /// dispose me
        /// </summary>
        void IComPort.Dispose()
        {
            _SerialPort.Close();
        }

        /// <summary>
        /// translate flake to serialport an vv
        /// </summary>
        /// <param name="value">flakenoun or serialportnoun</param>
        /// <returns>the representing other thing :-)</returns>
        private Parity Translate(ComPortParity value)
        {
            switch (value)
            {
                case ComPortParity.Even: return Parity.Even;
                case ComPortParity.Mark: return Parity.Mark;
                case ComPortParity.Odd: return Parity.Odd;
                case ComPortParity.Space: return Parity.Space;
                default: return Parity.None;
            }
        }

        /// <summary>
        /// translate flake to serialport an vv
        /// </summary>
        /// <param name="value">flakenoun or serialportnoun</param>
        /// <returns>the representing other thing :-)</returns>
        private ComPortParity Translate(Parity value)
        {
            switch (value)
            {
                case Parity.Even: return ComPortParity.Even;
                case Parity.Mark: return ComPortParity.Mark;
                case Parity.Odd: return ComPortParity.Odd;
                case Parity.Space: return ComPortParity.Space;
                default: return ComPortParity.None;
            }
        }

        /// <summary>
        /// translate flake to serialport an vv
        /// </summary>
        /// <param name="value">flakenoun or serialportnoun</param>
        /// <returns>the representing other thing :-)</returns>
        private StopBits Translate(ComPortStopBits value)
        {
            switch (value)
            {
                case ComPortStopBits.One: return StopBits.One;
                case ComPortStopBits.OnePointFive: return StopBits.OnePointFive;
                case ComPortStopBits.Two: return StopBits.Two;
                default: return StopBits.None;
            }
        }

        /// <summary>
        /// translate flake to serialport an vv
        /// </summary>
        /// <param name="value">flakenoun or serialportnoun</param>
        /// <returns>the representing other thing :-)</returns>
        private ComPortStopBits Translate(StopBits value)
        {
            switch (value)
            {
                case StopBits.One: return ComPortStopBits.One;
                case StopBits.OnePointFive: return ComPortStopBits.OnePointFive;
                case StopBits.Two: return ComPortStopBits.Two;
                default: return ComPortStopBits.None;
            }
        }
    }
}