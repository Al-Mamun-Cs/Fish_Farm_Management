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
