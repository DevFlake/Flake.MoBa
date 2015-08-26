using System;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;

namespace Flake.MoBa.XpressNetLi.Base.Enums.CentralStartMode
{
    public class CentralStartModeExtended : EnumExtendedBase<CentralStartMode>
    {
        public CentralStartModeExtended(CentralStartMode Value) : base(Value) { }

        public CentralStartModeExtended(String Value) : base(Value) { }

        public CentralStartModeExtended(int Value) : base(Value) { }

        protected override CentralStartMode NoneValue() { return CentralStartMode.man; }

        protected override string Caption()
        {
            switch (_Value)
            {
                case CentralStartMode.auto: return i18n.CentralStartMode.auto;
                case CentralStartMode.man: return i18n.CentralStartMode.man;
                default: return _Value.ToString();
            }
        }

        public static System.Collections.ArrayList GetList()
        {
            EnumExtendedBase<CentralStartMode>.MyList a = new EnumExtendedBase<CentralStartMode>.MyList();
            a.Add(new CentralStartModeExtended(CentralStartMode.auto));
            a.Add(new CentralStartModeExtended(CentralStartMode.man));
            return (a);
        }
    }
}