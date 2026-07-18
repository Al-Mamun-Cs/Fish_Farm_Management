using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SupplierTypes.Handlers.Commands
{
    public class DeleteSupplierTypeCommandHandler : IRequestHandler<DeleteSupplierTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSupplierTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSupplierTypeCommand request, CancellationToken cancellationToken)
        {
            var SupplierType = await _unitOfWork.Repository<SupplierType>().Get(request.SupplierTypeId);

            if (SupplierType == null)
                throw new NotFoundException(nameof(SupplierType), request.SupplierTypeId);


            try
            {
                await _unitOfWork.Repository<SupplierType>().Delete(SupplierType);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.SupplierTypeId);
            }

            return Unit.Value;
        }
    }
}
