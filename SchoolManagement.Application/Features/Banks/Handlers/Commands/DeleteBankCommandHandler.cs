using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Banks.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Banks.Handlers.Commands
{
    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            var Bank = await _unitOfWork.Repository<Bank>().Get(request.BankId);

            if (Bank == null)
                throw new NotFoundException(nameof(Bank), request.BankId);


            try
            {
                await _unitOfWork.Repository<Bank>().Delete(Bank);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.BankId);
            }

            return Unit.Value;
        }
    }
}
