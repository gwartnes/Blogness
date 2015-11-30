using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Models.Repository.Users
{
    public class UserStore<TUser> : 
        IUserStore<TUser, string>, 
        IUserEmailStore<TUser, string>,
        IUserRoleStore<TUser, string>,
        IUserPasswordStore<TUser, string>,
        IUserSecurityStampStore<TUser, string>,
        IUserLockoutStore<TUser, string>
        where TUser : User
    {
        IMongoCollection<User> _userCollection;
        public UserStore (IContext context)
        {
            _userCollection = context.SetAsync<User>() as IMongoCollection<User>;
            var userNameIndex = Builders<User>.IndexKeys.Ascending(u => u.UserName);
            _userCollection.Indexes.CreateOneAsync(userNameIndex, new CreateIndexOptions { Unique = true });
        }

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            return Task.Run(() => user.AddRole(roleName));
        }

        public Task CreateAsync(TUser user)
        {
            return _userCollection.InsertOneAsync(user);
        }

        public Task DeleteAsync(TUser user)
        {
            return _userCollection.DeleteOneAsync(u => u.Id == user.Id);
        }

        public void Dispose()
        {
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            return _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync() as Task<TUser>;
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync() as Task<TUser>;
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return _userCollection.Find(u => u.UserName == userName).FirstOrDefaultAsync() as Task<TUser>;
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult((DateTimeOffset)user.LockoutEndDate);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            return Task.FromResult((IList<string>)user.Roles);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.HasPassword());
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            return Task.Run(() =>
            {
                return user.IncrementAccessFailedCount();
            });
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return Task.FromResult(user.IsInRole(roleName));
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            return Task.Run(() => user.RemoveFromRole(roleName));
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            return Task.Run(() => user.AccessFailedCount = 0);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            return Task.Run(() => user.Email = email);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            return Task.Run(() => user.EmailConfirmed = confirmed);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            return Task.Run(() => user.LockoutEnabled = enabled);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            return Task.Run(() => user.LockoutEndDate = lockoutEnd.UtcDateTime);
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            return Task.Run(() => user.SecurityStamp = stamp);
        }

        public Task UpdateAsync(TUser user)
        {
            return _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}
