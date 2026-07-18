using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BloodGroups.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BloodGroups.Handlers.Commands
{
    public class DeleteBloodGroupCommandHandler : IRequestHandler<DeleteBloodGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBloodGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBloodGroupCommand request, CancellationToken cancellationToken)
        {
            var BloodGroup = await _unitOfWork.Repository<BloodGroup>().Get(request.BloodGroupId);

            if (BloodGroup == null)
                throw new NotFoundException(nameof(BloodGroup), request.BloodGroupId);


            try
            {
                await _unitOfWork.Repository<BloodGroup>().Delete(BloodGroup);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.BloodGroupId);
            }

            return Unit.Value;
        }
    }
}
