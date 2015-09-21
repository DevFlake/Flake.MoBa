using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flake.MoBa.Db.DataClasses;

namespace Flake.MoBa.Db.Dal
{
    public class HandleArtikel
    {
        public void StoreArtikel(MoBaDbArtikel artikel)
        {
            using (var db =  DbBase.GetConnection())
            {
                foreach (var schlagwort in artikel.Schlagworte)
                {
                    db.MoBaDb.InsertSchlagwortZuArtikel(artikel.ArtikelNid, schlagwort);
                }
                var linkHandler = new HandleLinkItems();
                linkHandler.StoreLinkItems(artikel.Links);
                //foreach (var l in hersteller.Links)
                //{
                //    _database.InsertLinkZuHersteller(hersteller.HerstellerNid,l.,l.Bezeichnung);
                //}
            }
        }
    }
}
