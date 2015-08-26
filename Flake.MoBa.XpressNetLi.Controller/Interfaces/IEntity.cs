using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace FlakeTrain.FlakeController
{
    /// <summary>
    /// Interface for entity classes
    /// </summary>
    public interface ILIEntity
    {
        /// <summary>
        /// Address of Entity
        /// </summary>
        int Address { get; }

        /// <summary>
        /// Extended address of entity
        /// </summary>
        HiLoAddress ExtendedAddress { get; } //Question: are ExtAddresses really used by every entity?

        /// <summary>
        /// Funcition for registering a central backwords to an entity
        /// </summary>
        /// <param name="central"></param>
        void RegisterCentral(Central central);

        /// <summary>
        /// Stop all movement
        /// </summary>
        void EmergencyStop();
    }
}