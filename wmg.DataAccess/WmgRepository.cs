using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wmg.DataAccess.dbContext;

namespace wmg.DataAccess
{
   public class WmgRepository : IRepository<WmgDbContext>
    {
        public WmgDbContext Context { get; }
        public WmgRepository(IDbFactory dbFactory)
        {
            Context = dbFactory.GetWmgDbContext;
        }

        public IQueryable<T> GetOne<T>() where T : class
        {
            return Context.Set<T>().AsQueryable();

        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Context.Set<T>().AsQueryable();
        }

        public void Add<T>(T tObject) where T : class
        {
            Context.Set<T>().Add(tObject);
        }

        public void Remove<T>(T tObject) where T : class
        {
            Context.Set<T>().Remove(tObject);
        }

    }
}
