namespace Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveSpeedSections
{
    public class LocomotiveSpeedSectionsDataSource : EnumDataSourceBase
    {
        public LocomotiveSpeedSectionsDataSource() : base() { }

        public LocomotiveSpeedSectionsDataSource(System.ComponentModel.IContainer container) : base(container) { }

        protected override System.Collections.IList GetListCore()
        {
            return LocomotiveSpeedSectionsExtended.GetList();
        }
    }
}