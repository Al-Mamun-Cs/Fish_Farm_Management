using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DepositorInvestments.Validators;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Commands
{
    public class CreateDepositorInvestmentCommandHandler : IRequestHandler<CreateDepositorInvestmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepositorInvestmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDepositorInvestmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDepositorInvestmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorInvestmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DepositorInvestment = _mapper.Map<DepositorInvestment>(request.DepositorInvestmentDto);
                DepositorInvestment = await _unitOfWork.Repository<DepositorInvestment>().Add(DepositorInvestment);
                
                    

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DepositorInvestment.DepositorInvestmentId;
            }

            return response;
        }
    }
}
