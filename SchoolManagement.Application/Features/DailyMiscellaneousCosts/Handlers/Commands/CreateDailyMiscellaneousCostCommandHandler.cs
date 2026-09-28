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
                var schedule = await _unitOfWork.Repository<ProjectSchedule>().Get(DailyMiscellaneousCost?.ProjectScheduleId ?? 0);
                DailyMiscellaneousCost.PondId = schedule.PondId;
                DailyMiscellaneousCost.TransactionDate = DailyMiscellaneousCost.TransactionDate.Value.AddDays(1);
                DailyMiscellaneousCost = await _unitOfWork.Repository<DailyMiscellaneousCost>().Add(DailyMiscellaneousCost);


                if (DailyMiscellaneousCost.TransactionType == 2 ||
        DailyMiscellaneousCost.PaymentStatusId == 1)
                {
                    // Warehouse CashInHand কমবে
                    var warehouse = await _unitOfWork
                        .Repository<Warehouse>()
                        .Get(DailyMiscellaneousCost?.WarehouseId ?? 0);

                    warehouse.CashInHand -= DailyMiscellaneousCost.Amount;

                    await _unitOfWork
                        .Repository<Warehouse>()
                        .Update(warehouse);


                    // Supplier Due কমবে
                    if (DailyMiscellaneousCost.SupplierId != null)
                    {
                        var supplier = await _unitOfWork
                            .Repository<Supplier>()
                            .Get(DailyMiscellaneousCost.SupplierId ?? 0);

                        supplier.TotalDueAmount -= DailyMiscellaneousCost.Amount;

                        await _unitOfWork
                            .Repository<Supplier>()
                            .Update(supplier);
                    }
                }
                else
                {
                    // TransactionType != 2 AND PaymentStatusId != 1
                    // Bank balance কমবে
                    var bank = await _unitOfWork
                        .Repository<EasyBikeBank>()
                        .Get(DailyMiscellaneousCost?.EasyBikeBankId ?? 0);

                    bank.BankBalance -= DailyMiscellaneousCost.Amount;

                    await _unitOfWork
                        .Repository<EasyBikeBank>()
                        .Update(bank);


                    // Supplier Due কমবে
                    if (DailyMiscellaneousCost.SupplierId != null)
                    {
                        var supplier = await _unitOfWork
                            .Repository<Supplier>()
                            .Get(DailyMiscellaneousCost.SupplierId ?? 0);

                        supplier.TotalDueAmount -= DailyMiscellaneousCost.Amount;

                        await _unitOfWork
                            .Repository<Supplier>()
                            .Update(supplier);
                    }
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
