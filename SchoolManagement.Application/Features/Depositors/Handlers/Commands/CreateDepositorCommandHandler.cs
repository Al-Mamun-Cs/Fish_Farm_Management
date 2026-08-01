using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Depositors.Validators;
using SchoolManagement.Application.Features.Depositors.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Commands
{
    public class CreateDepositorCommandHandler : IRequestHandler<CreateDepositorCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepositorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDepositorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDepositorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Depositor = _mapper.Map<Depositor>(request.DepositorDto);
                Depositor = await _unitOfWork.Repository<Depositor>().Add(Depositor);
                
                    

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Depositor.DepositorId;
            }

            return response;
        }
    }
}
