using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i18n = Flake.MoBa.Db.DataClasses.Resources.MoBaDbLocomotive;

namespace Flake.MoBa.Db.DataClasses.Ctl
{
    public class MoBaDbLocomotive
    {
        /// <summary>
        /// the digital address of the locomotve
        /// </summary>
        public int Address { get; set; } = -1; // TODO make a CV-class or decoder class
        // TODO missing speedsections of decoder

        /// <summary>
        /// the name of the locomotive
        /// </summary>
        public string Name { get; set; } = i18n.NewLoco;

        /// <summary>
        /// the description of the locomotive
        /// </summary>
        public string Description { get; set; } = i18n.NewLocoDesc;

        /// <summary>
        /// unique identifier of locomotive
        /// </summary>
        public int LocomotiveNid { get; set; }

        /// <summary>
        /// Real maximal speed in km/h or mph
        /// </summary>
        public int MaxSpeedReal { get; set; } = -1;

        /// <summary>
        /// the functions of the locomotive
        /// </summary>
        /// <remarks>Identifier as string from function class</remarks>
        private SortedList<string, MoBaDbLocomotiveFunction> _functions = new SortedList<string, MoBaDbLocomotiveFunction>();  

        /// <summary>
        /// returns all functions of the locomotive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MoBaDbLocomotiveFunction> GetAllFunctions()  
        {
            return _functions.Values;
        }

        /// <summary>
        /// creates a new instance of MoBaDbLocomotive
        /// </summary>
        public MoBaDbLocomotive()
        {

        }

        public void AddFunction(MoBaDbLocomotiveFunction function)
        {
            if (!_functions.ContainsValue(function))
            {
                _functions.Add(function.Identifier, function);
            }
        }

        
    }
}
