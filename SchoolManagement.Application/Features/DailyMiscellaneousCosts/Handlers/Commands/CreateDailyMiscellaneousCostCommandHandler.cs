using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts.Validators;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Commands
{
    public class CreateDailyMiscellaneousCostCommandHandler : IRequestHandler<CreateDailyMiscellaneousCostCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDailyMiscellaneousCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDailyMiscellaneousCostCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDailyMiscellaneousCostDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DailyMiscellaneousCostDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DailyMiscellaneousCost = _mapper.Map<DailyMiscellaneousCost>(request.DailyMiscellaneousCostDto);

                DailyMiscellaneousCost = await _unitOfWork.Repository<DailyMiscellaneousCost>().Add(DailyMiscellaneousCost);

                
                if (DailyMiscellaneousCost.TransactionType == 2)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(DailyMiscellaneousCost?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (DailyMiscellaneousCost.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                    if (DailyMiscellaneousCost.SupplierId != null)
                    {
                        var supplier = await _unitOfWork.Repository<Supplier>().Get(DailyMiscellaneousCost?.SupplierId ?? 0);
                        supplier.TotalDueAmount -= (DailyMiscellaneousCost.Amount);
                        await _unitOfWork.Repository<Supplier>().Update(supplier);
                    }
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(DailyMiscellaneousCost?.WarehouseId ?? 0);
                    warehouse.CashInHand += (DailyMiscellaneousCost.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                    var supplier = await _unitOfWork.Repository<Supplier>().Get(DailyMiscellaneousCost?.SupplierId ?? 0);
                    supplier.TotalDueAmount -= (DailyMiscellaneousCost.Amount);
                    await _unitOfWork.Repository<Supplier>().Update(supplier);

                }

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DailyMiscellaneousCost.DailyMiscellaneousCostId;
            }

            return response;
        }
    }
}
