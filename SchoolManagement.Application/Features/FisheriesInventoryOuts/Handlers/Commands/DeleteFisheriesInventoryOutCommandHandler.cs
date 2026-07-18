using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Commands
{
    public class DeleteFisheriesInventoryOutCommandHandler : IRequestHandler<DeleteFisheriesInventoryOutCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFisheriesInventoryOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFisheriesInventoryOutCommand request, CancellationToken cancellationToken)
        {
            var FisheriesInventoryOut = await _unitOfWork.Repository<FisheriesInventoryOut>().Get(request.FisheriesInventoryOutId);

            if (FisheriesInventoryOut == null)
                throw new NotFoundException(nameof(FisheriesInventoryOut), request.FisheriesInventoryOutId);


            try
            {
                await _unitOfWork.Repository<FisheriesInventoryOut>().Delete(FisheriesInventoryOut);
                var fInventoryDetail = await _unitOfWork.Repository<FisheriesInventoryDetail>().Get(FisheriesInventoryOut?.FisheriesInventoryDetailId ?? 0);
                fInventoryDetail.AvailableQty += (FisheriesInventoryOut.UseQty);
                await _unitOfWork.Repository<FisheriesInventoryDetail>().Update(fInventoryDetail);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FisheriesInventoryOutId);
            }

            return Unit.Value;
        }
    }
}
