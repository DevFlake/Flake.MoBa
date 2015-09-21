using Flake.MoBa.Db.Dal;
using Flake.MoBa.Db.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Db.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var dal = new HandleArtikel();

            MoBaDbArtikel artikel = new MoBaDbArtikel(1, "dummy");
            artikel.AddSchlagwort("test3");
            artikel.AddSchlagwort("test4");
            artikel.AddLinkEntry("funnyworld.de");
            artikel.AddOrUpdateLinkEntry(9, "neue Befunnyworld", "neue Besch2", "ordnungsM2");
            dal.StoreArtikel(artikel);

            var dal2 = new HandleHersteller();

            MoBaDbHersteller hersteller = new MoBaDbHersteller(1, "dummy");
            hersteller.AddSchlagwort("test3");
            //hersteller.AddSchlagwort("test4");
            //var x = new MobaDbLinkItem(new MobaDbLink( "URLTEST", 1, LinkItemTypes.LinkItemType.herstellerLinkItem);
            hersteller.AddLinkEntry("www.dummy.net", "AllgBez", "Bez" , "AllgBesch" , "Besch", "OM");
            hersteller.AddOrUpdateLinkEntry(1, "neue BezGoogle", "neue Besch2", "ordnungsM2");
            //dal2.StoreHersteller(hersteller);
        }
    }
}
