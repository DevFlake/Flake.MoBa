using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using System.Collections.Generic;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// LocomotiveFunctionTypeLo class (function type info F0 to F12)
    /// </summary>
    public class LocomotiveFunctionTypeLo : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// 5thframe of bytearray
        /// </summary>
        private string _F0;

        /// <summary>
        /// 6thframe of bytearray
        /// </summary>
        private string _F1;

        /// <summary>
        /// current Functionsettings
        /// </summary>
        private Dictionary<int, bool> _Functions;

        /// <summary>
        /// Creats a CommonLocomotiveInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public LocomotiveFunctionTypeLo(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.LocomotiveFunctionTypeLoName, i18n.FlakeComunicationAnswers.LocomotiveFunctionTypeLoDesc)
        {
            _ByteArray = byteArray;
            _F0 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[4], 8).Substring(3, 5));
            _F1 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[5], 8));
            _Functions = new Dictionary<int, bool>();

            // functions
            string temp = Base.FlakeHelper.ShiftArray(_F0, 1, false) + _F1;
            //for (int i = 0; i < 8; i++) { _Functions.Add(i + 5, (_F1[i] == '1')); }
            // for (int i = 0; i < 6; i++) { if (i == 4) i = -1; _Functions.Add(i + 1, (_F0[i] == '1')); if (i == -1) break; }
            for (int i = 0; i < 12; i++) { _Functions.Add(i, (temp[i] == '1')); }
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.LocomotiveFunctionTypeLo; }
        }

        /// <summary>
        /// return a dictionary of functions with their type
        /// </summary>
        /// <remarks>1 means tapping; 0 means switch</remarks>
        public Dictionary<int, bool> Functions { get { return _Functions; } }
    }
}