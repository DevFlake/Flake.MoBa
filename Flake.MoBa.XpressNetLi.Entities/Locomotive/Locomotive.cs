using System;
using System.Collections.Generic;
using System.Linq;
using i18n = Flake.MoBa.XpressNetLi.Entities.Resources;
using logme = Flake.MoBa.Log.FlakeLog;
using Flake.MoBa.XpressNetLi.Entities.Interfaces;
using Flake.MoBa.XpressNetLi.Base;
using Flake.MoBa.Base;
using Flake.MoBa.XpressNetLi.Comunication.Answers;
using Flake.MoBa.XpressNetLi.Comunication.Commands;
using Flake.MoBa.XpressNetLi.Comunication;
using Flake.MoBa.Db.DataClasses;

namespace Flake.MoBa.XpressNetLi.Entities.Locomotive
{
    /// <summary>
    /// locomotive class representation of an entity
    /// </summary>
    public class Locomotive : ILiEntity
    {
        /// <summary>
        /// The digital address of the locomotive (0-255)
        /// </summary>
        public int Address
        {
            get
            {
                return _ExtendedAdress.Address;
            }
        }

        /// <summary>
        /// The extendet digital address of teh locomotive (0-9999)
        /// </summary>
        public HiLoAddress ExtendedAddress
        {
            get
            {
                return _ExtendedAdress;
            }
        }

        /// <summary>
        /// Descriptive informations about the locomotive
        /// </summary>
        public LocomotiveDesc Description { get; set; }

        /// <summary>
        /// Create a new instance of a locomotive
        /// </summary>
        /// <param name="address">The digital address of the locomotive (0-255)</param>
        /// <param name="speedSections">How many steps may the speed take</param>
        /// <param name="useExtendedAdress">usage of the extended address (0-9999)</param>
        public Locomotive(int address, Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections speedSections = Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x128, bool useExtendedAdress = true)
        {
            _ExtendedAdress = new HiLoAddress(address);
            _SpeedSections = speedSections;
            _UseExtendedAdress = useExtendedAdress;
            _CurrentDirection = Base.Enums.LocomotiveDirection.LocomotiveDirection.forward;
            _CurrentSpeed = 0;
            _LocomotiveLocked = false;
            _functions = new Dictionary<int, LocomotiveFunction>();
            Description = new LocomotiveDesc();
            MaxSpeedReal = -1;
        }

        /// <summary>
        /// Create a new instance of a locomotive
        /// </summary>
        /// <param name="moBaDbLocomotive">all information of the locomoive object</param>
        public Locomotive(MoBaDbLocomotive moBaDbLocomotive)
            : this(moBaDbLocomotive.Address)
        {
            Description = new LocomotiveDesc() { Name = moBaDbLocomotive.Name, Description = moBaDbLocomotive.Description, };
            MaxSpeedReal = moBaDbLocomotive.MaxSpeedReal;
            foreach (var f in moBaDbLocomotive.GetAllFunctions())
            {
                // TODO add functions!
            }
        }

        /// <summary>
        /// Store the central, to which the locomotive is registered
        /// </summary>
        /// <param name="central">A representation of a central</param>
        public void RegisterCentral(ICentral central)
        {
            try
            {
                _Central = central;
                InitializeLocomotiveFunctionTypes();
                GetLocomotiveInfo();
                IsRegistered = true;
            }
            catch (Exception ex)
            {
                // TODO
                ex.ToString(); //foo
                IsRegistered = false;
            }
        }

        /// <summary>
        /// Stop all movement!
        /// </summary>
        public void EmergencyStop()
        {
            EmergencyBreak();
        }

        /// <summary>
        /// indicated whether the entity is registered to a central or not
        /// </summary>
        public bool IsRegistered { get; private set; }

        /// <summary>
        /// indicates whether the locomotive can react on commands or not
        /// </summary>
        private bool Online { get { return IsRegistered && _Central.Connected; } }

        #region stats and current values (private)

        /// <summary>
        /// Adresses of locomotive (basic and extended)
        /// </summary>
        private HiLoAddress _ExtendedAdress;
        /// <summary>
        /// Use the extended address (information for central)
        /// </summary>
        private bool _UseExtendedAdress;

