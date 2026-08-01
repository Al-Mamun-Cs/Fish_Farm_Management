using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Commands
{
    public class DeleteDepositorInstallmentCommandHandler : IRequestHandler<DeleteDepositorInstallmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepositorInstallmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDepositorInstallmentCommand request, CancellationToken cancellationToken)
        {
            var DepositorInstallment = await _unitOfWork.Repository<DepositorInstallment>().Get(request.DepositorInstallmentId);

            if (DepositorInstallment == null)
                throw new NotFoundException(nameof(DepositorInstallment), request.DepositorInstallmentId);


            try
            {
                await _unitOfWork.Repository<DepositorInstallment>().Delete(DepositorInstallment);

               

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DepositorInstallmentId);
            }

            return Unit.Value;
        }
    }
}
