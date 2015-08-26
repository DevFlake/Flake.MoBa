using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlakeTrain.FlakeController.Locomotive
{
    /// <summary>
    /// Representation of function type of locomotive
    /// </summary>
    public enum LocomotiveFunctionType
    {
        /// <summary>
        /// switching function like light
        /// </summary>
        switching = 0,
        /// <summary>
        /// tapping function like a horn
        /// </summary>
        tapping = 1,
    }
}