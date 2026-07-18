using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Banks.Requests.Commands;
using SchoolManagement.Application.DTOs.Banks.Validators;

namespace SchoolManagement.Application.Features.Banks.Handlers.Commands
{
    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBankDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BankDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Bank = await _unitOfWork.Repository<Bank>().Get(request.BankDto.BankId);

            if (Bank is null)
                throw new NotFoundException(nameof(Bank), request.BankDto.BankId);

            _mapper.Map(request.BankDto, Bank);

            await _unitOfWork.Repository<Bank>().Update(Bank);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
