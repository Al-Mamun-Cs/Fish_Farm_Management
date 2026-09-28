using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Commands
{
    public class DeleteProjectTypeCommandHandler : IRequestHandler<DeleteProjectTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProjectTypeCommand request, CancellationToken cancellationToken)
        {
            var ProjectType = await _unitOfWork.Repository<ProjectType>().Get(request.ProjectTypeId);

            if (ProjectType == null)
                throw new NotFoundException(nameof(ProjectType), request.ProjectTypeId);


            try
            {
                await _unitOfWork.Repository<ProjectType>().Delete(ProjectType);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ProjectTypeId);
            }

            return Unit.Value;
        }
    }
}
