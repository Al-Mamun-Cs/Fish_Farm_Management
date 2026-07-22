using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Commands
{
    public class DeleteShopHandCashWithdrowCommandHandler : IRequestHandler<DeleteShopHandCashWithdrowCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShopHandCashWithdrowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShopHandCashWithdrowCommand request, CancellationToken cancellationToken)
        {
            var ShopHandCashWithdrow = await _unitOfWork.Repository<ShopHandCashWithdrow>().Get(request.ShopHandCashWithdrowId);

            if (ShopHandCashWithdrow == null)
                throw new NotFoundException(nameof(ShopHandCashWithdrow), request.ShopHandCashWithdrowId);


            try
            {
                await _unitOfWork.Repository<ShopHandCashWithdrow>().Delete(ShopHandCashWithdrow);
                var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopHandCashWithdrow.WarehouseId ?? 0);
                warehouse.CashInHand += Convert.ToInt64(ShopHandCashWithdrow.TransferAmount);
                await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ShopHandCashWithdrowId);
            }

            return Unit.Value;
        }
    }
}
