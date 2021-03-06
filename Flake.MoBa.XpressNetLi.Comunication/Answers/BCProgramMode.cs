﻿using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// BC Program mode class
    /// </summary>
    public class BCProgramMode : AnswerBase, ILICommunication
    {
        /// <summary>
        /// Creats a BCProgramMode class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public BCProgramMode(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.BCProgramModeName, i18n.FlakeComunicationAnswers.BCProgramModeDesc, true)
        {
            _ByteArray = byteArray;
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.BCProgramMode; }
        }
    }
}