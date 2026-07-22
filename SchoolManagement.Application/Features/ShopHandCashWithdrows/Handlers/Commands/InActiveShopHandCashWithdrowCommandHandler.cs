using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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


            ShopHandCashWithdrow.ApproveStatus = 1;

            if (ShopHandCashWithdrow == null)
                throw new NotFoundException(nameof(ShopHandCashWithdrow), request.ShopHandCashWithdrowId);

            await _unitOfWork.Repository<ShopHandCashWithdrow>().Update(ShopHandCashWithdrow);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
