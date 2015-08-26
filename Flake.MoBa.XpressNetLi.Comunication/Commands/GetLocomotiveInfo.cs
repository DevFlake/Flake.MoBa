using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using Flake.MoBa.XpressNetLi.Controller;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting base locomotive info
    /// </summary>
    public class GetLocomotiveInfo : CommandBase, ILICommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        public GetLocomotiveInfo(HiLoAddress extAddress)
            : base(i18n.FlakeComunicationCommands.GetLocomotiveInfoName, i18n.FlakeComunicationCommands.GetLocomotiveInfoDesc)
        {
            _ByteArray = new byte[] { 255, 254, 227, 0, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetLocomotiveInfo, extAddress.Address.ToString());
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