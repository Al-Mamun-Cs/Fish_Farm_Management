using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Role;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Responses;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Contracts.Identity
{
    public interface IRoleService
    {

        Task<PagedResult<RoleDto>> GetAllRoles(QueryParams queryParams);
        Task<RoleDto> GetRoleById(string id);
        //Task<Employee> GetEmployee(string userId);
        //Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams);
        //Task<BaseCommandResponse> Save(CreateUserDto user);
        Task<BaseCommandResponse> Save(string roleId, CreateRoleDto model);
        Task<List<SelectedModel>> GetSelectedRoleList();
        Task<List<SelectedModel>> GetSelectedAllRoleList();
        Task<BaseCommandResponse> DeleteRole(string id);

        //Task<List<SelectedModel>> GetSelectedRoleForTraineeList();
    }
}
