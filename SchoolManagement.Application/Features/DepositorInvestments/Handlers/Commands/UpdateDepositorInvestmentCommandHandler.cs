using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Application.DTOs.DepositorInvestments.Validators;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Commands
{
    public class UpdateDepositorInvestmentCommandHandler : IRequestHandler<UpdateDepositorInvestmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepositorInvestmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDepositorInvestmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDepositorInvestmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorInvestmentDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DepositorInvestment = await _unitOfWork.Repository<DepositorInvestment>().Get(request.DepositorInvestmentDto.DepositorInvestmentId);

            if (DepositorInvestment is null)
                throw new NotFoundException(nameof(DepositorInvestment), request.DepositorInvestmentDto.DepositorInvestmentId);

            _mapper.Map(request.DepositorInvestmentDto, DepositorInvestment);

            await _unitOfWork.Repository<DepositorInvestment>().Update(DepositorInvestment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
