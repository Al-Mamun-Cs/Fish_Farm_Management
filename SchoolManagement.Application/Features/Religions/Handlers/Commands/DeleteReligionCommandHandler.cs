using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Religions.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Religions.Handlers.Commands
{
    public class DeleteReligionCommandHandler : IRequestHandler<DeleteReligionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReligionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReligionCommand request, CancellationToken cancellationToken)
        {
            var Religion = await _unitOfWork.Repository<Religion>().Get(request.ReligionId);

            if (Religion == null)
                throw new NotFoundException(nameof(Religion), request.ReligionId);


            try
            {
                await _unitOfWork.Repository<Religion>().Delete(Religion);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.ReligionId);
            }

            return Unit.Value;
        }
    }
}
