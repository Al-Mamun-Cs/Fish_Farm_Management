using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Commands;
using SchoolManagement.Application.DTOs.SupplierTypes.Validators;

namespace SchoolManagement.Application.Features.SupplierTypes.Handlers.Commands
{
    public class UpdateSupplierTypeCommandHandler : IRequestHandler<UpdateSupplierTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSupplierTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSupplierTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SupplierTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SupplierType = await _unitOfWork.Repository<SupplierType>().Get(request.SupplierTypeDto.SupplierTypeId);

            if (SupplierType is null)
                throw new NotFoundException(nameof(SupplierType), request.SupplierTypeDto.SupplierTypeId);

            _mapper.Map(request.SupplierTypeDto, SupplierType);

            await _unitOfWork.Repository<SupplierType>().Update(SupplierType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
