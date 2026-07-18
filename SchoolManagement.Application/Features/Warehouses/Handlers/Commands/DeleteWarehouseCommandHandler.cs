using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Warehouses.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Commands
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteWarehouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var Warehouse = await _unitOfWork.Repository<Warehouse>().Get(request.WarehouseId);

            if (Warehouse == null)
                throw new NotFoundException(nameof(Warehouse), request.WarehouseId);

            
            try
            {
                await _unitOfWork.Repository<Warehouse>().Delete(Warehouse);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.WarehouseId);
            }

            return Unit.Value;
        }
    }
}
