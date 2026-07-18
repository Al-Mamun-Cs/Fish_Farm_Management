using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Upozilas.Requests.Commands;
using SchoolManagement.Application.DTOs.Upozilas.Validators;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Commands
{
    public class UpdateUpozilaCommandHandler : IRequestHandler<UpdateUpozilaCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUpozilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUpozilaCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUpozilaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpozilaDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Upozila = await _unitOfWork.Repository<Upozila>().Get(request.UpozilaDto.UpazilaId);

            if (Upozila is null)
                throw new NotFoundException(nameof(Upozila), request.UpozilaDto.UpazilaId);

            _mapper.Map(request.UpozilaDto, Upozila);

            await _unitOfWork.Repository<Upozila>().Update(Upozila);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
