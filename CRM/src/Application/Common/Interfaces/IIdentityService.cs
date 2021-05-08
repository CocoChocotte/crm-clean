using System.Collections.Generic;
using System.Threading;
using CRM.Application.Common.Models;
using System.Threading.Tasks;
using CRM.Domain.Interfaces;

namespace CRM.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<List<IApplicationUser>> GetUsersAsync(int? idSociete, string nom, string login, string email, CancellationToken token = default);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role);

        Task<Result> UserIsInRoleAsync(string userId, string role);

        Task<Result> UserHasClaim(string userId, string type, string value);

        Task<bool> UserExistAsync(string username, CancellationToken token = default);

        Task<IApplicationUser> FindUserByIdAsync(string id, CancellationToken token = default);

        Task<Result> AddUserRoleAsync(string id, string role, CancellationToken token = default);

        Task<Result> DeleteUserRoleAsync(string id, string role, CancellationToken token = default);

        Task<Result> UpdateUserAsync(IApplicationUser user, CancellationToken token = default);

        Task<string> GetForgotPasswordTokenAsync(string id, CancellationToken token = default);
    }
}