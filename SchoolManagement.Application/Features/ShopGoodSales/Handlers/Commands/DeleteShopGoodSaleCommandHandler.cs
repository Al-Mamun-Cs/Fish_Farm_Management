using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Commands
{
    public class DeleteShopGoodSaleCommandHandler : IRequestHandler<DeleteShopGoodSaleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShopGoodSaleDetail> _ShopGoodSaleDetailRepository;

        public DeleteShopGoodSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ShopGoodSaleDetail> ShopGoodSaleDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ShopGoodSaleDetailRepository = ShopGoodSaleDetailRepository;
        }

        public async Task<Unit> Handle(DeleteShopGoodSaleCommand request, CancellationToken cancellationToken)
        {
            var ShopGoodSale = await _unitOfWork.Repository<ShopGoodSale>().Get(request.ShopGoodSaleId);

            if (ShopGoodSale == null)
                throw new NotFoundException(nameof(ShopGoodSale), request.ShopGoodSaleId);


            try
            {
                var supplier = await _unitOfWork.Repository<Supplier>().Get(ShopGoodSale.SupplierId ?? 0);

                if (supplier != null)
                {
                    supplier.TotalDueAmount -= ShopGoodSale.CustomerDueAmount;

                    await _unitOfWork.Repository<Supplier>().Update(supplier);
                }
                if (ShopGoodSale.PaymentStatusId == 1)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopGoodSale.WarehouseId ?? 0);

                    warehouse.CashAmount -= Convert.ToInt64(ShopGoodSale.CustomerPaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }

                // Get the details first
                var detailRepo = _unitOfWork.Repository<ShopGoodSaleDetail>()
                                      .FilterWithInclude(x => x.ShopGoodSaleId == request.ShopGoodSaleId);
                foreach (var detail in detailRepo)
                {
                    var inventoryDetail = await _unitOfWork.Repository<ShopInventoryDetail>().Get(detail.ShopInventoryDetailId ?? 0);
                    inventoryDetail.AvailableQty += Convert.ToInt64(detail.SaleQty);
                    await _unitOfWork.Repository<ShopInventoryDetail>().Update(inventoryDetail);

                    _ShopGoodSaleDetailRepository.Delete(detail);
                }
                await _unitOfWork.Repository<ShopGoodSale>().Delete(ShopGoodSale);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ShopGoodSaleId);
            }

            return Unit.Value;
        }
    }
}
