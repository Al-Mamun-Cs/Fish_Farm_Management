using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Models.Hikvision;
using SchoolManagement.Application.Models.Identity;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using SchoolManagement.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace SchoolManagement.Identity.Services
{
    public class UserService : IUserService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager,  IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }
        public async Task<BaseCommandResponse> UpdateUserPassword(string userId,PasswordChangeDto userDto)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);
            await _signinManager.RefreshSignInAsync(user);

            return response;
        }
        //public async Task<Employee> UpdateUserPassword(string userId, PasswordChangeDto userDto)
        //{
        //    var User = new ApplicationUser();

        //    if (!string.IsNullOrWhiteSpace(userId))
        //    {
        //        User = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        //        User.PasswordHash = userDto.Password;
        //        await _userManager.UpdateAsync(User);
        //    }

        //    var employee = await _userManager.FindByIdAsync(userId);
        //    return new Employee
        //    {
        //        Email = employee.Email,
        //        Id = employee.Id,
        //        Firstname = employee.FirstName,
        //        Lastname = employee.LastName,
        //        Password = userDto.Password,
        //        ConfirmPassword = userDto.ConfirmPassword
        //    };
        //}

        public async Task<Employee> GetEmployeeByUserId(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee { 
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = new UserDto();
                
            userDto = new UserDto
            {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    PhoneNumber = user.PhoneNumber,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    RoleName = user.RoleName,
                    BranchId = Int32.Parse(user.BranchId),
                    PhotoPath = user.PhotoPath,
                    SignaturePath = user.SignaturePath,
                    IsActive = user.IsActive
            };





            return userDto;


        }

        //public async Task<BaseCommandResponse> DeleteUser(string id)
        //{
        //    var response = new BaseCommandResponse();


        //    var user = await _userManager.FindByIdAsync(id);
        //    await _userManager.DeleteAsync(user);
        //    response.Success = true;
        //    response.Message = "Deleted Successful";
        //    return response;


        //}

        public async Task<BaseCommandResponse> ResetPassword(string userId, CreateUserDto userDto)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(userId);


            if (user == null)
                throw new BadRequestException("Invalide Request !!");
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string newPassword = "Admin@123";
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!resetPassResult.Succeeded)
            {
                throw new BadRequestException(resetPassResult.Errors.ToString());

            }
            //await _userManager.ChangePasswordAsync(user, user.p, userDto.NewPassword);
            //var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            ////await _userManager.ResetPasswordAsync(user, resetToken, userDto.Password);
            //await _signinManager.RefreshSignInAsync(user);
            // debug doren
            return response;
        }

        public async Task<BaseCommandResponse> DeleteUser(string userId)
        {
            var response = new BaseCommandResponse();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new BadRequestException("Invalide Request !!");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new BadRequestException("User not found.");
            }
            user.IsActive = false;
            if (DeleteUserRoles(user))
            {
                IdentityResult result = _userManager.DeleteAsync(user).Result;
                response.Success = result.Succeeded;
                response.Message = "Deleted Successful";
                return response;
            }
            response.Success = false;
            response.Message = "Deleted Failed";
            return response;
        }

        public bool DeleteUserRoles(ApplicationUser user)
        {
           
            IList<string> userRoles = _userManager.GetRolesAsync(user).Result;
            if (userRoles.Any())
            {
                return  _userManager.RemoveFromRolesAsync(user, userRoles).Result.Succeeded;
            }

            return true;
        }

        public async Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(queryParams);

            if (validationResult.IsValid == false)
                throw new FluentValidation.ValidationException(validationResult.ToString());

            IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();
            string[] searchingRoles = { CustomRoleTypes.Member };
         
            userQuery = userQuery.Where(x => !searchingRoles.Contains(x.RoleName));
            var totalCount = userQuery.Count();
            userQuery = userQuery.OrderByDescending(x => x.UserName).Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            var UsersDtos = userQuery.Select(q => new UserDto
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                UserName = q.UserName,
                PhoneNumber =q.PhoneNumber,
                RoleName =q.RoleName,
                //TraineeId = q.PNo,
                BranchId = string.IsNullOrEmpty(q.BranchId) ? (int?)null : int.Parse(q.BranchId)
            }).ToList();

            foreach (var dto in UsersDtos)
            {
                if (dto.BranchId.HasValue)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(dto.BranchId.Value);
                    if (warehouse != null)
                    {
                        dto.WarehouseName = warehouse.WarehouseName;
                    }
                    else
                    {
                        dto.WarehouseName = "N/A"; // or keep it null if you prefer
                    }
                }
                else
                {
                    dto.WarehouseName = "N/A"; // branch not assigned
                }
            }

            var permission = new RoleFeature();
            var result = new PagedResult<UserDto>(UsersDtos, totalCount, queryParams.PageNumber, queryParams.PageSize, permission);

            return result;

        }

        public async Task<BaseCommandResponse> Save(string userId,CreateUserDto userDto)
        {
            var response = new BaseCommandResponse();

        
                var User = new ApplicationUser();
                /////// Image Upload //////////
                string uniquePhotoName = null;
                string uniqueSignatureName = null;

                if (userDto.Photo != null)
                {
                    var fileName = Path.GetFileName(userDto.Photo.FileName);
                    uniquePhotoName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\users\\photo", uniquePhotoName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await userDto.Photo.CopyToAsync(fileSteam);
                    }
                    userDto.PhotoPath = "/Content/files/users/photo/" + uniquePhotoName;
                }
                if (userDto.Signature != null)
                {
                    var fileName = Path.GetFileName(userDto.Signature.FileName);
                    uniqueSignatureName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\users\\signature", uniqueSignatureName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await userDto.Signature.CopyToAsync(fileSteam);
                    }
                    userDto.SignaturePath = "/Content/files/users/signature/" + uniqueSignatureName;
                }

                bool isSameRole = false;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    User = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                    isSameRole = User.RoleName != userDto.RoleName;
                }

                User.Email = userDto.Email;
                User.FirstName = userDto.FirstName;
                User.PhoneNumber = userDto.PhoneNumber;
                User.LastName = userDto.LastName;
                User.UserName = userDto.UserName;
                User.RoleName = userDto.RoleName;
                User.PNo =  userDto.TraineeId ;
                User.BranchId =  userDto.BranchId;
                User.SupplierId =  userDto.SupplierId ??0;
                User.DepartmentPostPositionId =  userDto.DepartmentPostPositionId ?? 0;
                User.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
                User.CreatedDate = DateTime.Now;
                User.InActiveBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
                User.InActiveDate = DateTime.Now;
                User.IsActive = userDto.IsActive;
                User.EmailConfirmed = true;
                User.PhotoPath = userDto.PhotoPath;
                User.SignaturePath = userDto.SignaturePath;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await _userManager.UpdateAsync(User);
                if (isSameRole)
                {
                    await _userManager.RemoveFromRoleAsync(User, userDto.RoleName);
                    await _userManager.AddToRoleAsync(User, userDto.RoleName);
                }
                    response.Success = true;
                    response.Message = "Updated Successful";
                    return response;
                }
                var existingUser = await _userManager.FindByNameAsync(userDto.UserName);

                if (existingUser != null)
                {
                    throw new BadRequestException($"Username '{userDto.UserName}' already exists.");
                }

                var existingEmail = await _userManager.FindByEmailAsync(userDto.Email);

                if (existingEmail == null)
                {
                    var result = await _userManager.CreateAsync(User, userDto.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(User, userDto.RoleName);

                        response.Success = true;
                        response.Message = "Creation Successful";
                       // response.Id = User.Id;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException($"{result.Errors}");
                        //throw new BadRequestException($"{result.Errors}");
                    }
                }
                else
                {
                    throw new BadRequestException($"Email {userDto.Email } already exists.");
                }

            
            return response;
        }

        public async Task<PagedResult<UserDto>> GetMembershipUsers(QueryParams queryParams)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(queryParams);

            if (validationResult.IsValid == false)
                throw new FluentValidation.ValidationException(validationResult.ToString());

            IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();

            string[] searchingRoles = { CustomRoleTypes.Member };
            userQuery = userQuery.Where(x => searchingRoles.Contains(x.RoleName));
            var totalCount = userQuery.Count();
            userQuery = userQuery.OrderByDescending(x => x.UserName).Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            var UsersDtos = userQuery.Select(q => new UserDto
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                UserName = q.UserName,
                PhoneNumber = q.PhoneNumber,
                RoleName = q.RoleName,
                //PNo = Convert.ToInt32(q.PNo),
                IsActive = q.IsActive
            }).ToList();
            var permission = new RoleFeature();
            var result = new PagedResult<UserDto>(UsersDtos, totalCount, queryParams.PageNumber, queryParams.PageSize, permission);

            return result;
        }

        public async Task<PagedResult<UserDto>> GetUserByPNO(QueryParams queryParams)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(queryParams);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.ToString());

            IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();

            string[] searchingRoles = { CustomRoleTypes.Member };

            // 1️⃣ Filter out Member roles
            userQuery = userQuery.Where(x => !searchingRoles.Contains(x.RoleName));

            // 2️⃣ Apply search if SearchText is provided
            if (!string.IsNullOrEmpty(queryParams.SearchText))
            {
                string search = queryParams.SearchText.ToLower();

                userQuery = userQuery.Where(x =>
                    (!string.IsNullOrEmpty(x.UserName) && x.UserName.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(x.LastName) && x.LastName.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber.ToLower().Contains(search))
                );
            }

            var totalCountBeforePNoFilter = userQuery.Count();

            // 3️⃣ Apply pagination before materializing
            var pagedUsers = userQuery
                .OrderByDescending(x => x.UserName)
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToList(); // bring data into memory to safely filter PNo > 0

            // 4️⃣ Filter PNo > 0 in memory and map to DTO
            var UsersDtos = pagedUsers
                .Where(x => !string.IsNullOrEmpty(x.PNo) && int.TryParse(x.PNo, out int pno) && pno > 0)
                .Select(q => new UserDto
                {
                    Id = q.Id,
                    Email = q.Email,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    UserName = q.UserName,
                    PhoneNumber = q.PhoneNumber,
                    RoleName = q.RoleName,
                    TraineeId = int.TryParse(q.PNo, out int pno2) ? pno2 : (int?)null,
                    BranchId = string.IsNullOrEmpty(q.BranchId) ? (int?)null : int.Parse(q.BranchId)
                }).ToList();

            // 5️⃣ Fetch warehouse names
            foreach (var dto in UsersDtos)
            {
                if (dto.BranchId.HasValue)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(dto.BranchId.Value);
                    dto.WarehouseName = warehouse != null ? warehouse.WarehouseName : "N/A";
                }
                else
                {
                    dto.WarehouseName = "N/A";
                }
            }

            var permission = new RoleFeature();
            var totalCount = UsersDtos.Count;

            var result = new PagedResult<UserDto>(
                UsersDtos,
                totalCount,
                queryParams.PageNumber,
                queryParams.PageSize,
                permission
            );

            return result;
        }


        



        //public async Task<List<Employee>> IUserService.GetEmployeeByUserId(string userId)
        //{
        //    var employee = await _userManager.FindByIdAsync(userId);
        //    return new Employee
        //    {
        //        Email = employee.Email,
        //        Id = employee.Id,
        //        Firstname = employee.FirstName,
        //        Lastname = employee.LastName
        //    };
        //}

        //public Task<List<Employee>> GetEmployeeByUserId(string userId)
        //{
        //    var employee = await _userManager.FindByIdAsync(userId);
        //    return new Employee
        //    {
        //        Email = employee.Email,
        //        Id = employee.Id,
        //        Firstname = employee.FirstName,
        //        Lastname = employee.LastName
        //    }; 
        //}
    }
}
