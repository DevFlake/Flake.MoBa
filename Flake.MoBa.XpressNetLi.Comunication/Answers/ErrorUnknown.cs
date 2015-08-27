using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// ErrorUnknown class
    /// </summary>
    public class ErrorUnknown : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a CommonAnswer class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public ErrorUnknown(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.ErrorUnknownName, i18n.FlakeComunicationAnswers.ErrorUnknownDesc)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.ErrorUnknown; }
        }
    }
}