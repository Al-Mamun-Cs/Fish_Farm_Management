using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Commands
{
    public class DeleteFisheriesProductReturnCommandHandler : IRequestHandler<DeleteFisheriesProductReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFisheriesProductReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFisheriesProductReturnCommand request, CancellationToken cancellationToken)
        {
            var FisheriesProductReturn = await _unitOfWork.Repository<FisheriesProductReturn>().Get(request.FisheriesProductReturnId);

            if (FisheriesProductReturn == null)
                throw new NotFoundException(nameof(FisheriesProductReturn), request.FisheriesProductReturnId);


            try
            {
                await _unitOfWork.Repository<FisheriesProductReturn>().Delete(FisheriesProductReturn);

                if (FisheriesProductReturn.PaymentReturnType == 1)
                {
                    var supplier = await _unitOfWork.Repository<Supplier>().Get(FisheriesProductReturn?.SupplierId ?? 0);
                    supplier.TotalDueAmount += (FisheriesProductReturn.ReturnAmount);
                    await _unitOfWork.Repository<Supplier>().Update(supplier);

                    var fd = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesProductReturn?.FisheriesInventoryDetailId ?? 0);
                    fd.AvailableQty += (FisheriesProductReturn.ReturnQty);
                    await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fd);

                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(FisheriesProductReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (FisheriesProductReturn.ReturnAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                    var fd = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesProductReturn?.FisheriesInventoryDetailId ?? 0);
                    fd.AvailableQty += (FisheriesProductReturn.ReturnQty);
                    await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fd);
                }

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FisheriesProductReturnId);
            }

            return Unit.Value;
        }
    }
}
