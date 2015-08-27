using i18n = Flake.MoBa.XpressNetLi.Entities.Resources;

namespace Flake.MoBa.XpressNetLi.Entities.Locomotive
{
    /// <summary>
    /// Class for a locomotive funktion
    /// </summary>
    public class LocomotiveFunction
    {
        /// <summary>
        /// functionname like 'light'
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Describes the function (optional)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Funcion number f0 to fx
        /// </summary>
        public int FNumber { get; private set; }

        /// <summary>
        /// permanent function (like light)
        /// </summary>
        public LocomotiveFunctionType Type { get; set; }

        /// <summary>
        /// Status of function (on or off)
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// represents the activation state as bit
        /// </summary>
        public string ActiveBit
        {
            get
            {
                return ((Active) ? ("1") : ("0"));
            }
        }

        /// <summary>
        /// Creates a new locomotive function
        /// </summary>
        /// <param name="fNumber">function number</param>
        /// <remarks>the name of the function will be set automatically from number</remarks>
        public LocomotiveFunction(int fNumber)
        {
            Name = string.Format(i18n.FlakeLocomotive.Function, fNumber.ToString());
            Description = string.Format(i18n.FlakeLocomotive.EmptyDescription, fNumber.ToString());
            Type = LocomotiveFunctionType.switching;
            FNumber = fNumber;
            Active = false;
        }

        /// <summary>
        /// Createa a new locomotive function
        /// </summary>
        /// <param name="fNumber">function number</param>
        /// <param name="name">name of function</param>
        /// <param name="description">description of function</param>
        /// <param name="type">is this a permanent/switching function (light)?</param>
        public LocomotiveFunction(int fNumber, string name, string description, LocomotiveFunctionType type)
        {
            Name = name;
            Description = description;
            FNumber = fNumber;
            Type = type;
            Active = false;
        }

        /// <summary>
        /// Toggles the function state
        /// </summary>
        /// <remarks>no function with non permantent functions</remarks>
        public void Toggle()
        {
            if (Type == LocomotiveFunctionType.switching) Active = !Active;
        }
    }
}