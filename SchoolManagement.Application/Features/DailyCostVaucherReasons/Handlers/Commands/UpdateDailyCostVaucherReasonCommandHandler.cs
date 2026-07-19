using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons.Validators;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Commands
{
    public class UpdateDailyCostVaucherReasonCommandHandler : IRequestHandler<UpdateDailyCostVaucherReasonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDailyCostVaucherReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDailyCostVaucherReasonCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDailyCostVaucherReasonDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DailyCostVaucherReasonDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DailyCostVaucherReason = await _unitOfWork.Repository<DailyCostVaucherReason>().Get(request.DailyCostVaucherReasonDto.DailyCostVaucherReasonId);

            if (DailyCostVaucherReason is null)
                throw new NotFoundException(nameof(DailyCostVaucherReason), request.DailyCostVaucherReasonDto.DailyCostVaucherReasonId);

            _mapper.Map(request.DailyCostVaucherReasonDto, DailyCostVaucherReason);

            await _unitOfWork.Repository<DailyCostVaucherReason>().Update(DailyCostVaucherReason);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
