using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Commands
{
    public class InActiveDepositorInstallmentCommandHandler : IRequestHandler<InActiveDepositorInstallmentCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveDepositorInstallmentCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveDepositorInstallmentCommand request, CancellationToken cancellationToken)
        {
            var DepositorInstallment = await _unitOfWork.Repository<DepositorInstallment>().Get(request.DepositorInstallmentId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            DepositorInstallment.ApproveStatus = 1;
            DepositorInstallment.ApproveDate = DateTime.Now;
            DepositorInstallment.ApproveBy = username;

            if (DepositorInstallment == null)
                throw new NotFoundException(nameof(DepositorInstallment), request.DepositorInstallmentId);

            await _unitOfWork.Repository<DepositorInstallment>().Update(DepositorInstallment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
