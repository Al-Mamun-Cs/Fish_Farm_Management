using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Ponds.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Commands
{
    public class DeletePondCommandHandler : IRequestHandler<DeletePondCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePondCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePondCommand request, CancellationToken cancellationToken)
        {
            var Pond = await _unitOfWork.Repository<Pond>().Get(request.PondId);

            if (Pond == null)
                throw new NotFoundException(nameof(Pond), request.PondId);


            try
            {
                await _unitOfWork.Repository<Pond>().Delete(Pond);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.PondId);
            }

            return Unit.Value;
        }
    }
}
