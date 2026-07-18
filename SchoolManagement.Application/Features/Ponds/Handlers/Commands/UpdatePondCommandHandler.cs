using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Ponds.Requests.Commands;
using SchoolManagement.Application.DTOs.Ponds.Validators;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Commands
{
    public class UpdatePondCommandHandler : IRequestHandler<UpdatePondCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePondCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePondCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePondDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PondDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Pond = await _unitOfWork.Repository<Pond>().Get(request.PondDto.PondId);

            if (Pond is null)
                throw new NotFoundException(nameof(Pond), request.PondDto.PondId);

            _mapper.Map(request.PondDto, Pond);

            await _unitOfWork.Repository<Pond>().Update(Pond);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
