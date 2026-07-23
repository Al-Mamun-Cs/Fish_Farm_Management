using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopInventorys.Validators;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Commands
{
    public class CreateShopInventoryCommandHandler : IRequestHandler<CreateShopInventoryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShopInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShopInventoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShopInventoryDetailDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShopInventoryDetailDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            try
            {
                // Step 1: Map and Add ShopInventory
                var ShopInventory = _mapper.Map<ShopInventory>(request.ShopInventoryDetailDto);
                ShopInventory = await _unitOfWork.Repository<ShopInventory>().Add(ShopInventory);

                var FInventory = await _unitOfWork.Repository<Supplier>().Get(ShopInventory?.SupplierId ?? 0);
                FInventory.TotalDueAmount += (ShopInventory.DueAmount);
                await _unitOfWork.Repository<Supplier>().Update(FInventory);

                var paymentStatus = await _unitOfWork.Repository<PaymentStatus>().Get(ShopInventory.PaymentStatusId ?? 0);
                if (paymentStatus != null && paymentStatus.PriorityNo == 1)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopInventory.WarehouseId ?? 0);

                    warehouse.CashInHand -= Convert.ToInt64(ShopInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopInventory.WarehouseId ?? 0);

                    warehouse.BankBalance -= Convert.ToInt64(ShopInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }

                // Step 2: Save to get ShopInventoryId from DB
                await _unitOfWork.Save();

                // Step 3: Map and Add ShopInventoryDetails
                if (request.ShopInventoryDetailDto.ShopInventoryDetail != null && request.ShopInventoryDetailDto.ShopInventoryDetail.Any())
                {
                    foreach (var detailDto in request.ShopInventoryDetailDto.ShopInventoryDetail)
                    {
                        var ShopInventoryDetail = _mapper.Map<ShopInventoryDetail>(detailDto);
                        ShopInventoryDetail.ShopInventoryId = ShopInventory.ShopInventoryId; // Set FK
                        ShopInventoryDetail.WarehouseId = ShopInventory.WarehouseId;
                        ShopInventoryDetail.AvailableQty = ShopInventoryDetail.TotalUnitQty;
                        await _unitOfWork.Repository<ShopInventoryDetail>().Add(ShopInventoryDetail);

                        
                    }
                }

                // Step 4: Save all ShopInventoryDetail entries
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ShopInventory.ShopInventoryId;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Creation Failed due to an exception.";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
