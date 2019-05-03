using AutoMapper;
using wmg.Common.Entites;
using wmg.Common.Extentions;
using wmg.Common.Resources.Project;

namespace wmg.Common.Mapping
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            #region Project type
            CreateMap<ProjectTypeResource, ProjectType>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<ProjectType, ProjectTypeResource>();

            CreateMap(typeof(QueryResult<>), typeof(ProjectTypeQueryResource));
            #endregion

            #region Project difficulty

            CreateMap<ProjectDifficultyResource, ProjectDifficulty>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<ProjectDifficulty, ProjectDifficultyResource>();

            CreateMap(typeof(QueryResult<>), typeof(ProjectDifficultyQueryResource));

            #endregion

            #region project

            CreateMap<ProjectResource, Project>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<Project, ProjectResource>();

            CreateMap(typeof(QueryResult<>), typeof(ProjectQueryResource));

            #endregion
        }
    }
}
