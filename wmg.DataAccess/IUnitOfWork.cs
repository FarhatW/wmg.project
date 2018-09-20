using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wmg.DataAccess
{
  public  interface IUnitOfWork
    {
        Task SaveIntoWmgDbContextAsync();
    }
}
