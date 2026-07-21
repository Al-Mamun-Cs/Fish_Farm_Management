using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Commands
{
    public class DeleteFisheriesInventoryCommandHandler : IRequestHandler<DeleteFisheriesInventoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesInventoryDetail> _FisheriesInventoryDetailRepository;

        public DeleteFisheriesInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<FisheriesInventoryDetail> FisheriesInventoryDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _FisheriesInventoryDetailRepository = FisheriesInventoryDetailRepository;
        }

        public async Task<Unit> Handle(DeleteFisheriesInventoryCommand request, CancellationToken cancellationToken)
        {
            var FisheriesInventory = await _unitOfWork.Repository<FisheriesInventory>().Get(request.FisheriesInventoryId);

            if (FisheriesInventory == null)
                throw new NotFoundException(nameof(FisheriesInventory), request.FisheriesInventoryId);


            try
            {
                var supplier = await _unitOfWork.Repository<Supplier>().Get(FisheriesInventory.SupplierId ?? 0);

                if (supplier != null)
                {
                    supplier.TotalDueAmount -= FisheriesInventory.DueAmount;

                    await _unitOfWork.Repository<Supplier>().Update(supplier);
                }
                var paymentStatus = await _unitOfWork.Repository<PaymentStatus>().Get(FisheriesInventory.PaymentStatusId ?? 0);
                if (paymentStatus != null && paymentStatus.PriorityNo == 1)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FisheriesInventory.WarehouseId ?? 0);

                    warehouse.CashAmount += Convert.ToInt64(FisheriesInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FisheriesInventory.WarehouseId ?? 0);

                    warehouse.BankBalance += Convert.ToInt64(FisheriesInventory.PaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }
                // Get the details first
                var detailRepo = _unitOfWork.Repository<FisheriesInventoryDetail>()
                                      .FilterWithInclude(x => x.FisheriesInventoryId == request.FisheriesInventoryId);
                foreach (var detail in detailRepo)
                {
                    _FisheriesInventoryDetailRepository.Delete(detail);
                }
                await _unitOfWork.Repository<FisheriesInventory>().Delete(FisheriesInventory);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FisheriesInventoryId);
            }

            return Unit.Value;
        }
    }
}
