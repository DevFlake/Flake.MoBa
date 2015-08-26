using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Command central state class
    /// </summary>
    public class CentralStateInfo : AnswerBase, ILICommunication
    {
        /// <summary>
        /// Creats a CentralStateInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public CentralStateInfo(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.CentralStateInfoName, i18n.FlakeComunicationAnswers.CentralStateInfoDesc)
        {
            _ByteArray = byteArray;

            string tmp = Base.FlakeHelper.ConvertDecimalToBinary(_ByteArray[4], 8);
            EmergencyStop = (tmp[0] == '1');
            EmergencyOff = (tmp[1] == '1');
            StartMode = (tmp[2] == '1') ? (Base.Enums.CentralStartMode.CentralStartMode.auto) : (Base.Enums.CentralStartMode.CentralStartMode.man);
            ProgramMode = (tmp[3] == '1');
            ColdReset = (tmp[6] == '1');
            RamCheckError = (tmp[7] == '1');
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.CentralStateInfo; }
        }

        /// <summary>
        /// Indicator for emergency stop
        /// </summary>
        public bool EmergencyStop { get; private set; }

        /// <summary>
        /// Indicator for emergency off
        /// </summary>
        public bool EmergencyOff { get; private set; }

        /// <summary>
        /// Start type of the central
        /// </summary>
        public Base.Enums.CentralStartMode.CentralStartMode StartMode { get; private set; }

        /// <summary>
        /// Start type of the central as Text
        /// </summary>
        public string StartModeString
        {
            get
            {
                return new Base.Enums.CentralStartMode.CentralStartModeExtended(StartMode).Name;
            }
        }

        /// <summary>
        /// Indicator of programmode
        /// </summary>
        public bool ProgramMode { get; private set; }

        /// <summary>
        /// Indicator of a cold start or reset
        /// </summary>
        public bool ColdReset { get; private set; }

        /// <summary>
        /// Indicator of a ram error in central
        /// </summary>
        public bool RamCheckError { get; private set; }
    }
}