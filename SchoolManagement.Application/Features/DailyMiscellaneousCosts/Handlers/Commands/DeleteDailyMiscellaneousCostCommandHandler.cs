using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Commands
{
    public class DeleteDailyMiscellaneousCostCommandHandler : IRequestHandler<DeleteDailyMiscellaneousCostCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDailyMiscellaneousCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDailyMiscellaneousCostCommand request, CancellationToken cancellationToken)
        {
            var DailyMiscellaneousCost = await _unitOfWork.Repository<DailyMiscellaneousCost>().Get(request.DailyMiscellaneousCostId);

            if (DailyMiscellaneousCost == null)
                throw new NotFoundException(nameof(DailyMiscellaneousCost), request.DailyMiscellaneousCostId);


            try
            {
                await _unitOfWork.Repository<DailyMiscellaneousCost>().Delete(DailyMiscellaneousCost);
                var warehouse = await _unitOfWork.Repository<Warehouse>().Get(DailyMiscellaneousCost?.WarehouseId ?? 0);
                warehouse.CashAmount += (DailyMiscellaneousCost.Amount);
                await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DailyMiscellaneousCostId);
            }

            return Unit.Value;
        }
    }
}
