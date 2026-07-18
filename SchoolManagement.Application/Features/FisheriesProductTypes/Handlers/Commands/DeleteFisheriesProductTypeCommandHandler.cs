using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Commands
{
    public class DeleteFisheriesProductTypeCommandHandler : IRequestHandler<DeleteFisheriesProductTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFisheriesProductTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFisheriesProductTypeCommand request, CancellationToken cancellationToken)
        {
            var FisheriesProductType = await _unitOfWork.Repository<FisheriesProductType>().Get(request.FisheriesProductTypeId);

            if (FisheriesProductType == null)
                throw new NotFoundException(nameof(FisheriesProductType), request.FisheriesProductTypeId);


            try
            {
                await _unitOfWork.Repository<FisheriesProductType>().Delete(FisheriesProductType);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FisheriesProductTypeId);
            }

            return Unit.Value;
        }
    }
}
