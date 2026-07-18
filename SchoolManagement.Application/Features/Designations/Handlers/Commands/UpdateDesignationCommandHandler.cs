using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Designations.Requests.Commands;
using SchoolManagement.Application.DTOs.Designations.Validators;

namespace SchoolManagement.Application.Features.Designations.Handlers.Commands
{
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDesignationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DesignationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Designation = await _unitOfWork.Repository<Designation>().Get(request.DesignationDto.DesignationId);

            if (Designation is null)
                throw new NotFoundException(nameof(Designation), request.DesignationDto.DesignationId);

            _mapper.Map(request.DesignationDto, Designation);

            await _unitOfWork.Repository<Designation>().Update(Designation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
