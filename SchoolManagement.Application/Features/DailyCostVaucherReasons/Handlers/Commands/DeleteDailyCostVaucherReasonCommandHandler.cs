using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Commands
{
    public class DeleteDailyCostVaucherReasonCommandHandler : IRequestHandler<DeleteDailyCostVaucherReasonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDailyCostVaucherReasonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDailyCostVaucherReasonCommand request, CancellationToken cancellationToken)
        {
            var DailyCostVaucherReason = await _unitOfWork.Repository<DailyCostVaucherReason>().Get(request.DailyCostVaucherReasonId);

            if (DailyCostVaucherReason == null)
                throw new NotFoundException(nameof(DailyCostVaucherReason), request.DailyCostVaucherReasonId);


            try
            {
                await _unitOfWork.Repository<DailyCostVaucherReason>().Delete(DailyCostVaucherReason);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DailyCostVaucherReasonId);
            }

            return Unit.Value;
        }
    }
}
