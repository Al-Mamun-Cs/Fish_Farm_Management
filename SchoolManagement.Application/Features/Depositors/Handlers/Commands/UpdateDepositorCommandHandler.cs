using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Depositors.Requests.Commands;
using SchoolManagement.Application.DTOs.Depositors.Validators;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Commands
{
    public class UpdateDepositorCommandHandler : IRequestHandler<UpdateDepositorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepositorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDepositorCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDepositorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Depositor = await _unitOfWork.Repository<Depositor>().Get(request.DepositorDto.DepositorId);

            if (Depositor is null)
                throw new NotFoundException(nameof(Depositor), request.DepositorDto.DepositorId);

            _mapper.Map(request.DepositorDto, Depositor);

            await _unitOfWork.Repository<Depositor>().Update(Depositor);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
