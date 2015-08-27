using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for setting refresh mode for a locomotive
    /// </summary>
    public class SetLFunctionRefreshMode : CommandBase, ILiCommunication
    {
        /// <summary>
        /// internal log message
        /// </summary>
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Adress of locomotive</param>
        /// <param name="mode">Startmode for central</param>
        public SetLFunctionRefreshMode(HiLoAddress extAddress, Base.Enums.LocoFunctionRefreshMode.LocoFunctionRefreshMode mode)
            : base(i18n.FlakeComunicationCommands.SetLFunctionRefreshModeName, i18n.FlakeComunicationCommands.SetLFunctionRefreshModeDesc)
        {
            _ByteArray = new byte[] { 255, 254, 228, 47, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo, (byte)mode };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.SetLFunctionRefreshMode, new Base.Enums.LocoFunctionRefreshMode.LocoFunctionRefreshModeExtended(mode).Name, extAddress.Address.ToString());
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