        /// <summary>
        /// Dictionalry of locomotive functions and their stats
        /// </summary>
        private Dictionary<int, LocomotiveFunction> _functions;

        /// <summary>
        /// digital speed sections of decoder
        /// </summary>
        private Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections _SpeedSections;

        /// <summary>
        /// Central to which the locomotive is registered
        /// </summary>
        private ICentral _Central;

        /// <summary>
        /// Digitalsystem: is locomotive locked by another expressnetcontroller
        /// </summary>
        private bool _LocomotiveLocked;

        /// <summary>
        /// Digitalsystem: current direction of locomotive
        /// </summary>
        private Base.Enums.LocomotiveDirection.LocomotiveDirection _CurrentDirection;

        /// <summary>
        /// Digitalsystem: current speed of locomotive
        /// </summary>
        private int _CurrentSpeed;

        /// <summary>
        /// Real max speed in km/h or mph
        /// </summary>
        public int MaxSpeedReal { get; set; }

        /// <summary>
        /// Read the current locomotive stats from central
        /// </summary>
        private void GetLocomotiveInfo(bool overwrightOwnSettingsByCentralSettings = false)
        {
            SetStatsByLocomotiveInfoAnswer(ReadLocomotiveInfoArray(), overwrightOwnSettingsByCentralSettings);
            SetStatsByLocomotiveInfoAnswer(ReadLocomotiveFunctionsExt());
            SetStatsByLocomotiveInfoAnswer(ReadLocomotiveFunctionTypesLo(), overwrightOwnSettingsByCentralSettings);
            SetStatsByLocomotiveInfoAnswer(ReadLocomotiveFunctionTypesHi(), overwrightOwnSettingsByCentralSettings);
        }

        /// <summary>
        /// Set the locomotive stats by a stats-answer-byte array
        /// </summary>
        /// <param name="answer">answer of getstats command</param>
        /// <param name="overwrightSpeedsections">if true, the speedsection settings will be overwritten (not recommended for init)</param>
        private void SetStatsByLocomotiveInfoAnswer(CommonLocomotiveInfo answer, bool overwrightSpeedsections = false)
        {
            if (answer != null && answer.ByteArray.Length > 0)
            {
                //goodcase
                _LocomotiveLocked = answer.LocomotiveLocked;

                if (overwrightSpeedsections) _SpeedSections = answer.ReadSpeedSections;

                // speed ans direction
                _CurrentDirection = answer.Direction;
                _CurrentSpeed = answer.GetCurrentSpeed(_SpeedSections);

                // functions
                foreach (int i in answer.Functions.Keys)
                {
                    if (_functions.Keys.Contains(i) && _functions[i].Active != answer.Functions[i]) _functions[i].Toggle();
                }
            }
            else
            {
                logme.Log(i18n.ErrorMessages.AnswerEmpty, logme.LogLevel.error, answer == null ? null : answer.ByteArray);
            }
        }

        /// <summary>
        /// Set the locomotive function states of F13 to F28
        /// </summary>
        /// <param name="answer">answer of getstates command</param>
        private void SetStatsByLocomotiveInfoAnswer(LocomotiveFunctionStateExt answer)
        {
            if (answer != null && answer.ByteArray.Length > 0)
            {
                // functions
                foreach (int i in answer.Functions.Keys)
                {
                    if (_functions.Keys.Contains(i) && _functions[i].Active != answer.Functions[i]) _functions[i].Toggle();
                }
            }
            else
            {
                logme.Log(i18n.ErrorMessages.AnswerEmpty, logme.LogLevel.error, answer.ByteArray);
            }
        }

