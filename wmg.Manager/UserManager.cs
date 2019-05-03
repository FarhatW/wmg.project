using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using wmg.BusinessLayer.IManager;
using wmg.Common.Core;
using wmg.Common.Entites;
using wmg.Common.Extentions;
using wmg.Common.Query;
using wmg.Common.Resources;
using wmg.Common.Resources.User;
using wmg.Common.Setting;
using wmg.DataAccess;
using wmg.DataAccess.dbContext;

namespace wmg.Manager
{
    public class UserManager : IUserManager
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly IMapper _mapper;
        private readonly IRoleManager _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        private IRepository<WmgDbContext> Repository { get; }
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly AuthSetting _optionsUser;

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper, IRoleManager roleManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IRepository<WmgDbContext> repository, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<AuthSetting> optionsUser)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            Repository = repository;
            _claimsFactory = claimsFactory;
            _optionsUser = optionsUser.Value;
        }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoWmgDbContextAsync();
        }

        public async Task Delete(int id)
        {
            var user = await GetItemById(id);

            if (user == null)
                throw new Exception("user not Found");

            Repository.Remove(user);
            await SaveChanges();
        }

        public Task SaveHistoryAction(string action, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var user = await Repository.GetOne<User>().Include(v => v.UserRoles)
                .ThenInclude(vf => vf.Role).FirstOrDefaultAsync(v => v.Id == id);
            var result = _mapper.Map<User, UserResource>(user);
            return result;
        }

        public async Task<UserResource> Add(ResourceEntity resourceEntity)
        {
            var saveUserResource = (UserSaveResource)resourceEntity;
            if (!EnumerableExtensions.Any(saveUserResource.Roles)) throw new Exception("role is required");

            if (!await RoleExiste(saveUserResource.Roles))
            {
                throw new Exception("role dont exist, valid role is required");
            }

            var user = _mapper.Map<UserSaveResource, User>(saveUserResource);
            user.UserName = RandomString(5) + "@.aze.com";
            user.Email = RandomString(5) + "@.aze.com";

            var result = await _userManager.CreateAsync(user, saveUserResource.Password);
            if (!result.Succeeded) throw new Exception(result.Errors.ToString());
//            var mappedUser = _mapper.Map<User, UserSaveResource>(user);
//            mappedUser.Roles = saveUserResource.Roles;
//
//            var updatedUser = await Update(user.Id, mappedUser);
            var claims = await GetUserClaims(user);
            await _userManager.AddClaimsAsync(user, claims);
            var updatedUser = _mapper.Map<User, UserResource>(user);
            updatedUser.Token = GetUserToken(claims.ToList());
            return updatedUser;
        }

        public async Task<UserResource> Update(int id, ResourceEntity resourceEntity)
        {
            var updateUserResource = (UpdateUserResource)resourceEntity;
            var user = await Repository.GetOne<User>().Include(u => u.UserRoles)
                .ThenInclude(vf => vf.Role).FirstOrDefaultAsync(v => v.Id == id);


            if (user == null)
                throw new Exception("user not Found");

            if (updateUserResource.Email != user.Email && EmailExist(updateUserResource.Email))
            {
                throw new Exception("email exist");
            }

            _mapper.Map(updateUserResource, user);
            //await Helper.UpdateUserDatas(saveUserResource, user, this);

            await SaveChanges();


            return _mapper.Map<User, UserResource>(user);
        }

        public async Task<QueryResult<UserResource>> GetAll(FilterResource filterResource)
        {
            var queryResource = (UserQueryResource)filterResource;
            var result = new QueryResult<User>();
            var filters = _mapper.Map<UserQueryResource, UserQuery>(queryResource);

            var query = Repository.GetAll<User>().Include(v => v.UserRoles)
                .ThenInclude(vf => vf.Role).AsQueryable();

            var columMap = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["id"] = v => v.Id,
                ["email"] = v => v.Email

            };

            var queryObj = _mapper.Map<UserQueryResource, UserQuery>(queryResource);

            result.TotalItems = await query.CountAsync();
            if (queryObj.PageSize == 0)
            {
                queryObj.PageSize = result.TotalItems;
            }
            else if (!string.IsNullOrEmpty(queryObj.Search))
            {
                query = query.Where(s =>
                    s.UserName.ToLowerInvariant().Contains(queryObj.Search.ToLowerInvariant()));
            }
            query = query.ApplyOrdering(queryObj, columMap);

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<User>, QueryResult<UserResource>>(result);

        }

        public async Task<UserResource> GetIUserByEmail(string email)
        {
            var user = await Repository.GetOne<User>().Include(v => v.UserRoles)
                .ThenInclude(vf => vf.Role).FirstOrDefaultAsync(v => v.UserName == email);
           
            var result = _mapper.Map<User, UserResource>(user);
            var principal = await _claimsFactory.CreateAsync(user);
            result.Token = GetUserToken(principal.Claims.ToList());
            return result;
        }

        public bool EmailExist(string email)
        {
            var employees = Repository.GetAll<User>();

            return employees != null && employees.Any(x => x.Email == email);

        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> RoleExiste(ICollection<int> roles)
        {

            var result = false;
            foreach (var role in roles)
            {
                result = await _roleManager.GetItemById(role) != null;
            }

            return result;
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(User user)
        {
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
//            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
//            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
//            claims.Add(new Claim(ClaimTypes.Email, user.Email));
//
//            claims.AddRange(user.UserRoles.Select(item => new Claim(ClaimTypes.Role, item.Role.Name)));

            return claims;
        }

        private  string GetUserToken(List<Claim> Claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_optionsUser.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
           return  tokenHandler.WriteToken(token);
           
        }

    }
}
