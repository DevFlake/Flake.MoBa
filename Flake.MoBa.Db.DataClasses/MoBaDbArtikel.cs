using Flake.MoBa.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Db.DataClasses
{
    public class MoBaDbArtikel
    {
        public string Bezeichnung { get; set; }
        public int ArtikelNid { get; set; }
        public IEnumerable<string> Schlagworte { get { return _schlagworte.AsEnumerable(); } }
        public IEnumerable<MobaDbLinkItem> Links { get { return _links.OrderBy(a => a.Value.Ordnungsmerkmal).Select(a => a.Value).AsEnumerable(); } }

        private List<string> _schlagworte = new List<string>();

        private Dictionary<string, MobaDbLinkItem> _links = new Dictionary<string, MobaDbLinkItem>();

        public MoBaDbArtikel()
        {
            _schlagworte = new List<string>();
            Bezeichnung = string.Empty;
            ArtikelNid = 0;
        }

        public MoBaDbArtikel(string bezeichnung)
            : this()
        {
            Bezeichnung = bezeichnung;
        }

        public MoBaDbArtikel(int artikelNid, string bezeichnung)
            : this(bezeichnung)
        {
            ArtikelNid = artikelNid;
        }

        public void AddSchlagwort(string schlagwort)
        {
            if (!_schlagworte.Contains(schlagwort))
                _schlagworte.Add(schlagwort);
        }

        public void UpdateLinkEntry(MobaDbLinkItem link)
        {
            var vorhanden = _links.Values.Where(a => a.Key == link.Key);

            if (vorhanden.Count() > 0)
            {
                vorhanden.Foreach(a => _links.Remove(a.Key));
            }
            _links.Add(link.Key, link);
        }

        public void AddLinkEntry(string url, string linkBezeichnungAllg = "", string linkBezeichnungBzglArtikel = "", string linkBeschreibungAllg = "",
                                    string linkBeschreibungBzglArtikel = "", string ordnungsMerkmalBzglArtikel = "")
        {
            MobaDbLink allgemeinerLink = new MobaDbLink(url, linkBezeichnungAllg, linkBeschreibungAllg);
            MobaDbLinkItem link = new MobaDbLinkItem(allgemeinerLink, ArtikelNid, LinkItemTypes.LinkItemType.artikelLinkItem, linkBezeichnungBzglArtikel, linkBeschreibungBzglArtikel, ordnungsMerkmalBzglArtikel);
            _links.Add(link.Key, link);
        }

        public void AddOrUpdateLinkEntry(int linkNid, string linkBezeichnungBzglArtikel = "", string linkBeschreibungBzglArtikel = "", string ordnungsMerkmalBzglArtikel = "")
        {
            MobaDbLink allgemeinerLink = new MobaDbLink(linkNid);

            MobaDbLinkItem link = new MobaDbLinkItem(allgemeinerLink, ArtikelNid, LinkItemTypes.LinkItemType.artikelLinkItem, linkBezeichnungBzglArtikel, linkBeschreibungBzglArtikel, ordnungsMerkmalBzglArtikel);
            if (_links.ContainsKey(link.Key))
            {
                UpdateLinkEntry(link);
            }
            else
            {
                _links.Add(link.Key, link);
            }
        }

    }
}