        /// <summary>
        /// Set the locomotive function types of F0 to F12
        /// </summary>
        /// <param name="answer">answer of getstates command</param>
        /// <param name="overwrightFunctionTypes">if true, the function type settings will be overwritten (not recommended for init)</param>
        private void SetStatsByLocomotiveInfoAnswer(LocomotiveFunctionTypeLo answer, bool overwrightFunctionTypes = false)
        {
            if (overwrightFunctionTypes)
            {
                if (answer != null && answer.ByteArray.Length > 0)
                {
                    // functions
                    foreach (int i in answer.Functions.Keys)
                    {
                        if (_functions.Keys.Contains(i))
                        {
                            _functions[i].Type = (answer.Functions[i]) ? (LocomotiveFunctionType.tapping) : (LocomotiveFunctionType.switching);
                        }
                    }
                }
                else
                {
                    logme.Log(i18n.ErrorMessages.AnswerEmpty, logme.LogLevel.error);
                }
            }
        }

        /// <summary>
        /// Set the locomotive function types of F13 to F28
        /// </summary>
        /// <param name="answer">answer of getstates command</param>
        /// <param name="overwrightFunctionTypes">if true, the function type settings will be overwritten (not recommended for init)</param>
        private void SetStatsByLocomotiveInfoAnswer(LocomotiveFunctionTypeHi answer, bool overwrightFunctionTypes = false)
        {
            if (overwrightFunctionTypes)
            {
                if (answer != null && answer.ByteArray.Length > 0)
                {
                    // functions
                    foreach (int i in answer.Functions.Keys)
                    {
                        if (_functions.Keys.Contains(i))
                        {
                            _functions[i].Type = (answer.Functions[i]) ? (LocomotiveFunctionType.tapping) : (LocomotiveFunctionType.switching);
                        }
                    }
                }
                else
                {
                    logme.Log(i18n.ErrorMessages.AnswerEmpty, logme.LogLevel.error, answer.ByteArray);
                }
            }
        }

        #endregion stats and current values (private)

        #region speed and direction management

        // functions for external speed management

        /// <summary>
        /// Returns the current speed of locomotive in steps
        /// </summary>
        public int CurrentSpeed { get { return _CurrentSpeed; } }

        /// <summary>
        /// Returns the current real speed of the locomotive
        /// </summary>
        public int CurrentSpeedReal { get { return (int)(Math.Round((decimal)MaxSpeedReal / (decimal)_SpeedSections * _CurrentSpeed)); } }

        /// <summary>
        /// Gets or sets the direction of the locomotive
        /// </summary>
        public Base.Enums.LocomotiveDirection.LocomotiveDirection Direction
        {
            get
            {
                return _CurrentDirection;
            }
            set
            {
                _CurrentDirection = value;
                Drive();
            }
        }

        /// <summary>
        /// Switches the direction of the locomotive
        /// </summary>
        public void ToggleDirection()
        {
            switch (_CurrentDirection)
            {
                case Base.Enums.LocomotiveDirection.LocomotiveDirection.backward:
                    _CurrentDirection = Base.Enums.LocomotiveDirection.LocomotiveDirection.forward;
                    break;
                case Base.Enums.LocomotiveDirection.LocomotiveDirection.forward:
                    _CurrentDirection = Base.Enums.LocomotiveDirection.LocomotiveDirection.backward;
                    break;
                default:
                    logme.Log(i18n.ComunicationMessages.NotSupportetDirection, logme.LogLevel.error);
                    break;
            }
            Drive();
        }

        /// <summary>
        /// Sets a new speed to the locomotive
        /// </summary>
        /// <param name="value">Numeric value depending on the locomotive's speed steps</param>
        /// <param name="direction">Sets the direction of the locomotive</param>
        /// <param name="useRealSpeed">if a max speed in mph or kmh is set use this key to set a real speed instead of bitvalue</param>
        /// <remarks>set the direction to 'none' to use the active direction</remarks>
        public void SetSpeedAndDirection(int value, Base.Enums.LocomotiveDirection.LocomotiveDirection direction = Base.Enums.LocomotiveDirection.LocomotiveDirection.none, bool useRealSpeed = false)
        {
            // real speed
            if (useRealSpeed)
            {
                if (MaxSpeedReal > -1)
                {
                    decimal temp = ((decimal)_SpeedSections / (decimal)MaxSpeedReal) * value;
                    value = (int)Math.Round(temp, 0);
                }
                else
                {
                    logme.Log(string.Format(i18n.ErrorMessages.MaxSpeedNotSet, value.ToString()), logme.LogLevel.warning);
                }
            }

            // speed component
            int speedvalue = CalculateSpeedValue(value);
            switch (_SpeedSections)
            {
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x128:
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14:
                    if (speedvalue == 1) speedvalue++;
                    if (speedvalue > 126) speedvalue = 126;
                    _CurrentSpeed = speedvalue;
                    break;
                default:
                    _CurrentSpeed = speedvalue;
                    break;
            }

            // direction component
            if (direction != Base.Enums.LocomotiveDirection.LocomotiveDirection.none) _CurrentDirection = direction;

            Drive();
        }

