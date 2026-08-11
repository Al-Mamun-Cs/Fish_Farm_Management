using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesProductReturns.Validators;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Commands
{
    public class CreateFisheriesProductReturnCommandHandler : IRequestHandler<CreateFisheriesProductReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFisheriesProductReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFisheriesProductReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFisheriesProductReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesProductReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FisheriesProductReturn = _mapper.Map<FisheriesProductReturn>(request.FisheriesProductReturnDto);
                FisheriesProductReturn = await _unitOfWork.Repository<FisheriesProductReturn>().Add(FisheriesProductReturn);

                if (FisheriesProductReturn.PaymentReturnType == 1)
                {
                    var supplier = await _unitOfWork.Repository<Supplier>().Get(FisheriesProductReturn?.SupplierId ?? 0);
                    supplier.TotalDueAmount -= (FisheriesProductReturn.ReturnAmount);
                    await _unitOfWork.Repository<Supplier>().Update(supplier);

                    var fd = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesProductReturn?.FisheriesInventoryDetailId ?? 0);
                    fd.AvailableQty -= (FisheriesProductReturn.ReturnQty);
                    await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fd);

                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FisheriesProductReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand += (FisheriesProductReturn.ReturnAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                    var fd = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesProductReturn?.FisheriesInventoryDetailId ?? 0);
                    fd.AvailableQty -= (FisheriesProductReturn.ReturnQty);
                    await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fd);
                }


                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = FisheriesProductReturn.FisheriesProductReturnId;
            }

            return response;
        }
    }
}
