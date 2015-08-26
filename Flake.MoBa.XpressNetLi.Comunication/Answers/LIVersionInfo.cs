using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Command interface version class
    /// </summary>
    public class LIVersionInfo : AnswerBase, ILICommunication
    {
        /// <summary>
        /// Creats a LIVersionInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public LIVersionInfo(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.LIVersionInfoName, i18n.FlakeComunicationAnswers.LIVersionInfoDesc)
        {
            _ByteArray = byteArray;
            LICodenumber = (int)_ByteArray[4];
            LIVersion = Base.FlakeHelper.GetDecimalFromBCD(_ByteArray[3]);
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.LIVersionInfo; }
        }

        /// <summary>
        /// LI-USB-Version
        /// </summary>
        public double LIVersion { get; private set; }

        /// <summary>
        /// LI-USB-Codenumber
        /// </summary>
        public int LICodenumber { get; private set; }
    }
}