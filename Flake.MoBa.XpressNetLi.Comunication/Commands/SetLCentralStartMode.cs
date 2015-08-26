using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for setting startmode of central
    /// </summary>
    public class SetLCentralStartMode : CommandBase, ILICommunication
    {
        /// <summary>
        /// internal log message
        /// </summary>
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="mode">Startmode for central</param>
        public SetLCentralStartMode(Base.Enums.CentralStartMode.CentralStartMode mode)
            : base(i18n.FlakeComunicationCommands.SetLCentralStartModeName, i18n.FlakeComunicationCommands.SetLCentralStartModeDesc)
        {
            byte databyte = (byte)Base.FlakeHelper.ConvertBinaryStringToDecimal(string.Format("00000{0}00", (mode == Base.Enums.CentralStartMode.CentralStartMode.auto) ? ("1") : ("0")));
            _ByteArray = new byte[] { 255, 254, 34, 34, databyte };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.SetLCentralStartMode, new Base.Enums.CentralStartMode.CentralStartModeExtended(mode).Name);
        }

        /// <summary>
        /// Returns a message for logging
        /// </summary>
        public string LogMessage
        {
            get { return _LogMsg; }
        }
    }
}