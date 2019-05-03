using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using wmg.BusinessLayer.Core;
using wmg.BusinessLayer.IManager;
using wmg.Common.Core;
using wmg.Common.Entites;
using wmg.Common.Extentions;
using wmg.Common.Query;
using wmg.Common.Resources;
using wmg.Common.Resources.Project;
using wmg.DataAccess;
using wmg.DataAccess.dbContext;

namespace wmg.Manager
{
    public class ProjectManager : IProjectManager
    {
        private readonly IMapper _mapper;

        private IRepository<WmgDbContext> Repository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public ProjectManager(IMapper mapper, IRepository<WmgDbContext> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoWmgDbContextAsync();
        }

        #region project

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveHistoryAction(string action, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        Task<ProjectResource> IActionManager<ProjectResource>.GetItemById(int id, FilterResource filterResource)
        {
            throw new NotImplementedException();
        }

        Task<ProjectResource> IActionManager<ProjectResource>.Add(ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        Task<ProjectResource> IActionManager<ProjectResource>.Update(int id, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        Task<QueryResult<ProjectResource>> IActionManager<ProjectResource>.GetAll(FilterResource filterResource)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region project type
        public async Task<ProjectTypeResource> GetProjectType(int id, FilterResource filterResource = null)
        {
            var projectType = await Repository.GetOne<ProjectType>().FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<ProjectType, ProjectTypeResource>(projectType);
        }
        public async Task<ProjectTypeResource> AddProjectType(ResourceEntity resourceEntity)
        {
            var projectTypeResource = (ProjectTypeResource)resourceEntity;

            if (ProjectTypeExist(projectTypeResource.LbProject))
            {
                throw new Exception("projectType exist");
            }
            var projectType = _mapper.Map<ProjectTypeResource, ProjectType>(projectTypeResource);

            Repository.Add(projectType);

            await SaveChanges();

            return _mapper.Map<ProjectType, ProjectTypeResource>(projectType);
        }

        public async Task<ProjectTypeResource> UpdateProjectType(int id, ResourceEntity resourceEntity)
        {
            var projectTypeResource = (ProjectTypeResource)resourceEntity;
            var projectType = await Repository.GetOne<ProjectType>().FirstOrDefaultAsync(v => v.Id == id);

            if (projectType == null)
                throw new Exception("ProjectType not Found");

            _mapper.Map(resourceEntity, projectType);
            projectType.UpdatedOn = DateTime.Now;
            await SaveChanges();
            //await SaveHistoryAction("Update", roleSave);

            var result = await GetItemById(projectType.Id);
            return result;
        }

        public async Task<QueryResult<ProjectTypeResource>> GetAllProjectType(FilterResource filterResource)
        {
            var queryResource = (ProjectTypeQueryResource)filterResource;

            var result = new QueryResult<ProjectType>();
            var filtres = _mapper.Map<ProjectTypeQueryResource, ProjectTypeQuery>(queryResource);

            var query = Repository.GetAll<ProjectType>().AsQueryable();
            var columMap = new Dictionary<string, Expression<Func<ProjectType, object>>>
            {
                ["id"] = v => v.Id,
                ["LbDifficulty"] = v => v.LbProject

            };
            result.TotalItems = await query.CountAsync();

            if (filtres.PageSize == 0)
            {
                filtres.PageSize = result.TotalItems;
            }
            else if (!string.IsNullOrEmpty(filtres.LbProject))
            {
                query = query.Where(s =>
                    s.LbProject.ToLowerInvariant().Contains(filtres.LbProject.ToLowerInvariant()));
            }
            else if (filtres.Id != 0)
            {
                query = query.Where(s =>
                    s.Id.ToString().ToLowerInvariant().Contains(filtres.Id.ToString().ToLowerInvariant()));
            }
            query = query.ApplyOrdering(filtres, columMap);

            query = query.ApplyPaging(filtres);

            result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<ProjectType>, QueryResult<ProjectTypeResource>>(result);
        }

        public async Task DeleteProjectType(int id)
        {
            var projectType = await Repository.GetOne<ProjectType>().FirstOrDefaultAsync(v => v.Id == id);

            if (projectType == null)
                throw new Exception("RoleId not Found");
            var resource = _mapper.Map<ProjectType, ProjectTypeResource>(projectType);

            Repository.Remove(projectType);
            await SaveChanges();
        }

        public bool ProjectTypeExist(string role)
        {
            var projectTypes = Repository.GetAll<ProjectType>();

            return projectTypes != null && projectTypes.Any(x => string.Equals(x.LbProject, role, StringComparison.CurrentCultureIgnoreCase));
        }
        #endregion

        #region project difficulty

        public Task DeleteProjectDifficulty(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDifficultyResource> GetProjectDifficulty(int id, FilterResource filterResource = null)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDifficultyResource> AddProjectDifficulty(ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDifficultyResource> UpdateProjectDifficultye(int id, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<ProjectDifficultyResource>> GetAllProjectDifficultye(FilterResource filterResource)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
