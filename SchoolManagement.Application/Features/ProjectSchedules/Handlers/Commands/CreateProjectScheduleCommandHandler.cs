using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectSchedules.Validators;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Commands
{
    public class CreateProjectScheduleCommandHandler : IRequestHandler<CreateProjectScheduleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProjectScheduleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProjectScheduleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectScheduleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ProjectSchedule = _mapper.Map<ProjectSchedule>(request.ProjectScheduleDto);

                ProjectSchedule = await _unitOfWork.Repository<ProjectSchedule>().Add(ProjectSchedule);
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
                response.Id = ProjectSchedule.ProjectScheduleId;
            }

            return response;
        }
    }
}
