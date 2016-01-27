using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blogness.Models.Repository.Interfaces;

namespace Blogness.Models.Repository.Users
{
    public class UserStore<TUser> : 
        IUserStore<TUser>, 
        IUserEmailStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserLockoutStore<TUser>,
        IQueryableUserStore<TUser>
        where TUser : User
    {
        IMongoCollection<User> _userCollection;

        public IQueryable<TUser> Users
        {
            get
            {
                return _userCollection.AsQueryable() as IMongoQueryable<TUser>;
            }
        }

        public UserStore (IContext context)
        {
            _userCollection = context.SetAsync<User>() as IMongoCollection<User>;
            var userNameIndex = Builders<User>.IndexKeys.Ascending(u => u.UserName);
            _userCollection.Indexes.CreateOneAsync(userNameIndex, new CreateIndexOptions { Unique = true });
        }

        public void Dispose()
        {
        }

        public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken token)
        {
            await Task.Run(() => user.AddRole(roleName), token);
        }

        public async Task<string> GetUserIdAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.Id);
        }

        public async Task<string> GetUserNameAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.UserName);
        }

        public async Task SetUserNameAsync(TUser user, string userName, CancellationToken token)
        {
            await Task.Run(() => user.UserName = userName, token);
        }

        public async Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.UserName.Normalize());
        }
        public async Task SetNormalizedUserNameAsync(TUser user, string userName, CancellationToken token)
        {
            await Task.Run(() => user.UserName = userName.Normalize(), token);
        }

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken token)
        {         
            try
            {
                await _userCollection.InsertOneAsync(user, token);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken token)
        {
            try
            {
                await _userCollection.DeleteOneAsync(u => u.Id == user.Id);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<TUser> FindByEmailAsync(string email, CancellationToken token)
        {
            return await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync(token) as TUser;
        }

        public async Task<TUser> FindByIdAsync(string userId, CancellationToken token)
        {
            return await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync(token) as TUser;
        }

        public async Task<TUser> FindByNameAsync(string userName, CancellationToken token)
        {
            return await _userCollection.Find(u => u.UserName == userName).FirstOrDefaultAsync(token) as TUser;
        }

        public async Task<int> GetAccessFailedCountAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.AccessFailedCount);
        }

        public async Task<string> GetEmailAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.Email);
        }

        public async Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.Email.Normalize());
        }

        public async Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.EmailConfirmed);
        }

        public async Task<bool> GetLockoutEnabledAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.LockoutEnabled);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult((DateTimeOffset)user.LockoutEndDate);
        }

        public async Task<string> GetPasswordHashAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.GetRoles());
        }

        public async Task<string> GetSecurityStampAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.SecurityStamp);
        }

        public async Task<bool> HasPasswordAsync(TUser user, CancellationToken token)
        {
            return await Task.FromResult(user.HasPassword());
        }

        public async Task<int> IncrementAccessFailedCountAsync(TUser user, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return user.IncrementAccessFailedCount();
            }, token);
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken token)
        {
            return await Task.FromResult(user.IsInRole(roleName));
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken token)
        {
            await Task.Run(() => user.RemoveFromRole(roleName), token);
        }

        public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken token)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Roles, r => r.RoleName == roleName);
            return await _userCollection.Find(filter).ToListAsync(token) as IList<TUser>;
        }

        public async Task ResetAccessFailedCountAsync(TUser user, CancellationToken token)
        {
            await Task.Run(() => user.AccessFailedCount = 0, token);
        }

        public async Task SetEmailAsync(TUser user, string email, CancellationToken token)
        {
            await Task.Run(() => user.Email = email, token);
        }

        public async Task SetNormalizedEmailAsync(TUser user, string email, CancellationToken token)
        {
            await Task.Run(() => user.Email = email.Normalize(), token);
        }

        public async Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken token)
        {
            await Task.Run(() => user.EmailConfirmed = confirmed, token);
        }

        public async Task SetLockoutEnabledAsync(TUser user, bool enabled, CancellationToken token)
        {
            await Task.Run(() => user.LockoutEnabled = enabled, token);
        }

        public async Task SetLockoutEndDateAsync(TUser user, DateTimeOffset? lockoutEnd, CancellationToken token)
        {
            await Task.Run(() => user.LockoutEndDate = lockoutEnd.HasValue ? lockoutEnd.Value.UtcDateTime : DateTime.MinValue, token);
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken token)
        {
            await Task.Run(() => user.PasswordHash = passwordHash, token);
        }

        public async Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken token)
        {
            await Task.Run(() => user.SecurityStamp = stamp, token);
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken token)
        {
            try
            {
                await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }       
        }
    }
}
