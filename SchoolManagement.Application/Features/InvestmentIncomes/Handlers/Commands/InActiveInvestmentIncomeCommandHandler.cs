using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Commands
{
    public class InActiveInvestmentIncomeCommandHandler : IRequestHandler<InActiveInvestmentIncomeCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveInvestmentIncomeCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveInvestmentIncomeCommand request, CancellationToken cancellationToken)
        {
            var InvestmentIncome = await _unitOfWork.Repository<InvestmentIncome>().Get(request.InvestmentIncomeId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            InvestmentIncome.ApproveStatus = 1;
            InvestmentIncome.ApproveDate = DateTime.Now;
            InvestmentIncome.ApproveBy = username;

            if (InvestmentIncome == null)
                throw new NotFoundException(nameof(InvestmentIncome), request.InvestmentIncomeId);

            await _unitOfWork.Repository<InvestmentIncome>().Update(InvestmentIncome);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
