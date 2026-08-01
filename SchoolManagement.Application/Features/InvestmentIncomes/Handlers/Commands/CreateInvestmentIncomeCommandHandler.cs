using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.InvestmentIncomes.Validators;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Commands
{
    public class CreateInvestmentIncomeCommandHandler : IRequestHandler<CreateInvestmentIncomeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInvestmentIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateInvestmentIncomeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateInvestmentIncomeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.InvestmentIncomeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var InvestmentIncome = _mapper.Map<InvestmentIncome>(request.InvestmentIncomeDto);
                InvestmentIncome = await _unitOfWork.Repository<InvestmentIncome>().Add(InvestmentIncome);
                
                    

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = InvestmentIncome.InvestmentIncomeId;
            }

            return response;
        }
    }
}
