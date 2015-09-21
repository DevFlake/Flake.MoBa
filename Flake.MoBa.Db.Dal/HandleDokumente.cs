using Flake.MoBa.Db.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Db.Dal
{
    public class HandleDokumente
    {
        public void StoreDokumente(IEnumerable<Byte[]> dokumente)
        {
            //using (var db = DbBase.GetConnection())
            //{
            //    var linkItemGroups = linkItems.GroupBy(a => a.LinkItemType);

            //    foreach (var group in linkItemGroups)
            //    {
            //        switch (group.Key)
            //        {
            //            case LinkItemTypes.LinkItemType.herstellerLinkItem:
            //                StoreHerstellerLinkedItem(group, db);
            //                break;
            //            case LinkItemTypes.LinkItemType.artikelLinkItem:
            //                throw new NotImplementedException();
            //            default:
            //                throw new NotImplementedException();
            //        }
            //    }


            //}
        }

        private void StoreHerstellerLinkedItem(IEnumerable<MobaDbLinkItem> linkItems, DbBase.MoBaDbConnectionObject db)
        {


        }
    }
}
