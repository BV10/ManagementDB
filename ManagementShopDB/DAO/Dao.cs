using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementShopDB.DAO
{
    public abstract class Dao
    {
        private string connectionSql;

        protected Dao(string connectionSql)
        {
            this.connectionSql = connectionSql;
        }

        public abstract int insert<T>(T data);
        public abstract int remove<T>(T id);
        public abstract int update<T, E>(T id, E elem);
        public abstract DataTable select<T>(T data);
    }
}
