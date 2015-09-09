using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using logme = Flake.MoBa.Log.FlakeLog;
namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Command interface address class
    /// </summary>
    public class LIAddressInfo : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a LIAddressInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public LIAddressInfo(byte[] byteArray)
            : base(i18n.Answers.LIAddressInfoName, i18n.Answers.LIAddressInfoDesc)
        {
            _ByteArray = byteArray;
            LIAddress = (int)_ByteArray[4];
            if (LIAddress < 1 || LIAddress > 31)
            {
                logme.Log(string.Format(i18n.ComunicationMessages.ErrorReceivingFunctionNumber, LIAddress.ToString()), logme.LogLevel.error, byteArray);
            }
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.LogMessages.LIAddressInfo; }
        }

        /// <summary>
        /// LI-USB-Address
        /// </summary>
        public int LIAddress { get; private set; }
    }
}