using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesUnits.Validators;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Commands
{
    public class CreateFisheriesUnitCommandHandler : IRequestHandler<CreateFisheriesUnitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFisheriesUnitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFisheriesUnitCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFisheriesUnitDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesUnitDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FisheriesUnit = _mapper.Map<FisheriesUnit>(request.FisheriesUnitDto);

                FisheriesUnit = await _unitOfWork.Repository<FisheriesUnit>().Add(FisheriesUnit);
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
                response.Id = FisheriesUnit.FisheriesUnitId;
            }

            return response;
        }
    }
}
