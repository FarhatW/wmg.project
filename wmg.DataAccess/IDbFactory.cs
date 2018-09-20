using System;
using System.Collections.Generic;
using System.Text;
using wmg.DataAccess.dbContext;

namespace wmg.DataAccess
{
   public interface IDbFactory
    {
        WmgDbContext GetWmgDbContext { get; }
    }
}
