using System;
using System.Collections.Generic;
using System.Text;
using wmg.DataAccess.dbContext;

namespace wmg.DataAccess
{
   public class DbFactory : IDbFactory
    {

        public WmgDbContext GetWmgDbContext { get; }


        public DbFactory(WmgDbContext getWmgDbContext)
        {
            GetWmgDbContext = getWmgDbContext;

        }

    }
}
