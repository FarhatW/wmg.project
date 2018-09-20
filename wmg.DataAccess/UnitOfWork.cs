using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wmg.DataAccess
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task SaveIntoWmgDbContextAsync()
        {
            await _dbFactory.GetWmgDbContext.SaveChangesAsync();
        }
    }
}
