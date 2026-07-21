using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopGoodSales.Validators;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Commands
{
    public class CreateShopGoodSaleCommandHandler : IRequestHandler<CreateShopGoodSaleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShopGoodSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShopGoodSaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShopGoodSaleDetailDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShopGoodSaleDetailDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            try
            {
                // Step 1: Map and Add ShopGoodSale
                var ShopGoodSale = _mapper.Map<ShopGoodSale>(request.ShopGoodSaleDetailDto);
                ShopGoodSale = await _unitOfWork.Repository<ShopGoodSale>().Add(ShopGoodSale);

                if (ShopGoodSale.SupplierId != null)
                {
                    var supplier = await _unitOfWork.Repository<Supplier>().Get(ShopGoodSale?.SupplierId ?? 0);
                    if (supplier != null)
                    {
                        supplier.TotalDueAmount += (ShopGoodSale.CustomerDueAmount ?? 0);
                        await _unitOfWork.Repository<Supplier>().Update(supplier);
                    }
                        
                }

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopGoodSale.WarehouseId ?? 0);

                    warehouse.CashInHand += Convert.ToInt64(ShopGoodSale.CustomerPaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                

                // Step 2: Save to get ShopGoodSaleId from DB
                await _unitOfWork.Save();

                // Step 3: Map and Add ShopGoodSaleDetails
                if (request.ShopGoodSaleDetailDto.ShopGoodSaleDetail != null && request.ShopGoodSaleDetailDto.ShopGoodSaleDetail.Any())
                {
                    foreach (var detailDto in request.ShopGoodSaleDetailDto.ShopGoodSaleDetail)
                    {
                        var inventoryDetail = await _unitOfWork.Repository<ShopInventoryDetail>().Get(detailDto.ShopInventoryDetailId ?? 0);
                        var ShopGoodSaleDetail = _mapper.Map<ShopGoodSaleDetail>(detailDto);
                        ShopGoodSaleDetail.ShopGoodSaleId = ShopGoodSale.ShopGoodSaleId; // Set FK
                        ShopGoodSaleDetail.WarehouseId = ShopGoodSale.WarehouseId;
                        //ShopGoodSaleDetail.CostingPrice = inventoryDetail.CostingPrice;
                        ShopGoodSaleDetail.UnitPurchasePrice = inventoryDetail.UnitPurchasePrice;

                        inventoryDetail.AvailableQty -= Convert.ToInt64(ShopGoodSaleDetail.SaleQty);
                        await _unitOfWork.Repository<ShopInventoryDetail>().Update(inventoryDetail);


                        await _unitOfWork.Repository<ShopGoodSaleDetail>().Add(ShopGoodSaleDetail);

                        
                    }
                }

                // Step 4: Save all ShopGoodSaleDetail entries
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ShopGoodSale.ShopGoodSaleId;
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
