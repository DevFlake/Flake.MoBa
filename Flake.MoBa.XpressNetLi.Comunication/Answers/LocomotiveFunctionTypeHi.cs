using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using System.Collections.Generic;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// LocomotiveFunctionTypeLo class (function type info F0 to F12)
    /// </summary>
    public class LocomotiveFunctionTypeHi : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// 5thframe of bytearray
        /// </summary>
        private string _F2;

        /// <summary>
        /// 6thframe of bytearray
        /// </summary>
        private string _F3;

        /// <summary>
        /// current Functionsettings
        /// </summary>
        private Dictionary<int, bool> _Functions;

        /// <summary>
        /// Creats a CommonLocomotiveInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public LocomotiveFunctionTypeHi(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.LocomotiveFunctionTypeHiName, i18n.FlakeComunicationAnswers.LocomotiveFunctionTypeHiDesc)
        {
            _ByteArray = byteArray;
            _F2 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[4], 8));
            _F3 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[5], 8));
            _Functions = new Dictionary<int, bool>();

            // functions
            string temp = _F2 + _F3;
            //for (int i = 0; i < 8; i++) { _Functions.Add(i + 5, (_F1[i] == '1')); }
            // for (int i = 0; i < 6; i++) { if (i == 4) i = -1; _Functions.Add(i + 1, (_F0[i] == '1')); if (i == -1) break; }
            for (int i = 13; i < 28; i++) { _Functions.Add(i, (temp[i - 13] == '1')); }
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.LocomotiveFunctionTypeHi; }
        }

        /// <summary>
        /// return a dictionary of functions with their type
        /// </summary>
        /// <remarks>true means tapping; false means switch</remarks>
        public Dictionary<int, bool> Functions { get { return _Functions; } }
    }
}