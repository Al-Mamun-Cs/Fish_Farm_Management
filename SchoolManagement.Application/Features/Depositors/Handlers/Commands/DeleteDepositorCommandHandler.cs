using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Depositors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Commands
{
    public class DeleteDepositorCommandHandler : IRequestHandler<DeleteDepositorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepositorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDepositorCommand request, CancellationToken cancellationToken)
        {
            var Depositor = await _unitOfWork.Repository<Depositor>().Get(request.DepositorId);

            if (Depositor == null)
                throw new NotFoundException(nameof(Depositor), request.DepositorId);


            try
            {
                await _unitOfWork.Repository<Depositor>().Delete(Depositor);

                

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DepositorId);
            }

            return Unit.Value;
        }
    }
}
