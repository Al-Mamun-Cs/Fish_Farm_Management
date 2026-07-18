using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Suppliers.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Commands
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var Supplier = await _unitOfWork.Repository<Supplier>().Get(request.SupplierId);

            if (Supplier == null)
                throw new NotFoundException(nameof(Supplier), request.SupplierId);

            
            try
            {
                await _unitOfWork.Repository<Supplier>().Delete(Supplier);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.SupplierId);
            }

            return Unit.Value;
        }
    }
}
