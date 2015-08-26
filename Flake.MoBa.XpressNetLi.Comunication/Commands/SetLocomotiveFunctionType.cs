using i18n = Flake.MoBa.XpressNetLi.Comunication.Resources;
using Flake.MoBa.XpressNetLi.Comunication.Interfaces;
using Flake.MoBa.XpressNetLi.Base;
using logme = Flake.MoBa.Log.FlakeLog;
using System.Collections.Generic;
using Flake.MoBa.XpressNetLi.Controller;
using System.Linq;

namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Commad for setting locomotive function types
    /// </summary>
    public class SetLocomotiveFunctionType : CommandBase, ILICommunication
    {
        /// <summary>
        /// internal log message
        /// </summary>
        string _LogMsg;

        /// <summary>
        /// Dictionalry of locomotive functions and their stats
        /// </summary>
        private Dictionary<int, bool> _Functions;

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="extAddress">Extended address of locomotive</param>
        /// <param name="functionNumber">function-number to switch</param>
        /// <param name="setTapping">set on tapping (1) or switching (0) the function</param>
        /// <param name="currentFunctions">current function state dictionary</param>
        public SetLocomotiveFunctionType(HiLoAddress extAddress, int functionNumber, bool setTapping, Dictionary<int, FlakeTrain.FlakeController.Locomotive.LocomotiveFunction> currentFunctions)
            : base(i18n.FlakeComunicationCommands.SetLocomotiveFunctionTypeName, i18n.FlakeComunicationCommands.SetLocomotiveFunctionTypeDesc)
        {
            _Functions = new Dictionary<int, bool>();
            foreach (var f in currentFunctions)
            {
                if (!_Functions.Keys.Contains(f.Key)) _Functions.Add(f.Key, (f.Value.Type == FlakeTrain.FlakeController.Locomotive.LocomotiveFunctionType.tapping));
            }
            if (_Functions.Keys.Contains(functionNumber)) _Functions[functionNumber] = setTapping;

            byte identifier;
            byte data3byte = GetData3Byte(functionNumber, out identifier);

            _ByteArray = new byte[] { 255, 254, 228, identifier, (byte)extAddress.Address_Hi, (byte)extAddress.Address_Lo, data3byte };
            CommunicationHelper.AddChecksumByteToArray(ref _ByteArray);
            _LogMsg = string.Format(i18n.FlakeComunicationCommandsLogMsgs.SetLocomotiveFunctionType, functionNumber.ToString(), extAddress.Address.ToString(), (setTapping) ? (i18n.FlakeComunicationBase.tapping) : (i18n.FlakeComunicationBase.switching));
        }

        /// <summary>
        /// Returns a message for logging
        /// </summary>
        public string LogMessage
        {
            get { return _LogMsg; }
        }

        /// <summary>
        /// Switches a locomotive function types
        /// </summary>
        /// <param name="functionNumber">f-number</param>
        /// <param name="functionGroup">datagroup which represents the given f-number (as byte)</param>
        public byte GetData3Byte(int functionNumber, out byte functionGroup)
        {
            byte data3byte = 0;
            string binaryData = string.Empty;
            functionGroup = 36;
            if (functionNumber > -1 && functionNumber < 5)
            {
                functionGroup = 32;
                binaryData = "000" + ((_Functions.ContainsKey(0)) ? ((_Functions[0]) ? ("1") : ("0")) : ("0"));
                for (int i = 4; i > 0; i--)
                {
                    binaryData += ((_Functions.ContainsKey(i)) ? ((_Functions[i]) ? ("1") : ("0")) : ("0"));
                }
                data3byte = (byte)FlakeHelper.ConvertBinaryStringToDecimal(binaryData);
            }
            if (functionNumber > 4 && functionNumber < 9)
            {
                functionGroup = 37;
                binaryData = "0000";
                for (int i = 8; i > 4; i--)
                {
                    binaryData += ((_Functions.ContainsKey(i)) ? ((_Functions[i]) ? ("1") : ("0")) : ("0"));
                }
                data3byte = (byte)FlakeHelper.ConvertBinaryStringToDecimal(binaryData);
            }
            if (functionNumber > 8 && functionNumber < 13)
            {
                functionGroup = 38;
                binaryData = "0000";
                for (int i = 12; i > 7; i--)
                {
                    binaryData += ((_Functions.ContainsKey(i)) ? ((_Functions[i]) ? ("1") : ("0")) : ("0"));
                }
                data3byte = (byte)FlakeHelper.ConvertBinaryStringToDecimal(binaryData);
            }
            if (functionNumber > 12 && functionNumber < 21)
            {
                functionGroup = 39;
                for (int i = 20; i > 12; i--)
                {
                    binaryData += ((_Functions.ContainsKey(i)) ? ((_Functions[i]) ? ("1") : ("0")) : ("0"));
                }
                data3byte = (byte)FlakeHelper.ConvertBinaryStringToDecimal(binaryData);
            }
            if (functionNumber > 20 && functionNumber < 29)
            {
                functionGroup = 44;
                for (int i = 28; i > 19; i--)
                {
                    binaryData += ((_Functions.ContainsKey(i)) ? ((_Functions[i]) ? ("1") : ("0")) : ("0"));
                }
            }
            if (functionNumber < -1 || functionNumber > 28)
            {
                // if we arrive here there is an error in the choose of functionnumber (too large)
                logme.Log(i18n.FlakeComunicationMsgs.ErrorReceivingFunctionNumber, logme.LogLevel.error);
            }
            return data3byte;
        }
    }
}