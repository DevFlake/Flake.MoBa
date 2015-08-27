using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using System.Collections.Generic;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using logme = Flake.MoBa.Log.FlakeLog;


namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// CommonLocomotiveInfo class
    /// </summary>
    public class CommonLocomotiveInfo : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// identifier of bytearray
        /// </summary>
        private string _Identifier;

        /// <summary>
        /// 4thframe of bytearray
        /// </summary>
        private string _Speed;

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
        public CommonLocomotiveInfo(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.CommonLocomotiveInfoName, i18n.FlakeComunicationAnswers.CommonLocomotiveInfoDesc)
        {
            _ByteArray = byteArray;
            _Identifier = Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[3], 8);
            _Speed = Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[4], 8);
            _F0 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[5], 8).Substring(3, 5));
            _F1 = Base.FlakeHelper.ReverseBitArray(Base.FlakeHelper.ConvertDecimalToBinary((int)_ByteArray[6], 8));
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
            get { return i18n.FlakeComunicationAnswerLogMsgs.CommonLocomotiveInfo; }
        }

        /// <summary>
        /// indicates the lock of locomotive
        /// </summary>
        public bool LocomotiveLocked { get { return (_Identifier[4] == '1'); } }

        /// <summary>
        /// Returns the current direction of locomotive
        /// </summary>
        public Base.Enums.LocomotiveDirection.LocomotiveDirection Direction { get { return (_Speed[0] == '1') ? (Base.Enums.LocomotiveDirection.LocomotiveDirection.forward) : (Base.Enums.LocomotiveDirection.LocomotiveDirection.backward); } }

        /// <summary>
        /// Get current speed in order of speedSteps
        /// </summary>
        /// <param name="speedSections">speedsteps of locomotive</param>
        /// <returns>Returns a steps-depending value of speed</returns>
        public int GetCurrentSpeed(Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections) { return DecodeSpeedBitArray(_Speed.Substring(1, _Speed.Length - 1), speedSections); }

        /// <summary>
        /// return a dictionary of functions with their settings
        /// </summary>
        public Dictionary<int, bool> Functions { get { return _Functions; } }

        /// <summary>
        /// read the speed sections from locomotive
        /// </summary>
        public Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections ReadSpeedSections
        {
            get
            {
                switch (Base.FlakeHelper.ConvertBinaryStringToDecimal(_Identifier.Substring(4, 3)))
                {
                    case 0: return Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14;
                    case 1: return Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x27;
                    case 2: return Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x28;
                    case 4: return Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x128;
                    default: logme.Log(i18n.FlakeComunicationMsgs.NotSupportetSpeedSections, logme.LogLevel.error);
                        return Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x128;
                }
            }
        }

        /// <summary>
        /// gets a int value from a speed bitarray of a info msg by central
        /// </summary>
        /// <param name="bitArray">info msg by central</param>
        /// <param name="speedSections">speedsections of locomotive to calculate the speed</param>
        /// <returns>returns the decimal representation of given array</returns>
        private int DecodeSpeedBitArray(string bitArray, Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections)
        {
            int ret = 0;
            int temp = 0;
            switch (speedSections)
            {
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14:
                    temp = Base.FlakeHelper.ConvertBinaryStringToDecimal(bitArray);
                    if (temp == 1) ret = 0;
                    ret = temp;
                    break;
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x27:
                    temp = Base.FlakeHelper.ConvertBinaryStringToDecimal(Base.FlakeHelper.ShiftArray(bitArray));
                    if (temp == 1 || temp == 2 || temp == 3) ret = 0;
                    ret = temp;
                    break;
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x28:
                    temp = Base.FlakeHelper.ConvertBinaryStringToDecimal(Base.FlakeHelper.ShiftArray(bitArray));
                    if (temp == 1 || temp == 2 || temp == 3) ret = 0;
                    ret = temp;
                    break;
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x128:
                    temp = Base.FlakeHelper.ConvertBinaryStringToDecimal(bitArray);
                    if (temp == 1) ret = 0;
                    ret = temp;
                    break;
            }
            return ret;
        }
    }
}