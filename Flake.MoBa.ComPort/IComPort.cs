using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Flake.MoBa.ComPort
{
    /// <summary>
    /// Interface for a Comport Implementation
    /// </summary>
    public interface IComPort
    {
        /// <summary>
        /// Events that gets triggered when data arrives
        /// </summary>
        event Flake.MoBa.ComPort.ComPortEventHandler ComDataReceived;

        /// <summary>
        /// write a bytearray to the comport
        /// </summary>
        /// <param name="buffer">bytearray to write</param>
        /// <param name="offset">start point</param>
        /// <param name="count">lenght to write</param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// read a bytearray from the comport
        /// </summary>
        /// <param name="buffer">bytearray to write</param>
        /// <param name="offset">start point</param>
        /// <param name="count">lenght to write</param>
        void Read(ref byte[] buffer, int offset, int count);

        /// <summary>
        /// number of waiting bytes to be read
        /// </summary>
        int BytesToRead { get; }

        /// <summary>
        /// Port property BaudRate
        /// </summary>
        int BaudRate { get; set; }

        /// <summary>
        /// Port property Parity
        /// </summary>
        ComPortParity Parity { get; set; }

        /// <summary>
        /// Port property Handshake
        /// </summary>
        ComPortHandshake Handshake { get; set; }

        /// <summary>
        /// Port property DataBits
        /// </summary>
        int DataBits { get; set; }

        /// <summary>
        /// Port property StopBits
        /// </summary>
        ComPortStopBits StopBits { get; set; }

        /// <summary>
        /// Port property PortName
        /// </summary>
        string PortName { get; set; }

        /// <summary>
        /// opens the connection to the comport
        /// </summary>
        void Open();

        /// <summary>
        /// close comport connection
        /// </summary>
        void Close();

        /// <summary>
        /// dispose me
        /// </summary>
        void Dispose();
    }

    /// <summary>
    /// Enventhandler for receiving data
    /// </summary>
    public delegate void ComPortEventHandler();

    /// <summary>
    /// parity for comport
    /// </summary>
    public enum ComPortParity { None, Even, Mark, Odd, Space }

    /// <summary>
    /// handshakr option for comport
    /// </summary>
    public enum ComPortHandshake { None, RequestToSend, RequestToSendXOnXOff, XOnXOff }

    /// <summary>
    /// stopbits for comport
    /// </summary>
    public enum ComPortStopBits { None, One, OnePointFive, Two }
}