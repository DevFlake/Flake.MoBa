using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using logme = Flake.MoBa.Log.FlakeLog;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using System;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Command central version class
    /// </summary>
    public class CentralVersionInfo : AnswerBase, ILiCommunication
    {
        /// <summary>
        /// Creats a LIVersionInfo class
        /// </summary>
        /// <param name="byteArray">bytearray from central</param>
        public CentralVersionInfo(byte[] byteArray)
            : base(i18n.FlakeComunicationAnswers.CentralVersionInfoName, i18n.FlakeComunicationAnswers.CentralVersionInfoDesc)
        {
            _ByteArray = byteArray;
            CentralVersion = Base.FlakeHelper.GetDecimalFromBCD(_ByteArray[4]);
            switch (_ByteArray[5])
            {
                case (byte)0:
                    CentralType = TypeOfCentral.LZ100;
                    break;
                case (byte)1:
                    CentralType = TypeOfCentral.LH200;
                    break;
                case (byte)2:
                    CentralType = TypeOfCentral.DPC;
                    break;
                default:
                    logme.Log(i18n.FlakeComunicationMsgs.NotRecognizedCentralType, logme.LogLevel.error, byteArray);
                    break;
            }
        }

        /// <summary>
        /// Get the message for logging ths answertype
        /// </summary>
        public string LogMessage
        {
            get { return i18n.FlakeComunicationAnswerLogMsgs.CentralVersionInfo; }
        }

        /// <summary>
        /// Central-version
        /// </summary>
        public double CentralVersion { get; private set; }

        /// <summary>
        /// Central types
        /// </summary>
        public enum TypeOfCentral
        {
            /// <summary>
            /// LZ 100 Central
            /// </summary>
            LZ100 = 0,
            /// <summary>
            /// LH 200 Central
            /// </summary>
            LH200 = 1,
            /// <summary>
            /// DPC Central (Compact or Commander)
            /// </summary>
            DPC = 2
        }

        /// <summary>
        /// Central Type
        /// </summary>
        public TypeOfCentral CentralType { get; private set; }

        /// <summary>
        /// Central Type as string
        /// </summary>
        public string CentralTypeName
        {
            get
            {
                switch (CentralType)
                {
                    case TypeOfCentral.LZ100:
                        return i18n.FlakeComunicationAnswers.CentralTypeLZ100;
                    case TypeOfCentral.LH200:
                        return i18n.FlakeComunicationAnswers.CentralTypeLH200;
                    case TypeOfCentral.DPC:
                        return i18n.FlakeComunicationAnswers.CentralTypeDCP;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}