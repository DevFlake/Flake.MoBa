using System;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;

namespace Flake.MoBa.XpressNetLi.Base.Enums.LocoFunctionRefreshMode
{
    public class LocoFunctionRefreshModeExtended : EnumExtendedBase<LocoFunctionRefreshMode>
    {
        public LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode Value) : base(Value) { }

        public LocoFunctionRefreshModeExtended(String Value) : base(Value) { }

        public LocoFunctionRefreshModeExtended(int Value) : base(Value) { }

        protected override LocoFunctionRefreshMode NoneValue() { return LocoFunctionRefreshMode.f0tof4; }

        protected override string Caption()
        {
            switch (_Value)
            {
                case LocoFunctionRefreshMode.f0tof4: return i18n.LocoFunctionRefreshMode.LocoFunctionRefreshMode_f0tof4;
                case LocoFunctionRefreshMode.f0tof8: return i18n.LocoFunctionRefreshMode.LocoFunctionRefreshMode_f0tof8;
                case LocoFunctionRefreshMode.f0tof12: return i18n.LocoFunctionRefreshMode.LocoFunctionRefreshMode_f0tof12;
                case LocoFunctionRefreshMode.f0tof20: return i18n.LocoFunctionRefreshMode.LocoFunctionRefreshMode_f0tof20;
                case LocoFunctionRefreshMode.f0tof28: return i18n.LocoFunctionRefreshMode.LocoFunctionRefreshMode_f0tof28;
                default: return _Value.ToString();
            }
        }

        public static System.Collections.ArrayList GetList()
        {
            EnumExtendedBase<LocoFunctionRefreshMode>.MyList a = new EnumExtendedBase<LocoFunctionRefreshMode>.MyList();
            a.Add(new LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode.f0tof4));
            a.Add(new LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode.f0tof8));
            a.Add(new LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode.f0tof12));
            a.Add(new LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode.f0tof20));
            a.Add(new LocoFunctionRefreshModeExtended(LocoFunctionRefreshMode.f0tof28));
            return (a);
        }
    }
}