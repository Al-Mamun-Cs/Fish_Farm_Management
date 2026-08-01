using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Commands
{
    public class DeleteInvestmentIncomeCommandHandler : IRequestHandler<DeleteInvestmentIncomeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteInvestmentIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteInvestmentIncomeCommand request, CancellationToken cancellationToken)
        {
            var InvestmentIncome = await _unitOfWork.Repository<InvestmentIncome>().Get(request.InvestmentIncomeId);

            if (InvestmentIncome == null)
                throw new NotFoundException(nameof(InvestmentIncome), request.InvestmentIncomeId);


            try
            {
                await _unitOfWork.Repository<InvestmentIncome>().Delete(InvestmentIncome);

                
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.InvestmentIncomeId);
            }

            return Unit.Value;
        }
    }
}
