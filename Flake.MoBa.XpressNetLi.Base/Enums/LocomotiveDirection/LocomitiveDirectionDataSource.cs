namespace Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveDirection
{
    public class LocomotiveDirectionDataSource : EnumDataSourceBase
    {
        public LocomotiveDirectionDataSource() : base() { }

        public LocomotiveDirectionDataSource(System.ComponentModel.IContainer container) : base(container) { }

        protected override System.Collections.IList GetListCore()
        {
            return LocomotiveDirectionExtended.GetList();
        }
    }
}