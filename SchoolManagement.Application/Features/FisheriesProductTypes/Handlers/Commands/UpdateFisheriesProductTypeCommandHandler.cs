using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands;
using SchoolManagement.Application.DTOs.FisheriesProductTypes.Validators;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Commands
{
    public class UpdateFisheriesProductTypeCommandHandler : IRequestHandler<UpdateFisheriesProductTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFisheriesProductTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFisheriesProductTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFisheriesProductTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesProductTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FisheriesProductType = await _unitOfWork.Repository<FisheriesProductType>().Get(request.FisheriesProductTypeDto.FisheriesProductTypeId);

            if (FisheriesProductType is null)
                throw new NotFoundException(nameof(FisheriesProductType), request.FisheriesProductTypeDto.FisheriesProductTypeId);

            _mapper.Map(request.FisheriesProductTypeDto, FisheriesProductType);

            await _unitOfWork.Repository<FisheriesProductType>().Update(FisheriesProductType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
