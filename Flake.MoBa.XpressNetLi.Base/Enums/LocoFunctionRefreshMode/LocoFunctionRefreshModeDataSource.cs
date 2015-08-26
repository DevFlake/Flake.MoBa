namespace Flake.MoBa.XpressNetLi.Base.Enums.LocoFunctionRefreshMode
{
    public class LocoFunctionRefreshModeDataSource : EnumDataSourceBase
    {
        public LocoFunctionRefreshModeDataSource() : base() { }

        public LocoFunctionRefreshModeDataSource(System.ComponentModel.IContainer container) : base(container) { }

        protected override System.Collections.IList GetListCore()
        {
            return LocoFunctionRefreshModeExtended.GetList();
        }
    }
}