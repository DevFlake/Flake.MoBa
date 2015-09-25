using Flake.MoBa.Base;
using Flake.MoBa.Db.DataClasses.Ctl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Db.Dal.Ctl
{
    public class HandleLocomotives
    {
        private IEnumerable<MoBaDbLocomotive> GetLocomotives(IEnumerable<int> locomotiveNids)
        {
            var ret = new List<MoBaDbLocomotive>();

            using (var db = DbBase.GetConnection())
            {
                if (locomotiveNids == null || locomotiveNids.Count() == 0) locomotiveNids = db.MoBaDb.Locomotives.Select(a => a.LocomotiveNid);
                foreach (var loco in db.MoBaDb.Locomotives.Where(a => locomotiveNids.Contains(a.LocomotiveNid)))
                {
                    var tmpLoco = new MoBaDbLocomotive() { Name = loco.Name, LocomotiveNid = loco.LocomotiveNid, Address = loco.DigitalAddress, Description = loco.Description, MaxSpeedReal = loco.LocomotiveDataSheets.MaxSpeed, };
                    foreach(var fct in db.MoBaDb.LocomotiveFunctions.Where(a=>a.LocomotiveNid == loco.LocomotiveNid))
                    {
                        var tmpFunc = new MoBaDbLocomotiveFunction() { LocomotiveFunctionNid = fct.LocomotiveFunctionNid, Name = fct.Name, Description = fct.Description, FNumber = fct.FNumber, FunctionIsTappable = fct.Tappable, };
                        tmpLoco.AddFunction(tmpFunc);
                    }
                    ret.Add(tmpLoco);
                }
            }
            return ret;
        }

        public IEnumerable<MoBaDbLocomotive> GetAllLocomotives()
        {
            return GetLocomotives(null);
        }
        public MoBaDbLocomotive GetLocomotive(int locomotiveNid)
        {
            return GetLocomotives(locomotiveNid.AsEnumerable()).FirstOrDefault();
        }

    }
}
