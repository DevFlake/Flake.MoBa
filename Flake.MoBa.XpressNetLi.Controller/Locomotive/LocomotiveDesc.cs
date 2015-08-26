using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlakeTrain.FlakeController.Locomotive
{
    public class LocomotiveDesc
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public LocomotiveDesc()
        {
            Name = "NewLoco";
            Description = string.Empty;
        }
    }
}