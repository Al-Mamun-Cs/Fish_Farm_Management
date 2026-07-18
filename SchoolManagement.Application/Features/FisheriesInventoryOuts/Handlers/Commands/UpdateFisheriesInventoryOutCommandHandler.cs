using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts.Validators;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Commands
{
    public class UpdateFisheriesInventoryOutCommandHandler : IRequestHandler<UpdateFisheriesInventoryOutCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFisheriesInventoryOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFisheriesInventoryOutCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFisheriesInventoryOutDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesInventoryOutDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FisheriesInventoryOut = await _unitOfWork.Repository<FisheriesInventoryOut>().Get(request.FisheriesInventoryOutDto.FisheriesInventoryOutId);

            if (FisheriesInventoryOut is null)
                throw new NotFoundException(nameof(FisheriesInventoryOut), request.FisheriesInventoryOutDto.FisheriesInventoryOutId);

            _mapper.Map(request.FisheriesInventoryOutDto, FisheriesInventoryOut);

            await _unitOfWork.Repository<FisheriesInventoryOut>().Update(FisheriesInventoryOut);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