        /// <summary>
        /// Breaks down the locomotive to zero speed
        /// </summary>
        public void BreakToStop()
        {
            _CurrentSpeed = 0;
            Drive();
        }

        /// <summary>
        /// stops the locomotive immediately
        /// </summary>
        public void EmergencyBreak()
        {
            _CurrentSpeed = 1;
            Drive();
        }

        /// <summary>
        /// Cuts big speed values
        /// </summary>
        /// <param name="value">input speed value</param>
        /// <returns>Returns a speed value smaler or equal the maximum value set by the locomotive speed steps</returns>
        private int CalculateSpeedValue(int value)
        {
            if (value < 0)
            {
                logme.Log(i18n.ComunicationMessages.LocomotiveSpeedTooLowForCalc, logme.LogLevel.warning);
                return 0;
            }
            if (value > (int)_SpeedSections)
            {
                logme.Log(i18n.ComunicationMessages.LocomotiveSpeedTooLowForCalc, logme.LogLevel.warning);
                return (int)_SpeedSections;
            }
            return value;
        }

        /// <summary>
        /// calculates the speed and direction byte from current values
        /// </summary>
        /// <returns>Returnes a byte with coding fpr speed and direction</returns>
        private byte GetSpeedAndDirection()
        {
            string directioncomponent = (_CurrentDirection == Base.Enums.LocomotiveDirection.LocomotiveDirection.backward) ? ("0") : ("1");
            string tmp = string.Empty;
            switch (_SpeedSections)
            {
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x14:
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "000" + Base.FlakeHelper.ConvertDecimalToBinary(_CurrentSpeed, 4)));
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x27:
                    tmp = Base.FlakeHelper.ConvertDecimalToBinary(_CurrentSpeed + 3, 5);
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "000" + tmp.Substring(4, 1) + tmp.Substring(0, 4)));
                case Base.Enums.LocomotiveSpeedSections.LocomotiveSpeedSections.x28:
                    tmp = Base.FlakeHelper.ConvertDecimalToBinary(_CurrentSpeed + 3, 5);
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + "000" + tmp.Substring(4, 1) + tmp.Substring(0, 4)));
                default:
                    return (byte)(Base.FlakeHelper.ConvertBinaryStringToDecimal(directioncomponent + Base.FlakeHelper.ConvertDecimalToBinary(_CurrentSpeed, 7)));
            }
        }

        ///// <summary>
        ///// gets a int value from a speed bitarray of a info msg by central
        ///// </summary>
        ///// <param name="bitArray">info msg by central</param>
        ///// <returns>returns teh decimal representation of given array</returns>
        //private int DecodeSpeedBitArray(string bitArray)
        //{
        //    int ret = 0;
        //    int temp = 0;
        //    switch (_SpeedSections)
        //    {
        //        case LIBase.LocomotiveSpeedSections.x14:
        //            temp = LIHelper.ConvertBinaryStringToDecimal(bitArray);
        //            if (temp == 1) ret = 0;
        //            ret = temp;
        //            break;
        //        case LIBase.LocomotiveSpeedSections.x27:
        //            temp = LIHelper.ConvertBinaryStringToDecimal(LIHelper.ShiftArray(bitArray));
        //            if (temp == 1 || temp == 2 || temp == 3) ret = 0;
        //            ret = temp;
        //            break;
        //        case LIBase.LocomotiveSpeedSections.x28:
        //            temp = LIHelper.ConvertBinaryStringToDecimal(LIHelper.ShiftArray(bitArray));
        //            if (temp == 1 || temp == 2 || temp == 3) ret = 0;
        //            ret = temp;
        //            break;
        //        case LIBase.LocomotiveSpeedSections.x128:
        //            temp = LIHelper.ConvertBinaryStringToDecimal(bitArray);
        //            if (temp == 1) ret = 0;
        //            ret = temp;
        //            break;
        //    }
        //    return ret;
        //}

        #endregion speed and direction management

        #region LI commands

        // section for commands to the central

        /// <summary>
        /// Make the locomotive drive
        /// </summary>
        /// <remarks>uses the current speed and direction settings</remarks>
        private void Drive()
        {
            if (Online)
            {
                LocomotiveDrive cmd = new LocomotiveDrive(_ExtendedAdress, _CurrentSpeed, (_CurrentDirection == Base.Enums.LocomotiveDirection.LocomotiveDirection.forward), _SpeedSections);
                _Central.QueueNewCommand(new LiCommandAndAnswer(cmd));
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
            }
        }

        /// <summary>
        /// Gets information about locomotives stats from central
        /// </summary>
        private CommonLocomotiveInfo ReadLocomotiveInfoArray()
        {
            if (Online)
            {
                GetLocomotiveInfo cmd = new GetLocomotiveInfo(_ExtendedAdress);
                LiCommandAndAnswer commandAndAnswer = new LiCommandAndAnswer(cmd);
                _Central.QueueNewCommand(commandAndAnswer);
                DateTime requestDeadline = DateTime.Now.AddSeconds(_Central.Config.Data.TimeoutForLIResponse_s * 10000000);
                while (commandAndAnswer.Answer == null && DateTime.Now < requestDeadline)
                {
                    // wait until data arrives or timeout
                }
                return (CommonLocomotiveInfo)commandAndAnswer.Answer;
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
                return null;
            }
        }

        /// <summary>
        /// Gets information function state F13 to F28
        /// </summary>
        private LocomotiveFunctionStateExt ReadLocomotiveFunctionsExt()
        {
            if (Online)
            {
                GetLocomotiveFunctionState cmd = new GetLocomotiveFunctionState(_ExtendedAdress);
                LiCommandAndAnswer commandAndAnswer = new LiCommandAndAnswer(cmd);
                _Central.QueueNewCommand(commandAndAnswer);
                DateTime requestDeadline = DateTime.Now.AddSeconds(_Central.Config.Data.TimeoutForLIResponse_s * 10000000);
                while (commandAndAnswer.Answer == null && DateTime.Now < requestDeadline)
                {
                    // wait until data arrives or timeout
                }
                return (LocomotiveFunctionStateExt)commandAndAnswer.Answer;
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
                return null;
            }
        }

        /// <summary>
        /// Gets information function type F0 to F12
        /// </summary>
        private LocomotiveFunctionTypeLo ReadLocomotiveFunctionTypesLo()
        {
            if (Online)
            {
                GetLocomotiveFunctionTypesLo cmd = new GetLocomotiveFunctionTypesLo(_ExtendedAdress);
                LiCommandAndAnswer commandAndAnswer = new LiCommandAndAnswer(cmd);
                _Central.QueueNewCommand(commandAndAnswer);
                DateTime requestDeadline = DateTime.Now.AddSeconds(_Central.Config.Data.TimeoutForLIResponse_s * 10000000);
                while (commandAndAnswer.Answer == null && DateTime.Now < requestDeadline)
                {
                    // wait until data arrives or timeout
                }
                return (LocomotiveFunctionTypeLo)commandAndAnswer.Answer;
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
                return null;
            }
        }

        /// <summary>
        /// Gets information function type F13 to F28
        /// </summary>
        private LocomotiveFunctionTypeHi ReadLocomotiveFunctionTypesHi()
        {
            if (Online)
            {
                GetLocomotiveFunctionTypesHi cmd = new GetLocomotiveFunctionTypesHi(_ExtendedAdress);
                LiCommandAndAnswer commandAndAnswer = new LiCommandAndAnswer(cmd);
                _Central.QueueNewCommand(commandAndAnswer);
                DateTime requestDeadline = DateTime.Now.AddSeconds(_Central.Config.Data.TimeoutForLIResponse_s * 10000000);
                while (commandAndAnswer.Answer == null && DateTime.Now < requestDeadline)
                {
                    // wait until data arrives or timeout
                }
                return (LocomotiveFunctionTypeHi)commandAndAnswer.Answer;
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
                return null;
            }
        }

        /// <summary>
        /// Gets information function type F13 to F28
        /// </summary>
        private LocomotiveFunctionTypeHi ReadLocomotiveFunctionTypeshi()
        {
            if (Online)
            {
                GetLocomotiveFunctionTypesHi cmd = new GetLocomotiveFunctionTypesHi(_ExtendedAdress);
                LiCommandAndAnswer commandAndAnswer = new LiCommandAndAnswer(cmd);
                _Central.QueueNewCommand(commandAndAnswer);
                DateTime requestDeadline = DateTime.Now.AddSeconds(_Central.Config.Data.TimeoutForLIResponse_s * 10000000);
                while (commandAndAnswer.Answer == null && DateTime.Now < requestDeadline)
                {
                    // wait until data arrives or timeout
                }
                return (LocomotiveFunctionTypeHi)commandAndAnswer.Answer;
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
                return null;
            }
        }

        /// <summary>
        /// Sets a function of a locomotive to a value
        /// </summary>
        /// <param name="functionNumber">f-number</param>
        /// <param name="setFunction">set (1) or unset (0) the function</param>
        private void SetLocomotiveFunction(int functionNumber, bool setFunction)
        {
            if (Online)
            {
                var tmp = new Dictionary<int, bool>();
                _functions.Foreach(a => tmp.Add(a.Key, a.Value.Active));
                SetLocomotiveFunction cmd = new SetLocomotiveFunction(_ExtendedAdress, functionNumber, setFunction, tmp);
                _Central.QueueNewCommand(new LiCommandAndAnswer(cmd));
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
            }
        }

        /// <summary>
        /// Set the functiontypes of all functions in central
        /// </summary>
        private void InitializeLocomotiveFunctionTypes()
        {
            if (Online)
            {
                var tmp = new Dictionary<int, bool>();
                _functions.Foreach(a => tmp.Add(a.Key, a.Value.Type == LocomotiveFunctionType.tapping));
                SetLocomotiveFunctionType cmd = new SetLocomotiveFunctionType(_ExtendedAdress, 0, _functions[0].Type == LocomotiveFunctionType.tapping, tmp);
                _Central.QueueNewCommand(new LiCommandAndAnswer(cmd));
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
            }
        }

        #endregion LI commands

        #region locomotive functions

        /// <summary>
        /// Adds a new function to the locomotive
        /// </summary>
        /// <param name="function">the function like light or horn, etc.</param>
        public void AddFunction(LocomotiveFunction function)
        {
            if (_functions.ContainsKey(function.FNumber))
            {
                _functions[function.FNumber] = function;
            }
            else
            {
                _functions.Add(function.FNumber, function);
            }
        }

        /// <summary>
        /// Retrieve a list of available locomotive functions
        /// </summary>
        public Dictionary<int, string> ListLocomotiveFunktions
        {
            get
            {
                Dictionary<int, string> ret = new Dictionary<int, string>();
                foreach (int f in _functions.Keys)
                {
                    ret.Add(f, _functions[f].Name);
                }
                return ret;
            }
        }

        /// <summary>
        /// Switches a locomotive function
        /// </summary>
        /// <param name="functionNumber">f-number</param>
        public void ToggleFunction(int functionNumber)
        {
            if (Online)
            {
                if (_functions.Keys.Contains(functionNumber))
                {
                    SetLocomotiveFunction(functionNumber, !_functions[functionNumber].Active);
                    _functions[functionNumber].Toggle();
                }
                else
                {
                    logme.Log(i18n.ErrorMessages.WarningNotRegisteredLocoFunction, logme.LogLevel.warning);
                }
            }
            else
            {
                logme.Log(i18n.ErrorMessages.WarningNotOnline, logme.LogLevel.warning);
            }
        }

        #endregion locomotive functions
    }
}