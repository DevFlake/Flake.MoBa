using System;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;

namespace Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveDirection
{
    public class LocomotiveDirectionExtended : EnumExtendedBase<LocomotiveDirection>
    {
        public LocomotiveDirectionExtended(LocomotiveDirection Value) : base(Value) { }

        public LocomotiveDirectionExtended(String Value) : base(Value) { }

        public LocomotiveDirectionExtended(int Value) : base(Value) { }

        protected override LocomotiveDirection NoneValue() { return LocomotiveDirection.none; }

        protected override string Caption()
        {
            switch (_Value)
            {
                case LocomotiveDirection.forward: return i18n.LocomotiveDirection.forward;
                case LocomotiveDirection.backward: return i18n.LocomotiveDirection.backward;
                case LocomotiveDirection.none: return i18n.LocomotiveDirection.none;
                default: return _Value.ToString();
            }
        }

        public static System.Collections.ArrayList GetList()
        {
            EnumExtendedBase<LocomotiveDirection>.MyList a = new EnumExtendedBase<LocomotiveDirection>.MyList();
            a.Add(new LocomotiveDirectionExtended(LocomotiveDirection.none));
            a.Add(new LocomotiveDirectionExtended(LocomotiveDirection.forward));
            a.Add(new LocomotiveDirectionExtended(LocomotiveDirection.backward));
            return (a);
        }
    }
}