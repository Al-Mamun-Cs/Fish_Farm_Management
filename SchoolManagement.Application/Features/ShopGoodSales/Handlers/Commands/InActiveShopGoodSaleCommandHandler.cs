using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Commands
{
    public class InActiveShopGoodSaleCommandHandler : IRequestHandler<InActiveShopGoodSaleCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveShopGoodSaleCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveShopGoodSaleCommand request, CancellationToken cancellationToken)
        {
            var ShopGoodSale = await _unitOfWork.Repository<ShopGoodSale>().Get(request.ShopGoodSaleId);


            ShopGoodSale.ApproveStatus = 1;

            if (ShopGoodSale == null)
                throw new NotFoundException(nameof(ShopGoodSale), request.ShopGoodSaleId);

            await _unitOfWork.Repository<ShopGoodSale>().Update(ShopGoodSale);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
