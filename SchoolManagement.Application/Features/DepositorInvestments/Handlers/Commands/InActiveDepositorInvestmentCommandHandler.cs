using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Commands
{
    public class InActiveDepositorInvestmentCommandHandler : IRequestHandler<InActiveDepositorInvestmentCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveDepositorInvestmentCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveDepositorInvestmentCommand request, CancellationToken cancellationToken)
        {
            var DepositorInvestment = await _unitOfWork.Repository<DepositorInvestment>().Get(request.DepositorInvestmentId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            DepositorInvestment.ApproveStatus = 1;
            DepositorInvestment.ApproveDate = DateTime.Now;
            DepositorInvestment.ApproveBy = username;

            if (DepositorInvestment == null)
                throw new NotFoundException(nameof(DepositorInvestment), request.DepositorInvestmentId);

            await _unitOfWork.Repository<DepositorInvestment>().Update(DepositorInvestment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
