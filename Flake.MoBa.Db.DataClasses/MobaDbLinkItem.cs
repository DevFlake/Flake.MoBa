using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Flake.MoBa.Db.DataClasses
{
    public class MobaDbLinkItem
    {
        public MobaDbLink Link { get; set; }
        public int ElementNid { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public string Ordnungsmerkmal { get; set; }
        public LinkItemTypes.LinkItemType LinkItemType { get; set; }

        public string Key { get { return Link.LinkNid.ToString() + "|" + ElementNid.ToString(); } }
        public MobaDbLinkItem Value { get { return this; } }

        public MobaDbLinkItem()
        {
            Link = new MobaDbLink();
            ElementNid = 0;
            Bezeichnung = string.Empty;
            Beschreibung = string.Empty;
            Ordnungsmerkmal = string.Empty;
        }

        public MobaDbLinkItem(MobaDbLink link, int elementNid, LinkItemTypes.LinkItemType linkItemType)
            : this()
        {
            Link = link;
            ElementNid = elementNid;
            LinkItemType = linkItemType;
            Bezeichnung = link.Bezeichnung;
            Beschreibung = link.Beschreibung;
        }

        public MobaDbLinkItem(MobaDbLink link, int elementNid, LinkItemTypes.LinkItemType linkItemType, string bezeichnung = "")
            : this(link, elementNid, linkItemType)
        {
            Bezeichnung = (bezeichnung == string.Empty) ? link.Bezeichnung : bezeichnung;
            Beschreibung = link.Beschreibung;
        }

               public MobaDbLinkItem(MobaDbLink link, int elementNid, LinkItemTypes.LinkItemType linkItemType, string bezeichnung = "", string beschreibung = "", string ordnungsmerkmal = "")
            : this(link, elementNid, linkItemType, bezeichnung)
        {
            Beschreibung = (beschreibung == string.Empty) ? link.Beschreibung : beschreibung;
            Ordnungsmerkmal = ordnungsmerkmal;
        }

        //public MobaDbLinkItem(MobaDbLink link, int elementNid, LinkItemTypes.LinkItemType linkItemType, string bezeichnung, string ordnungsmerkmal)
        //    : this(link, elementNid, linkItemType, bezeichnung)
        //{
        //                Ordnungsmerkmal = ordnungsmerkmal;
        //}

    }
}
