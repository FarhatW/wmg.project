using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wmg.Common.Core;
using wmg.Common.Extentions;
using wmg.Common.Resources;
using wmg.DataAccess;

namespace wmg.BusinessLayer.Core
{

    public interface IActionManager<T>
    {
        IUnitOfWork UnitOfWork { get; }
        Task SaveChanges();
       
        Task SaveHistoryAction(string action, ResourceEntity resourceEntity);
        Task Delete(int id);
        Task<T> GetItemById(int id, FilterResource filterResource = null);
        Task<T> Add(ResourceEntity resourceEntity);
        Task<T> Update(int id, ResourceEntity resourceEntity);

        Task<QueryResult<T>> GetAll(FilterResource filterResource);
    }
}
