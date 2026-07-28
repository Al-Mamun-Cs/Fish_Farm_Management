using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Commands
{
    public class InActiveCompanyInvestorReturnCommandHandler : IRequestHandler<InActiveCompanyInvestorReturnCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveCompanyInvestorReturnCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveCompanyInvestorReturnCommand request, CancellationToken cancellationToken)
        {
            var CompanyInvestorReturn = await _unitOfWork.Repository<CompanyInvestorReturn>().Get(request.CompanyInvestorReturnId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            CompanyInvestorReturn.ApproveStatus = 1;
            CompanyInvestorReturn.ApproveDate = DateTime.Now;
            CompanyInvestorReturn.ApproveBy = username;

            if (CompanyInvestorReturn == null)
                throw new NotFoundException(nameof(CompanyInvestorReturn), request.CompanyInvestorReturnId);

            await _unitOfWork.Repository<CompanyInvestorReturn>().Update(CompanyInvestorReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
