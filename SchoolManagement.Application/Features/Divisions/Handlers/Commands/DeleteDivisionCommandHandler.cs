using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Divisions.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Divisions.Handlers.Commands
{
    public class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDivisionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDivisionCommand request, CancellationToken cancellationToken)
        {
            var Division = await _unitOfWork.Repository<Division>().Get(request.DivisionId);

            if (Division == null)
                throw new NotFoundException(nameof(Division), request.DivisionId);


            try
            {
                await _unitOfWork.Repository<Division>().Delete(Division);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DivisionId);
            }

            return Unit.Value;
        }
    }
}
