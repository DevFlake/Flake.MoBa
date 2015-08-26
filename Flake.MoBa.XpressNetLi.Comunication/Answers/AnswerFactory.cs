using System.Collections.Generic;
using System.Linq;
using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using logme = Flake.MoBa.Log.FlakeLog;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Factory for creating answers
    /// </summary>
    public static class AnswerFactory
    {
        /// <summary>
        /// Creates a new answer from factory
        /// </summary>
        /// <param name="answerArray">bytearray from central</param>
        /// <returns>returns a representational class to the central given bytearray</returns>
        public static ILICommunication CreateNew(byte[] answerArray)
        {
            ILICommunication answer = GetNewAnswer(answerArray);
            return answer;
        }

        /// <summary>
        /// Returns a new answer depending on bytearray
        /// </summary>
        /// <param name="answerArray">answer array without leading bytes (2)</param>
        /// <returns>returns a representing representation of an answer :-)</returns>
        private static ILICommunication GetNewAnswer(byte[] answerArray)
        {
            if (TestAnswerForXORByte(answerArray))
            {
                byte[] selector = CutOffStartBytes(answerArray);
                byte header = selector[0];
                byte identifier = selector[1];
                byte data2;
                if (selector.Length > 2) data2 = selector[2];

                switch (header)
                {
                    case 1:
                        switch (identifier)
                        {
                            case 1:
                                // Sending error
                                return new ErrorSending(answerArray);
                            case 4:
                                // Common Answer
                                return new CommonAnswer(answerArray);
                            case 10:
                                // Unknown error
                                return new ErrorUnknown(answerArray);
                        }
                        break;

                    case 2:
                        // LI-USB version info
                        return new LIVersionInfo(answerArray);

                    case 97:
                        switch (identifier)
                        {
                            case 1:
                                // Broadcast All On
                                return new BCAllOn(answerArray);
                            case 0:
                                // Broadcast All Off
                                return new BCAllOff(answerArray);
                            case 2:
                                // Broadcast programm mode
                                return new BCProgramMode(answerArray);
                            case 130:
                                // command not available
                                return new CommandNotAvailable(answerArray);
                        }
                        break;

                    case 98:
                        switch (identifier)
                        {
                            case 34:
                                // central state
                                return new CentralStateInfo(answerArray);
                        }
                        break;

                    case 99:
                        switch (identifier)
                        {
                            case 33:
                                // central version
                                return new CentralVersionInfo(answerArray);
                        }
                        break;

                    case 129:
                        switch (identifier)
                        {
                            case 0:
                                // Broadcast All locomotives off
                                return new BCAllLocosOff(answerArray);
                        }
                        break;

                    case 227:
                        {
                            switch (identifier)
                            {
                                case 80:
                                    // type of functions F0 to F12
                                    return new LocomotiveFunctionTypeLo(answerArray);
                                case 82:
                                    // state of F13 to F28
                                    return new LocomotiveFunctionStateExt(answerArray);
                            }
                        }
                        break;

                    case 228:
                        switch (identifier)
                        {
                            case 81:
                                // type of functions F13 to F28
                                return new LocomotiveFunctionTypeHi(answerArray);
                            default:
                                // Locomotive Info Answer
                                return new CommonLocomotiveInfo(answerArray);
                        }

                    case 242:
                        switch (identifier)
                        {
                            case 1:
                                // LI-USB Address
                                return new LIAddressInfo(answerArray);
                        }
                        break;
                }
            }
            else
            {
                logme.Log(i18n.FlakeComunicationErrors.WrongAnswerFormat, logme.LogLevel.error);
                return null;
            }
            logme.Log(i18n.FlakeComunicationErrors.UnknownAnswer, logme.LogLevel.error);
            return null;
        }

        /// <summary>
        /// cuts off the two leading bytes of an bytearray
        /// </summary>
        /// <param name="answerArray">bytearray from central</param>
        /// <returns>returns the given bytearray without the two leading bytes (frame 1 and 2)</returns>
        public static byte[] CutOffStartBytes(byte[] answerArray)
        {
            List<byte> ret = new List<byte>();
            for (int i = 2; i < answerArray.Length; i++)
            {
                ret.Add(answerArray[i]);
            }
            return ret.ToArray();
        }

        private static bool TestAnswerForXORByte(byte[] answerArray)
        {
            if (answerArray.Length > 1)
            {
                List<byte> tmp = answerArray.ToList();
                byte check = tmp[tmp.Count - 1];
                tmp.RemoveAt(tmp.Count - 1);
                return (Base.FlakeHelper.CalculateChecksumByteOfArray(tmp.ToArray(), true) == check);
            }
            else
            {
                return false;
            }
        }
    }
}