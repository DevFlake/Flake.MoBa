namespace Flake.MoBa.XpressNetLi.Entities.Locomotive
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