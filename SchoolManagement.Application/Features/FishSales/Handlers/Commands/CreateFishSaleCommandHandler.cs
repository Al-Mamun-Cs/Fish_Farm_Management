using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FishSales.Validators;
using SchoolManagement.Application.Features.FishSales.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Commands
{
    public class CreateFishSaleCommandHandler : IRequestHandler<CreateFishSaleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFishSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFishSaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFishSaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FishSaleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FishSale = _mapper.Map<FishSale>(request.FishSaleDto);

                FishSale = await _unitOfWork.Repository<FishSale>().Add(FishSale);

                if (FishSale.SupplierId != null)
                {
                    var supplier = await _unitOfWork.Repository<Supplier>().Get(FishSale?.SupplierId ?? 0);
                    supplier.TotalDueAmount += (FishSale.SaleDueAmount);
                    await _unitOfWork.Repository<Supplier>().Update(supplier);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FishSale?.WarehouseId ?? 0);
                    warehouse.CashInHand += (FishSale.SalePaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FishSale?.WarehouseId ?? 0);
                    warehouse.CashInHand += (FishSale.SalePaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = FishSale.FishSaleId;
            }

            return response;
        }
    }
}
