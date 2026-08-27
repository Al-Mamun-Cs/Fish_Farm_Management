using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands;
using SchoolManagement.Application.DTOs.ProjectSchedules.Validators;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Commands
{
    public class UpdateProjectScheduleCommandHandler : IRequestHandler<UpdateProjectScheduleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProjectScheduleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectScheduleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectScheduleDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ProjectSchedule = await _unitOfWork.Repository<ProjectSchedule>().Get(request.ProjectScheduleDto.ProjectScheduleId);

            if (ProjectSchedule is null)
                throw new NotFoundException(nameof(ProjectSchedule), request.ProjectScheduleDto.ProjectScheduleId);

            _mapper.Map(request.ProjectScheduleDto, ProjectSchedule);

            await _unitOfWork.Repository<ProjectSchedule>().Update(ProjectSchedule);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
