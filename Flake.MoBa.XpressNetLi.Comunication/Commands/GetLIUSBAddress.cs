using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting LI USB Address
    /// </summary>
    public class GetLIUSBAddress : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public GetLIUSBAddress()
            : base(i18n.FlakeComunicationCommands.GetLIUSBAddressName, i18n.FlakeComunicationCommands.GetLIUSBAddressDesc)
        {
            _ByteArray = new byte[] { 255, 254, 242, 1, 0 };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetLIUSBAddress);
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