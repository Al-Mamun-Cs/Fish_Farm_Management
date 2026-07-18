using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Upozilas.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Commands
{
    public class DeleteUpozilaCommandHandler : IRequestHandler<DeleteUpozilaCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUpozilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUpozilaCommand request, CancellationToken cancellationToken)
        {
            var Upozila = await _unitOfWork.Repository<Upozila>().Get(request.UpazilaId);

            if (Upozila == null)
                throw new NotFoundException(nameof(Upozila), request.UpazilaId);


            try
            {
                await _unitOfWork.Repository<Upozila>().Delete(Upozila);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.UpazilaId);
            }

            return Unit.Value;
        }
    }
}
