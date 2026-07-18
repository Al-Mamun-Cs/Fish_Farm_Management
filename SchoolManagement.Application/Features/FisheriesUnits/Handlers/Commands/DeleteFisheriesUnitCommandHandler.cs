using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Commands
{
    public class DeleteFisheriesUnitCommandHandler : IRequestHandler<DeleteFisheriesUnitCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFisheriesUnitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFisheriesUnitCommand request, CancellationToken cancellationToken)
        {
            var FisheriesUnit = await _unitOfWork.Repository<FisheriesUnit>().Get(request.FisheriesUnitId);

            if (FisheriesUnit == null)
                throw new NotFoundException(nameof(FisheriesUnit), request.FisheriesUnitId);


            try
            {
                await _unitOfWork.Repository<FisheriesUnit>().Delete(FisheriesUnit);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FisheriesUnitId);
            }

            return Unit.Value;
        }
    }
}
