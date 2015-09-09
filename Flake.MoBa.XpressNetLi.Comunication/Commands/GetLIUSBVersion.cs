using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting LI USB Version
    /// </summary>
    public class GetLIUSBVersion : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public GetLIUSBVersion()
            : base(i18n.Commands.GetLIUSBVersionName, i18n.Commands.GetLIUSBVersionDesc)
        {
            _ByteArray = new byte[] { 255, 254, 240, 240 };
            _LogMsg = string.Format(i18n.LogMessages.GetLIUSBVersion);
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