using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using wmg.Common.Entites;
using wmg.Common.Extentions;
using wmg.Common.Resources.User;

namespace wmg.Common.Mapping
{
    class UserMapping : Profile
    {
        public UserMapping()
        {


            CreateMap<RoleResource, Role>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<Role, RoleResource>();

            CreateMap<User, UserResource>()
                .ForMember(vr => vr.Roles, opt => opt.MapFrom(v => v.UserRoles.Select(vf => new Role
                {
                    Id = vf.RoleId,
                    Name = vf.Role.Name,
                    CreatedBy = vf.Role.CreatedBy,
                    CreatedOn = vf.Role.CreatedOn,
                    UpdatedBy = vf.Role.UpdatedBy,
                    UpdatedOn = vf.Role.UpdatedOn,
                    Enable = vf.Role.Enable,
                    Rank = vf.Role.Rank
                })))
                .AfterMap((a, b) => b.Address = new AddressResource
                {
                    Company = a.Company,
                    City = a.City,
                    Agency = a.Agency,
                    AddressExtra = a.AddressExtra,
                    Service = a.Service,
                    Address1 = a.Address1,
                    StreetNumber = a.StreetNumber,
                    Address2 = a.Address2,
                    PostalCode = a.PostalCode
                });



            CreateMap<UpdateUserResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(v => v.UserRoles, opt => opt.Ignore())
                .AfterMap((ur, u) =>
                {
                    var removedRoles = u.UserRoles.Where(f => !ur.Roles.Contains(f.RoleId)).ToList();
                    foreach (var f in removedRoles)
                    {
                        u.UserRoles.Remove(f);
                    }

                    var addedFeatures = ur.Roles.Where(id => u.UserRoles.All(f => f.RoleId != id))
                        .Select(id => new UserRole {RoleId = id, UserId = ur.Id}).ToList();
                    foreach (var f in addedFeatures)
                    {
                        u.UserRoles.Add(f);
                    }

                }).AfterMap((a, b) =>
                    {
                        b.Company = a.Address.Company;
                        b.City = a.Address.City;
                        b.Agency = a.Address.Agency;
                        b.AddressExtra = a.Address.AddressExtra;
                        b.Service = a.Address.Service;
                        b.Address1 = a.Address.Address1;
                        b.StreetNumber = a.Address.StreetNumber;
                        b.Address2 = a.Address.Address2;
                        b.PostalCode = a.Address.PostalCode;
                    }
                ); 

            CreateMap<User, UserSaveResource>()
                .ForMember(ur => ur.Roles, opt => opt.MapFrom(v => v.UserRoles.Select(vf => vf.RoleId)));


            CreateMap<UserSaveResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(v => v.UserRoles, opt => opt.Ignore())
                .AfterMap((ur, u) =>
                {
                    var removedRoles = u.UserRoles.Where(f => !ur.Roles.Contains(f.RoleId)).ToList();
                    foreach (var f in removedRoles)
                    {
                        u.UserRoles.Remove(f);
                    }

                    var addedFeatures = ur.Roles.Where(id => u.UserRoles.All(f => f.RoleId != id))
                        .Select(id => new UserRole {RoleId = id, UserId = ur.Id}).ToList();
                    foreach (var f in addedFeatures)
                    {
                        u.UserRoles.Add(f);
                    }

                }).AfterMap((a, b) =>
                    {
                        b.Company = a.Address.Company;
                        b.City = a.Address.City;
                        b.Agency = a.Address.Agency;
                        b.AddressExtra = a.Address.AddressExtra;
                        b.Service = a.Address.Service;
                        b.Address1 = a.Address.Address1;
                        b.StreetNumber = a.Address.StreetNumber;
                        b.Address2 = a.Address.Address2;
                        b.PostalCode = a.Address.PostalCode;
                    }
                );
            


            CreateMap(typeof(QueryResult<>), typeof(UserQueryResource));
        }
    }
}
