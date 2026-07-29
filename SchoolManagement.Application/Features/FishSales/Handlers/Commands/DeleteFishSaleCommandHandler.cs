using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FishSales.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Commands
{
    public class DeleteFishSaleCommandHandler : IRequestHandler<DeleteFishSaleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFishSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFishSaleCommand request, CancellationToken cancellationToken)
        {
            var FishSale = await _unitOfWork.Repository<FishSale>().Get(request.FishSaleId);

            if (FishSale == null)
                throw new NotFoundException(nameof(FishSale), request.FishSaleId);


            try
            {
                await _unitOfWork.Repository<FishSale>().Delete(FishSale);

                if (FishSale.SupplierId != null)
                {
                    var supplier = await _unitOfWork.Repository<Supplier>().Get(FishSale?.SupplierId ?? 0);
                    supplier.TotalDueAmount -= (FishSale.SaleDueAmount);
                    await _unitOfWork.Repository<Supplier>().Update(supplier);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FishSale?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (FishSale.SalePaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FishSale?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (FishSale.SalePaidAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FishSaleId);
            }

            return Unit.Value;
        }
    }
}
