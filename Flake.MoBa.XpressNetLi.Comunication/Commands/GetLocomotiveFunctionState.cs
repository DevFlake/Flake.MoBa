using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting locomotive functions switch state for function 13 to 28
    /// </summary>
    public class GetLocomotiveFunctionState : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        public GetLocomotiveFunctionState(HiLoAddress extAddress)
            : base(i18n.FlakeComunicationCommands.GetLocomotiveFunctionStateName, i18n.FlakeComunicationCommands.GetLocomotiveFunctionStateDesc)
        {
            _ByteArray = new byte[] { 255, 254, 227, 9, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetLocomotiveFunctionState, extAddress.Address.ToString());
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