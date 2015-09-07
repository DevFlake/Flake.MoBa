
using Flake.MoBa.XpressNetLi;
using Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveSpeedSections;
using Flake.MoBa.XpressNetLi.Base.Enums.LocomotiveDirection;
using Flake.MoBa.XpressNetLi.Entities.Locomotive;

namespace Flake.MoBa.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var c = new Central())
            {
                var l = new Locomotive(32, LocomotiveSpeedSections.x128);
                l.AddFunction(new LocomotiveFunction(0, "Light", "turn on the lights", LocomotiveFunctionType.switching));
                l.ToggleFunction(0);
                l.SetSpeedAndDirection(30, LocomotiveDirection.forward);
                System.Threading.Thread.Sleep(2000);
                l.BreakToStop();
                l.ToggleFunction(0);
            }
        }
    }
}
