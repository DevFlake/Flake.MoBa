using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;


namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// ErrorSending class
    /// </summary>
    public class ErrorSending : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a CommonAnswer class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public ErrorSending(byte[] byteArray)
            : base(i18n.Answers.ErrorSendingName, i18n.Answers.ErrorSendingDesc)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.LogMessages.ErrorSending; }
        }
    }
}