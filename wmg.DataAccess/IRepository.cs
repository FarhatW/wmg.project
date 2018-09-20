using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wmg.DataAccess
{
    public interface IRepository<u>

    {
        u Context { get; }
        IQueryable<T> GetOne<T>() where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        void Add<T>(T tObject) where T : class;
        void Remove<T>(T tObject) where T : class;
    }
}
