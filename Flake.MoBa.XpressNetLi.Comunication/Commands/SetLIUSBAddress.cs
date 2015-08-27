using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using logme = Flake.MoBa.Log.FlakeLog;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for setting LI USB Address
    /// </summary>
    public class SetLIUSBAddress : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public SetLIUSBAddress(int address)
            : base(i18n.FlakeComunicationCommands.SetLIUSBAddressName, i18n.FlakeComunicationCommands.SetLIUSBAddressDesc)
        {
            if (address < 1 || address > 31)
            {
                logme.Log(string.Format(i18n.FlakeComunicationErrors.InterfaceAddressCouldNotBeSet, address.ToString()), logme.LogLevel.error);
            }
            _ByteArray = new byte[] { 255, 254, 242, 0, (byte)address };
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.SetLIUSBAddress, address.ToString());
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