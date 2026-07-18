using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts.Validators;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Commands
{
    public class CreateFisheriesInventoryOutCommandHandler : IRequestHandler<CreateFisheriesInventoryOutCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFisheriesInventoryOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFisheriesInventoryOutCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFisheriesInventoryOutDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesInventoryOutDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FisheriesInventoryOut = _mapper.Map<FisheriesInventoryOut>(request.FisheriesInventoryOutDto);
                var fInventoryDetail = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesInventoryOut?.FisheriesInventoryDetailId ?? 0);
                FisheriesInventoryOut.UnitPurchasePrice = fInventoryDetail.UnitPurchasePrice;
                FisheriesInventoryOut = await _unitOfWork.Repository<FisheriesInventoryOut>().Add(FisheriesInventoryOut);
                
                fInventoryDetail.AvailableQty -= (FisheriesInventoryOut.UseQty);
                await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fInventoryDetail);

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = FisheriesInventoryOut.FisheriesInventoryOutId;
            }

            return response;
        }
    }
}
