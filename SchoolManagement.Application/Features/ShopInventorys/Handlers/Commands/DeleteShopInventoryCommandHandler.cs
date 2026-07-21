using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Commands
{
    public class DeleteShopInventoryCommandHandler : IRequestHandler<DeleteShopInventoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShopInventoryDetail> _ShopInventoryDetailRepository;

        public DeleteShopInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ShopInventoryDetail> ShopInventoryDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ShopInventoryDetailRepository = ShopInventoryDetailRepository;
        }

        public async Task<Unit> Handle(DeleteShopInventoryCommand request, CancellationToken cancellationToken)
        {
            var ShopInventory = await _unitOfWork.Repository<ShopInventory>().Get(request.ShopInventoryId);

            if (ShopInventory == null)
                throw new NotFoundException(nameof(ShopInventory), request.ShopInventoryId);


            try
            {
                var supplier = await _unitOfWork.Repository<Supplier>().Get(ShopInventory.SupplierId ?? 0);

                if (supplier != null)
                {
                    supplier.TotalDueAmount -= ShopInventory.DueAmount;

                    await _unitOfWork.Repository<Supplier>().Update(supplier);
                }
                var paymentStatus = await _unitOfWork.Repository<PaymentStatus>().Get(ShopInventory.PaymentStatusId ?? 0);
                if (paymentStatus != null && paymentStatus.PriorityNo == 1)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopInventory.WarehouseId ?? 0);

                    warehouse.CashAmount += Convert.ToInt64(ShopInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopInventory.WarehouseId ?? 0);

                    warehouse.BankBalance += Convert.ToInt64(ShopInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }

                // Get the details first
                var detailRepo = _unitOfWork.Repository<ShopInventoryDetail>()
                                      .FilterWithInclude(x => x.ShopInventoryId == request.ShopInventoryId);
                foreach (var detail in detailRepo)
                {
                    _ShopInventoryDetailRepository.Delete(detail);
                }
                await _unitOfWork.Repository<ShopInventory>().Delete(ShopInventory);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ShopInventoryId);
            }

            return Unit.Value;
        }
    }
}
