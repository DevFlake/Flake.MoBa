using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for driving a locomotive
    /// </summary>
    public class LocomotiveDrive : CommandBase, ILiCommunication
    {
        string _LogMsg;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        /// <param name="speedValue">Speed of locomotive to be set</param>
        /// <param name="driveForward">use forward direction</param>
        /// <param name="speedSections">speed section setup of locomotive</param>
        public LocomotiveDrive(HiLoAddress extAddress, int speedValue, bool driveForward, Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections)
            : base(i18n.Commands.DriveCommandName, i18n.Commands.DriveCommandDesc)
        {
            _ByteArray = new byte[] { 255, 254, 228, GetIdentifier(speedSections), (byte)extAddress.Address_Hi,
                        (byte)extAddress.Address_Lo, GetSpeedAndDirection(speedValue, driveForward, speedSections) };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.LogMessages.DriveCommand, extAddress.Address.ToString(), speedValue.ToString(), (driveForward) ?
                (new Base.Enums.LocomotiveDirection.LocomotiveDirectionExtended(Base.Enums.LocomotiveDirection.LocomotiveDirection.forward).Name) :
                (new Base.Enums.LocomotiveDirection.LocomotiveDirectionExtended(Base.Enums.LocomotiveDirection.LocomotiveDirection.backward).Name));
        }

        /// <summary>
        /// calculates the speed and direction byte from current values
        /// </summary>
        /// <returns>Returnes a byte with coding fpr speed and direction</returns>
        private static byte GetSpeedAndDirection(int speedValue, bool driveForward, Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections)
        {
            string directioncomponent = (driveForward) ? ("1") : ("0");
            string tmp = string.Empty;
            switch (speedSections)
            {
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14:
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "000" + Base.FlakeHelper.ConvertDecimalToBinary(speedValue, 4)));
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x27:
                    tmp = Base.FlakeHelper.ConvertDecimalToBinary(speedValue + 3, 5);
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "00" + tmp.Substring(4, 1) + tmp.Substring(0, 4)));
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x28:
                    tmp = Base.FlakeHelper.ConvertDecimalToBinary(speedValue + 3, 5);
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "00" + tmp.Substring(4, 1) + tmp.Substring(0, 4)));
                default:
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + Base.FlakeHelper.ConvertDecimalToBinary(speedValue, 7)));
            }
        }

        /// <summary>
        /// Gets the identifier in order of speedsections
        /// </summary>
        /// <param name="speedSections">Number of sections of speed</param>
        /// <returns>Returns a representing byte for identifier</returns>
        private byte GetIdentifier(Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections)
        {
            switch (speedSections)
            {
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14:
                    return (byte)16;
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x27:
                    return (byte)17;
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x28:
                    return (byte)18;
                default:
                    return (byte)19;
            }
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