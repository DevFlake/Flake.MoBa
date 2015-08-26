using System;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;

namespace Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveSpeedSections
{
    public class LocomotiveSpeedSectionsExtended : EnumExtendedBase<LocomotiveSpeedSections>
    {
        public LocomotiveSpeedSectionsExtended(LocomotiveSpeedSections Value) : base(Value) { }

        public LocomotiveSpeedSectionsExtended(String Value) : base(Value) { }

        public LocomotiveSpeedSectionsExtended(int Value) : base(Value) { }

        protected override LocomotiveSpeedSections NoneValue() { return LocomotiveSpeedSections.x128; }

        protected override string Caption()
        {
            switch (_Value)
            {
                case LocomotiveSpeedSections.x14: return i18n.LocomotiveSpeedSections.x14;
                case LocomotiveSpeedSections.x27: return i18n.LocomotiveSpeedSections.x27;
                case LocomotiveSpeedSections.x28: return i18n.LocomotiveSpeedSections.x28;
                case LocomotiveSpeedSections.x128: return i18n.LocomotiveSpeedSections.x128;
                default: return _Value.ToString();
            }
        }

        public static System.Collections.ArrayList GetList()
        {
            EnumExtendedBase<LocomotiveSpeedSections>.MyList a = new EnumExtendedBase<LocomotiveSpeedSections>.MyList();
            a.Add(new LocomotiveSpeedSectionsExtended(LocomotiveSpeedSections.x14));
            a.Add(new LocomotiveSpeedSectionsExtended(LocomotiveSpeedSections.x27));
            a.Add(new LocomotiveSpeedSectionsExtended(LocomotiveSpeedSections.x28));
            a.Add(new LocomotiveSpeedSectionsExtended(LocomotiveSpeedSections.x128));
            return (a);
        }
    }
}