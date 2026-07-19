using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Commands
{
    public class InActiveShopInventoryCommandHandler : IRequestHandler<InActiveShopInventoryCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveShopInventoryCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveShopInventoryCommand request, CancellationToken cancellationToken)
        {
            var ShopInventory = await _unitOfWork.Repository<ShopInventory>().Get(request.ShopInventoryId);


            ShopInventory.ApproveStatus = true;

            if (ShopInventory == null)
                throw new NotFoundException(nameof(ShopInventory), request.ShopInventoryId);

            await _unitOfWork.Repository<ShopInventory>().Update(ShopInventory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
