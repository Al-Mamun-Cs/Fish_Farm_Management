using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Commands
{
    public class InActiveShopHandCashWithdrowCommandHandler : IRequestHandler<InActiveShopHandCashWithdrowCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveShopHandCashWithdrowCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveShopHandCashWithdrowCommand request, CancellationToken cancellationToken)
        {
            var ShopHandCashWithdrow = await _unitOfWork.Repository<ShopHandCashWithdrow>().Get(request.ShopHandCashWithdrowId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            ShopHandCashWithdrow.ApproveStatus = 1;
            ShopHandCashWithdrow.ApproveDate = DateTime.Now;
            ShopHandCashWithdrow.ApproveBy = username;

            if (ShopHandCashWithdrow == null)
                throw new NotFoundException(nameof(ShopHandCashWithdrow), request.ShopHandCashWithdrowId);

            await _unitOfWork.Repository<ShopHandCashWithdrow>().Update(ShopHandCashWithdrow);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
