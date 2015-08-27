using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// BC All Locomotives Off class
    /// </summary>
    public class BCAllLocosOff : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a BCAllLocosOff class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public BCAllLocosOff(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.BCAllLocosOffName, i18n.FlakeComunicationAnswers.BCAllLocosOffDesc, true)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.BCAllLocosOff; }
        }
    }
}