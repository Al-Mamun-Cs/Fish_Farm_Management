using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectTypes.Validators;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Commands
{
    public class CreateProjectTypeCommandHandler : IRequestHandler<CreateProjectTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProjectTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProjectTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ProjectType = _mapper.Map<ProjectType>(request.ProjectTypeDto);

                ProjectType = await _unitOfWork.Repository<ProjectType>().Add(ProjectType);
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
                response.Id = ProjectType.ProjectTypeId;
            }

            return response;
        }
    }
}
