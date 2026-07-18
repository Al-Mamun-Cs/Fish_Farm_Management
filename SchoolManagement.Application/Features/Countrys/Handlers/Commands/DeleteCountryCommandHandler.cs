using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Countrys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Commands
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var Country = await _unitOfWork.Repository<Country>().Get(request.CountryId);

            if (Country == null)
                throw new NotFoundException(nameof(Country), request.CountryId);


            try
            {
                await _unitOfWork.Repository<Country>().Delete(Country);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.CountryId);
            }

            return Unit.Value;
        }
    }
}
