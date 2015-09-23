
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Entities.Interfaces
{
    /// <summary>
    /// Interface for entity classes
    /// </summary>
    public interface ILiEntity
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
        void RegisterCentral(ICentral central);

        /// <summary>
        /// Stop all movement
        /// </summary>
        void EmergencyStop();

        /// <summary>
        /// indicated whether the entity is registered to a central or not
        /// </summary>
        bool IsRegistered { get; }
    }
}