using Flake.MoBa.ComPort;
using Flake.MoBa.XPressNetLi.Configuration;

namespace Flake.MoBa.XpressNetLi.Entities.Interfaces
{
    public interface ICentral
    {
        int BaudRate { get; }
        bool ColdReset { get; }
        ConfigurationSet Config { get; }
        int DataBits { get; }
        bool EmergencyOff { get; }
        bool EmergencyStop { get; }
        ComPortParity ParityBits { get; }
        string Port { get; }
        bool ProgramMode { get; }
        bool RamCheckError { get; }
        IComPort SerialPortConnection { get; }
        string StartMode { get; }
        ComPortStopBits StopBits { get; }

        void QueueNewCommand(ILiCommandAndAnswer command);
        void RegisterEntity(ILiEntity item);
    }
}