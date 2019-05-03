using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wmg.BusinessLayer.Core;
using wmg.Common.Core;
using wmg.Common.Extentions;
using wmg.Common.Resources;
using wmg.Common.Resources.Project;

namespace wmg.BusinessLayer.IManager
{
   public interface IProjectManager : IActionManager<ProjectResource>
    {
        #region project type
        Task DeleteProjectType(int id);
        Task<ProjectTypeResource> GetProjectType(int id, FilterResource filterResource = null);
        Task<ProjectTypeResource> AddProjectType(ResourceEntity resourceEntity);
        Task<ProjectTypeResource> UpdateProjectType(int id, ResourceEntity resourceEntity);
        Task<QueryResult<ProjectTypeResource>> GetAllProjectType(FilterResource filterResource);
        #endregion

        #region project difficulty

        Task DeleteProjectDifficulty(int id);
        Task<ProjectDifficultyResource> GetProjectDifficulty(int id, FilterResource filterResource = null);
        Task<ProjectDifficultyResource> AddProjectDifficulty(ResourceEntity resourceEntity);
        Task<ProjectDifficultyResource> UpdateProjectDifficultye(int id, ResourceEntity resourceEntity);
        Task<QueryResult<ProjectDifficultyResource>> GetAllProjectDifficultye(FilterResource filterResource);

        #endregion

    }
}
