using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i18n = Flake.MoBa.Db.DataClasses.Resources.MoBaDbLocomotive;

namespace Flake.MoBa.Db.DataClasses
{
    public class MoBaDbLocomotiveFunction : IComparable
    {
        /// <summary>
        /// functionname like 'light'
        /// </summary>
        public string Name { get; set; } = i18n.NewLocoFunctionName;

        /// <summary>
        /// Describes the function (optional)
        /// </summary>
        public string Description { get; set; } = i18n.NewLocoFunctionDesc;

        /// <summary>
        /// Funcion number f0 to fx
        /// </summary>
        public int FNumber { get; private set; } = -1;

        /// <summary>
        /// tapp function of switch function
        /// </summary>
        /// <example>tapp a horn or switch light</example>
        public bool FunctionIsTappable { get; set; } = false;

        public string Identifier { get { return string.Format("{0}|{1}|{2}", FNumber.ToString("00"), Name, FunctionIsTappable ? "tap" : "switch"); } }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MoBaDbLocomotiveFunction tmp = obj as MoBaDbLocomotiveFunction;
            if (tmp != null)
            {
                return FNumber.CompareTo(tmp.FNumber);
            }
            else
            {
                return -1;
            }
        }
    }
}
