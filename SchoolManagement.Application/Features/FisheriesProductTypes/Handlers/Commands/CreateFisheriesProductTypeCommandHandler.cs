using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesProductTypes.Validators;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Commands
{
    public class CreateFisheriesProductTypeCommandHandler : IRequestHandler<CreateFisheriesProductTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFisheriesProductTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFisheriesProductTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFisheriesProductTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesProductTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FisheriesProductType = _mapper.Map<FisheriesProductType>(request.FisheriesProductTypeDto);

                FisheriesProductType = await _unitOfWork.Repository<FisheriesProductType>().Add(FisheriesProductType);
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
                response.Id = FisheriesProductType.FisheriesProductTypeId;
            }

            return response;
        }
    }
}
