using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Flake.MoBa.Db.Dal
{
    public static class DbBase
    {
        private static MoBaDbEntities _database;
        private static string _connectionString = string.Empty; // TODO

        //public DbBase()
        //{
        //    _database = new MoBaDbEntities();
        //    _connectionString= _database.Database.Connection.ConnectionString;
        //}

        //public void StoreArtikel (MoBaDbArtikel artikel)
        //{
        //    using(_database = GetConnection())
        //    {
        //        foreach(var schlagwort in artikel.Schlagworte)
        //        {

        //            _database.InsertSchlagwortZuArtikel(artikel.ArtikelNid, schlagwort);
        //        }
        //        _database.SaveChanges();
        //    }
        //}



        public static MoBaDbConnectionObject GetConnection()
        {
            if (_database == null)
            {
                _database = new MoBaDbEntities();
                // _database.Database.Connection.ConnectionString = _connectionString; muss persistiert sein

            }
            return new MoBaDbConnectionObject() { MoBaDb = _database, };
        }


        public class MoBaDbConnectionObject : IDisposable
        {
            public MoBaDbEntities MoBaDb { get; set; }

            void IDisposable.Dispose()
            {
                MoBaDb.SaveChanges();
            }
        }
    }
}
