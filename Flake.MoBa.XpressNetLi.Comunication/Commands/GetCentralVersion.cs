using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting central Version
    /// </summary>
    public class GetCentralVersion : CommandBase, ILICommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public GetCentralVersion()
            : base(i18n.FlakeComunicationCommands.GetCentralVersionName, i18n.FlakeComunicationCommands.GetCentralVersionDesc)
        {
            _ByteArray = new byte[] { 255, 254, FlakeHelper.ConvertTwoDigitHexToByte("21"), FlakeHelper.ConvertTwoDigitHexToByte("21"), FlakeHelper.ConvertTwoDigitHexToByte("00") };
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetCentralVersion);
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