using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting central Version
    /// </summary>
    public class GetCentralVersion : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public GetCentralVersion()
            : base(i18n.Commands.GetCentralVersionName, i18n.Commands.GetCentralVersionDesc)
        {
            _ByteArray = new byte[] { 255, 254, FlakeHelper.ConvertTwoDigitHexToByte("21"), FlakeHelper.ConvertTwoDigitHexToByte("21"), FlakeHelper.ConvertTwoDigitHexToByte("00") };
            _LogMsg = string.Format(i18n.LogMessages.GetCentralVersion);
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