using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;


namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Command not available class
    /// </summary>
    public class CommandNotAvailable : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a CommandNotAvailable class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public CommandNotAvailable(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.CommandNotAvailableName, i18n.FlakeComunicationAnswers.CommandNotAvailableDesc)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.CommandNotAvailable; }
        }
    }
}