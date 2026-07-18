using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Commands
{
    public class DeleteFiscalyearCommandHandler : IRequestHandler<DeleteFiscalyearCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFiscalyearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFiscalyearCommand request, CancellationToken cancellationToken)
        {
            var Fiscalyear = await _unitOfWork.Repository<Fiscalyear>().Get(request.FiscalyearId);

            if (Fiscalyear == null)
                throw new NotFoundException(nameof(Fiscalyear), request.FiscalyearId);


            try
            {
                await _unitOfWork.Repository<Fiscalyear>().Delete(Fiscalyear);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.FiscalyearId);
            }

            return Unit.Value;
        }
    }
}
