using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Commands;
using SchoolManagement.Application.DTOs.Fiscalyears.Validators;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Commands
{
    public class UpdateFiscalyearCommandHandler : IRequestHandler<UpdateFiscalyearCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFiscalyearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFiscalyearCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFiscalyearDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FiscalyearDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Fiscalyear = await _unitOfWork.Repository<Fiscalyear>().Get(request.FiscalyearDto.FiscalyearId);

            if (Fiscalyear is null)
                throw new NotFoundException(nameof(Fiscalyear), request.FiscalyearDto.FiscalyearId);

            _mapper.Map(request.FiscalyearDto, Fiscalyear);

            await _unitOfWork.Repository<Fiscalyear>().Update(Fiscalyear);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
