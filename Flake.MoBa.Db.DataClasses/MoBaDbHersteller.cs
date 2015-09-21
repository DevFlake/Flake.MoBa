using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flake.MoBa.Base;


namespace Flake.MoBa.Db.DataClasses
{
    public class MoBaDbHersteller
    {
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public int HerstellerNid { get; set; }
        public IEnumerable<string> Schlagworte { get { return _schlagworte.AsEnumerable(); } }
        public IEnumerable<MobaDbLinkItem> Links { get { return _links.OrderBy(a => a.Value.Ordnungsmerkmal).Select(a => a.Value).AsEnumerable(); } }

        private List<string> _schlagworte = new List<string>();
        private Dictionary<string, MobaDbLinkItem> _links = new Dictionary<string, MobaDbLinkItem>();

        #region Constructors

        public MoBaDbHersteller()
        {

        }

        public MoBaDbHersteller(string bezeichnung)
            : this()
        {
            Bezeichnung = bezeichnung;
        }

        public MoBaDbHersteller(int herstellerNid, string bezeichnung)
            : this(bezeichnung)
        {
            HerstellerNid = herstellerNid;
        }

        public MoBaDbHersteller(int herstellerNid, string bezeichnung, string beschreibung)
            : this(herstellerNid, bezeichnung)
        {
            Beschreibung = beschreibung;
        }

        #endregion

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

        public void AddLinkEntry(string url, string linkBezeichnungAllg = "", string linkBezeichnungBzglHersteller = "", string linkBeschreibungAllg = "",
                                    string linkBeschreibungBzglHersteller = "", string ordnungsMerkmalBzglHersteller = "")
        {
            MobaDbLink allgemeinerLink = new MobaDbLink(url, linkBezeichnungAllg, linkBeschreibungAllg);
            MobaDbLinkItem link = new MobaDbLinkItem(allgemeinerLink, HerstellerNid, LinkItemTypes.LinkItemType.herstellerLinkItem, linkBezeichnungBzglHersteller, linkBeschreibungBzglHersteller, ordnungsMerkmalBzglHersteller);
            _links.Add(link.Key, link);
        }

        public void AddOrUpdateLinkEntry(int linkNid, string linkBezeichnungBzglHersteller = "", string linkBeschreibungBzglHersteller = "", string ordnungsMerkmalBzglHersteller = "")
        {
            MobaDbLink allgemeinerLink = new MobaDbLink(linkNid);

            MobaDbLinkItem link = new MobaDbLinkItem(allgemeinerLink, HerstellerNid, LinkItemTypes.LinkItemType.herstellerLinkItem, linkBezeichnungBzglHersteller, linkBeschreibungBzglHersteller, ordnungsMerkmalBzglHersteller);
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
