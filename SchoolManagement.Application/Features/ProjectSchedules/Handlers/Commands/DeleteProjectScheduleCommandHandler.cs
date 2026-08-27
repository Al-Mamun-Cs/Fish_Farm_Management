using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Commands
{
    public class DeleteProjectScheduleCommandHandler : IRequestHandler<DeleteProjectScheduleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectScheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProjectScheduleCommand request, CancellationToken cancellationToken)
        {
            var ProjectSchedule = await _unitOfWork.Repository<ProjectSchedule>().Get(request.ProjectScheduleId);

            if (ProjectSchedule == null)
                throw new NotFoundException(nameof(ProjectSchedule), request.ProjectScheduleId);


            try
            {
                await _unitOfWork.Repository<ProjectSchedule>().Delete(ProjectSchedule);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ProjectScheduleId);
            }

            return Unit.Value;
        }
    }
}
