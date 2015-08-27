using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting locomotive function types F13 ti F28
    /// </summary>
    public class GetLocomotiveFunctionTypesHi : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        public GetLocomotiveFunctionTypesHi(HiLoAddress extAddress)
            : base(i18n.FlakeComunicationCommands.GetLocomotiveFunctionTypesHiName, i18n.FlakeComunicationCommands.GetLocomotiveFunctionTypesHidesc)
        {
            _ByteArray = new byte[] { 255, 254, 227, 8, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetLocomotiveFunctionTypesHi, extAddress.Address.ToString());
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