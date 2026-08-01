using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Application.DTOs.DepositorInstallments.Validators;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Commands
{
    public class UpdateDepositorInstallmentCommandHandler : IRequestHandler<UpdateDepositorInstallmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepositorInstallmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDepositorInstallmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDepositorInstallmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorInstallmentDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DepositorInstallment = await _unitOfWork.Repository<DepositorInstallment>().Get(request.DepositorInstallmentDto.DepositorInstallmentId);

            if (DepositorInstallment is null)
                throw new NotFoundException(nameof(DepositorInstallment), request.DepositorInstallmentDto.DepositorInstallmentId);

            _mapper.Map(request.DepositorInstallmentDto, DepositorInstallment);

            await _unitOfWork.Repository<DepositorInstallment>().Update(DepositorInstallment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
