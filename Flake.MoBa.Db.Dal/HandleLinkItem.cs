using System;
using System.Collections.Generic;
using System.Linq;
using Flake.MoBa.Db.DataClasses;
using Flake.MoBa.Base;

namespace Flake.MoBa.Db.Dal
{
    public class HandleLinkItems
    {
        public void StoreLinkItems(IEnumerable<MobaDbLinkItem> linkItems)
        {
            using (var db = DbBase.GetConnection())
            {
                var linkItemGroups = linkItems.GroupBy(a => a.LinkItemType);

                foreach (var group in linkItemGroups)
                {
                    switch (group.Key)
                    {
                        case LinkItemTypes.LinkItemType.herstellerLinkItem:
                            StoreHerstellerLinkItems(group, db);
                            break;
                        case LinkItemTypes.LinkItemType.artikelLinkItem:
                            StoreArtikelLinkItems(group, db);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }


            }
        }

        private void StoreHerstellerLinkItems(IEnumerable<MobaDbLinkItem> linkItems, DbBase.MoBaDbConnectionObject db)
        {
            foreach (var linkItem in linkItems)
            {
                int tLinkNid = linkItem.Link.LinkNid;
                if (tLinkNid == -1)
                {
                    tLinkNid = db.MoBaDb.Links.Add(new Links() { Link = linkItem.Link.Url, Bezeichnung = linkItem.Link.Bezeichnung, Beschreibung = linkItem.Link.Beschreibung }).LinkNid;
                }

                var tmp = db.MoBaDb.LinksZuHerstellern.Where(a => a.LinkNid == linkItem.Link.LinkNid && a.HerstellerNid == linkItem.ElementNid);
                if (tmp.Count() == 1)
                {
                    // update
                    tmp.First().Ordnungsmerkmal = linkItem.Ordnungsmerkmal;
                    tmp.First().Bezeichnung = linkItem.Bezeichnung;
                    tmp.First().Beschreibung = linkItem.Beschreibung;
                    tmp.First().LinkNid = tLinkNid;
                }
                else if (tmp.Count() == 0)
                {
                    // add
                    db.MoBaDb.LinksZuHerstellern.Add(new LinksZuHerstellern() { LinkNid = tLinkNid, Ordnungsmerkmal = linkItem.Ordnungsmerkmal, Bezeichnung = linkItem.Bezeichnung, Beschreibung = linkItem.Beschreibung, HerstellerNid = linkItem.ElementNid });
                }
            }
        }

        private void StoreArtikelLinkItems(IEnumerable<MobaDbLinkItem> linkItems, DbBase.MoBaDbConnectionObject db)
        {
            foreach (var linkItem in linkItems)
            {
                int tLinkNid = linkItem.Link.LinkNid;
                if (tLinkNid == -1)
                {
                    tLinkNid = db.MoBaDb.Links.Add(new Links() { Link = linkItem.Link.Url, Bezeichnung = linkItem.Link.Bezeichnung, Beschreibung = linkItem.Link.Beschreibung }).LinkNid;
                }

                var tmp = db.MoBaDb.LinksZuArtikeln.Where(a => a.LinkNid == linkItem.Link.LinkNid && a.ArtikelNid == linkItem.ElementNid);
                if (tmp.Count() == 1)
                {
                    // update
                    tmp.First().Ordnungsmerkmal = linkItem.Ordnungsmerkmal;
                    tmp.First().Bezeichnung = linkItem.Bezeichnung;
                    tmp.First().Beschreibung = linkItem.Beschreibung;
                    tmp.First().LinkNid = tLinkNid;
                }
                else if (tmp.Count() == 0)
                {
                    // add
                    db.MoBaDb.LinksZuArtikeln.Add(new LinksZuArtikeln() { LinkNid = tLinkNid, Ordnungsmerkmal = linkItem.Ordnungsmerkmal, Bezeichnung = linkItem.Bezeichnung, Beschreibung = linkItem.Beschreibung, ArtikelNid = linkItem.ElementNid });
                }
            }
        }
    }
}
