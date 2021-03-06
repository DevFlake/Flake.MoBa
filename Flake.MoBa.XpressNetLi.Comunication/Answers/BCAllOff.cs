﻿using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// BC All Off class
    /// </summary>
    public class BCAllOff : AnswerBase, ILICommunication
    {
        /// <summary>
        /// Creats a BCAllOff class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public BCAllOff(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.BCAllOffName, i18n.FlakeComunicationAnswers.BCAllOffDesc, true)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.BCAllOff; }
        }
    }
}