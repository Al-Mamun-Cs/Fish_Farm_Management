using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Fiscalyears.Validators;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Commands
{
    public class CreateFiscalyearCommandHandler : IRequestHandler<CreateFiscalyearCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFiscalyearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFiscalyearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFiscalyearDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FiscalyearDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Fiscalyear = _mapper.Map<Fiscalyear>(request.FiscalyearDto);
                Fiscalyear.StartDate = Fiscalyear.StartDate.Value.AddDays(1);
                Fiscalyear.EndDate = Fiscalyear.EndDate.Value.AddDays(1);
                Fiscalyear = await _unitOfWork.Repository<Fiscalyear>().Add(Fiscalyear);
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
                response.Id = Fiscalyear.FiscalyearId;
            }

            return response;
        }
    }
}
