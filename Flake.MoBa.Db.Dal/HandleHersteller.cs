using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flake.MoBa.Db.DataClasses;

namespace Flake.MoBa.Db.Dal
{
    public class HandleHersteller
    {
        public void StoreHersteller(MoBaDbHersteller hersteller)
        {
            using (var db =  DbBase.GetConnection())
            {
                foreach (var schlagwort in hersteller.Schlagworte)
                {
                    db.MoBaDb.InsertSchlagwortZuHersteller(hersteller.HerstellerNid, schlagwort);
                }



                var linkHandler = new HandleLinkItems();
                linkHandler.StoreLinkItems(hersteller.Links);
                
                //foreach (var l in hersteller.Links)
                //{
                //    db.MoBaDb.InsertLinkZuHersteller(hersteller.HerstellerNid,l.Link.Url, l.Bezeichnung);
                //}
            }
        }
    }
}
