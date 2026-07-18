using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Designations.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Designations.Handlers.Commands
{
    public class DeleteDesignationCommandHandler : IRequestHandler<DeleteDesignationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var Designation = await _unitOfWork.Repository<Designation>().Get(request.DesignationId);

            if (Designation == null)
                throw new NotFoundException(nameof(Designation), request.DesignationId);


            try
            {
                await _unitOfWork.Repository<Designation>().Delete(Designation);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DesignationId);
            }

            return Unit.Value;
        }
    }
}
