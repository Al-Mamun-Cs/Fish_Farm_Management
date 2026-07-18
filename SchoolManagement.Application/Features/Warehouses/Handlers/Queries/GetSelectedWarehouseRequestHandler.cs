using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Security.Claims;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Queries
{
    public class GetSelectedWarehouseRequestHandler : IRequestHandler<GetSelectedWarehouseRequest, List<SelectedModel>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISchoolManagementRepository<Warehouse> _WarehouseRepository;


        public GetSelectedWarehouseRequestHandler(ISchoolManagementRepository<Warehouse> WarehouseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _WarehouseRepository = WarehouseRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<List<SelectedModel>> Handle(GetSelectedWarehouseRequest request, CancellationToken cancellationToken)
        //{
        //    ICollection<Warehouse> codeValues = await _WarehouseRepository.FilterAsync(x => x.IsActive);
        //    List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
        //    {
        //        Text = x.WarehouseName,
        //        Value = x.WarehouseId
        //    }).ToList();
        //    return selectModels;
        //}

        public async Task<List<SelectedModel>> Handle(
    GetSelectedWarehouseRequest request,
    CancellationToken cancellationToken)
        {
            ICollection<Warehouse> warehouses;
            var userRole = _httpContextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.Role)?.Value;
            // Salary role check
            if (userRole == CustomRoleTypes.Salary) // or "Salary"
            {
                int[] salaryWarehouseIds = { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22,23, 24, 25, 26, 27, 28, 29, 30 };

                warehouses = await _WarehouseRepository.FilterAsync(
                    x => x.IsActive && salaryWarehouseIds.Contains(x.WarehouseId)
                );
            }
            else
            {
                warehouses = await _WarehouseRepository.FilterAsync(
                    x => x.IsActive
                );
            }

            List<SelectedModel> selectModels = warehouses.Select(x => new SelectedModel
            {
                Text = x.WarehouseName,
                Value = x.WarehouseId
            }).ToList();

            return selectModels;
        }

    }
}
