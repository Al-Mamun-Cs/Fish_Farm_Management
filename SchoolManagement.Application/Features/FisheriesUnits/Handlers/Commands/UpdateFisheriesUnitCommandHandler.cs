using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands;
using SchoolManagement.Application.DTOs.FisheriesUnits.Validators;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Commands
{
    public class UpdateFisheriesUnitCommandHandler : IRequestHandler<UpdateFisheriesUnitCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFisheriesUnitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFisheriesUnitCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFisheriesUnitDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesUnitDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FisheriesUnit = await _unitOfWork.Repository<FisheriesUnit>().Get(request.FisheriesUnitDto.FisheriesUnitId);

            if (FisheriesUnit is null)
                throw new NotFoundException(nameof(FisheriesUnit), request.FisheriesUnitDto.FisheriesUnitId);

            _mapper.Map(request.FisheriesUnitDto, FisheriesUnit);

            await _unitOfWork.Repository<FisheriesUnit>().Update(FisheriesUnit);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
