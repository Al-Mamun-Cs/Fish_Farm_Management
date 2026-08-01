using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands;
using SchoolManagement.Application.DTOs.InvestmentIncomes.Validators;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Commands
{
    public class UpdateInvestmentIncomeCommandHandler : IRequestHandler<UpdateInvestmentIncomeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInvestmentIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateInvestmentIncomeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateInvestmentIncomeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InvestmentIncomeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var InvestmentIncome = await _unitOfWork.Repository<InvestmentIncome>().Get(request.InvestmentIncomeDto.InvestmentIncomeId);

            if (InvestmentIncome is null)
                throw new NotFoundException(nameof(InvestmentIncome), request.InvestmentIncomeDto.InvestmentIncomeId);

            _mapper.Map(request.InvestmentIncomeDto, InvestmentIncome);

            await _unitOfWork.Repository<InvestmentIncome>().Update(InvestmentIncome);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
