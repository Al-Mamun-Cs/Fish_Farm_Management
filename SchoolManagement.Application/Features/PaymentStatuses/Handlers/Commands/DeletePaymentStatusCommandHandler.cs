using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PaymentStatuses.Handlers.Commands
{
    public class DeletePaymentStatusCommandHandler : IRequestHandler<DeletePaymentStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePaymentStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var PaymentStatus = await _unitOfWork.Repository<PaymentStatus>().Get(request.PaymentStatusId);

            if (PaymentStatus == null)
                throw new NotFoundException(nameof(PaymentStatus), request.PaymentStatusId);

            
            try
            {
                await _unitOfWork.Repository<PaymentStatus>().Delete(PaymentStatus);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.PaymentStatusId);
            }

            return Unit.Value;
        }
    }
}
