namespace Flake.MoBa.XpressNetLi.Base.Enums.CentralStartMode
{
    public class CentralStartModeDataSource : EnumDataSourceBase
    {
        public CentralStartModeDataSource() : base() { }

        public CentralStartModeDataSource(System.ComponentModel.IContainer container) : base(container) { }

        protected override System.Collections.IList GetListCore()
        {
            return CentralStartModeExtended.GetList();
        }
    }
}