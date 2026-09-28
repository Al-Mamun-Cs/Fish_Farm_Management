using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Commands;
using SchoolManagement.Application.DTOs.ProjectTypes.Validators;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Commands
{
    public class UpdateProjectTypeCommandHandler : IRequestHandler<UpdateProjectTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProjectTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ProjectType = await _unitOfWork.Repository<ProjectType>().Get(request.ProjectTypeDto.ProjectTypeId);

            if (ProjectType is null)
                throw new NotFoundException(nameof(ProjectType), request.ProjectTypeDto.ProjectTypeId);

            _mapper.Map(request.ProjectTypeDto, ProjectType);

            await _unitOfWork.Repository<ProjectType>().Update(ProjectType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
