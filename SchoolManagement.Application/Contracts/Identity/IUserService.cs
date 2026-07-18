using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Models.Identity;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<BaseCommandResponse> UpdateUserPassword(string userId,PasswordChangeDto userDto); 
        Task<Employee> GetEmployeeByUserId(string userId);
        Task<Employee> GetEmployee(string userId);
        Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams);
        Task<BaseCommandResponse> Save(string userId,CreateUserDto user);
        Task<UserDto> GetUserById(string id);
        Task<BaseCommandResponse> ResetPassword(string userId, CreateUserDto user);
        Task<BaseCommandResponse> DeleteUser(string id);
        Task<PagedResult<UserDto>> GetMembershipUsers(QueryParams queryParams);
        Task<PagedResult<UserDto>> GetUserByPNO(QueryParams queryParams);

    }
}
