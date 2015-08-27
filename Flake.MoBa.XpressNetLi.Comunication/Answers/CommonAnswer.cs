using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;


namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// CommonAnswer class
    /// </summary>
    public class CommonAnswer : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a CommonAnswer class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public CommonAnswer(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.CommonAnswerName, i18n.FlakeComunicationAnswers.CommonAnswerDesc)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.CommonAnswer; }
        }
    }
}