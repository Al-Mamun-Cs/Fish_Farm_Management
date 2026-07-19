using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons.Validators;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Commands
{
    public class CreateDailyCostVaucherReasonCommandHandler : IRequestHandler<CreateDailyCostVaucherReasonCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDailyCostVaucherReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDailyCostVaucherReasonCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDailyCostVaucherReasonDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DailyCostVaucherReasonDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var DailyCostVaucherReason = _mapper.Map<DailyCostVaucherReason>(request.DailyCostVaucherReasonDto);

                DailyCostVaucherReason = await _unitOfWork.Repository<DailyCostVaucherReason>().Add(DailyCostVaucherReason);
                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                //await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DailyCostVaucherReason.DailyCostVaucherReasonId;
            }

            return response;
        }
    }
}
