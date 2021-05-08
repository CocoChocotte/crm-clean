using System;
using System.Collections.Generic;
using CRM.Application.Common.Interfaces;
using CRM.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.Domain.Entities;
using CRM.Domain.Interfaces;

namespace CRM.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }

        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IApplicationUser>> GetUsersAsync(int? idSociete, string nom, string login, string email, CancellationToken token = default)
        {
            var user = _userManager.Users.Include(x => x.Utilisateur).AsQueryable();

            if (idSociete.HasValue)
            {
                user = user.Where(u => u.Utilisateur.IdSociete == idSociete);
            }

            if (!string.IsNullOrEmpty(nom))
            {
                user = user.Where(u => u.Utilisateur.Nom.Contains(nom) || u.Utilisateur.Nom.ToLower().Contains(nom.ToLower()));
            }

            if (!string.IsNullOrEmpty(login))
            {
                user = user.Where(u => u.UserName.Contains(login) || u.UserName.ToLower().Contains(login.ToLower()));
            }

            if (!string.IsNullOrEmpty(email))
            {
                user = user.Where(u => u.Email.Contains(email) || u.Email.ToLower().Contains(email.ToLower()));
            }

            return await user.Select(u => u as IApplicationUser).ToListAsync(cancellationToken: token);
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                Utilisateur = new Utilisateur()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!string.IsNullOrWhiteSpace(role))
            {
                user = await _userManager.FindByIdAsync(user.Id);
                await _userManager.AddToRoleAsync(user, role);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> UserIsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null || !await _userManager.IsInRoleAsync(user, role))
            {
                return Result.Failure(new[] { "User not found or is not in role." });
            }

            return Result.Success();
        }

        public async Task<Result> UserHasClaim(string userId, string type, string value)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null || !(await _userManager.GetClaimsAsync(user)).Any(i => i.Type == type && i.Value == value))
            {
                return Result.Failure(new[] { $"User does not have claim for \"{type}\"." });
            }

            return Result.Success();
        }

        public async Task<bool> UserExistAsync(string username, CancellationToken token = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username, cancellationToken: token);

            return user != null;
        }

        public async Task<IApplicationUser> FindUserByIdAsync(string id, CancellationToken token = default)
        {
            var user = await _userManager.Users
                .Include(x => x.Utilisateur)
                .SingleOrDefaultAsync(u => u.Id == id, token);

            return user;
        }

        public async Task<Result> AddUserRoleAsync(string id, string role, CancellationToken token = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id, token);

            if (user == null)
            {
                return Result.Failure(new[] { "User not found" });
            }

            var userIsInRole = await UserIsInRoleAsync(id, role);

            if (!userIsInRole.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return userIsInRole;
        }

        public async Task<Result> DeleteUserRoleAsync(string id, string role, CancellationToken token = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id, token);

            if (user == null)
            {
                return Result.Failure(new[] { "User not found" });
            }

            var userIsInRole = await UserIsInRoleAsync(id, role);

            if (userIsInRole.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            return userIsInRole;
        }

        public async Task<Result> UpdateUserAsync(IApplicationUser user, CancellationToken token = default)
        {
            var result = await _userManager.UpdateAsync(user as ApplicationUser);

            return result.ToApplicationResult();
        }

        public async Task<string> GetForgotPasswordTokenAsync(string id, CancellationToken token = default)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id, token);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}