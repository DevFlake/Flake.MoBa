using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Base;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting central state
    /// </summary>
    public class GetCentralStateInfo : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        public GetCentralStateInfo()
            : base(i18n.Commands.GetCentralStateInfoName, i18n.Commands.GetCentralStateInfoDesc)
        {
            _ByteArray = new byte[] { 255, 254, FlakeHelper.ConvertTwoDigitHexToByte("21"), FlakeHelper.ConvertTwoDigitHexToByte("24"), FlakeHelper.ConvertTwoDigitHexToByte("05") };
            _LogMsg = string.Format(i18n.LogMessages.GetCentralStateInfo);
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