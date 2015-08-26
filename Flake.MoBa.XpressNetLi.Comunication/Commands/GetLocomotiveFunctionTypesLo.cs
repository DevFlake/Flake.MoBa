using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using Flake.MoBa.XpressNetLi.Controller;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for getting locomotive function types F0 ti F12
    /// </summary>
    public class GetLocomotiveFunctionTypesLo : CommandBase, ILICommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        public GetLocomotiveFunctionTypesLo(HiLoAddress extAddress)
            : base(i18n.FlakeComunicationCommands.GetLocomotiveFunctionTypesLoName, i18n.FlakeComunicationCommands.GetLocomotiveFunctionTypesLoDesc)
        {
            _ByteArray = new byte[] { 255, 254, 227, 7, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.GetLocomotiveFunctionTypesLo, extAddress.Address.ToString());
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