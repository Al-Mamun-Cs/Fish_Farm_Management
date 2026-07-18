using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Districts.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Districts.Handlers.Commands
{
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            var District = await _unitOfWork.Repository<District>().Get(request.DistrictId);

            if (District == null)
                throw new NotFoundException(nameof(District), request.DistrictId);


            try
            {
                await _unitOfWork.Repository<District>().Delete(District);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.DistrictId);
            }

            return Unit.Value;
        }
    }
}
