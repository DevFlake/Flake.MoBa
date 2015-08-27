using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// BC All On class
    /// </summary>
    public class BCAllOn : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a BCAllOn class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public BCAllOn(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.BCAllOnName, i18n.FlakeComunicationAnswers.BCAllOnDesc, true)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.BCAllOn; }
        }
    }
}