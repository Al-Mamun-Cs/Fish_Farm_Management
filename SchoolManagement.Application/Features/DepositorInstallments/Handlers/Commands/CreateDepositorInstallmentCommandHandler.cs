using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DepositorInstallments.Validators;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Commands
{
    public class CreateDepositorInstallmentCommandHandler : IRequestHandler<CreateDepositorInstallmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepositorInstallmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDepositorInstallmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDepositorInstallmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorInstallmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DepositorInstallment = _mapper.Map<DepositorInstallment>(request.DepositorInstallmentDto);
                DepositorInstallment = await _unitOfWork.Repository<DepositorInstallment>().Add(DepositorInstallment);
                
                    

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DepositorInstallment.DepositorInstallmentId;
            }

            return response;
        }
    }
}
