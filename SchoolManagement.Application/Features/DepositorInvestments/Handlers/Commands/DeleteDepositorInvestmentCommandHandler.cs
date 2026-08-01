using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Commands
{
    public class DeleteDepositorInvestmentCommandHandler : IRequestHandler<DeleteDepositorInvestmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepositorInvestmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDepositorInvestmentCommand request, CancellationToken cancellationToken)
        {
            var DepositorInvestment = await _unitOfWork.Repository<DepositorInvestment>().Get(request.DepositorInvestmentId);

            if (DepositorInvestment == null)
                throw new NotFoundException(nameof(DepositorInvestment), request.DepositorInvestmentId);


            try
            {
                await _unitOfWork.Repository<DepositorInvestment>().Delete(DepositorInvestment);

                

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DepositorInvestmentId);
            }

            return Unit.Value;
        }
    }
}
