using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Countrys.Requests.Commands;
using SchoolManagement.Application.DTOs.Countrys.Validators;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Commands
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCountryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CountryDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Country = await _unitOfWork.Repository<Country>().Get(request.CountryDto.CountryId);

            if (Country is null)
                throw new NotFoundException(nameof(Country), request.CountryDto.CountryId);

            _mapper.Map(request.CountryDto, Country);

            await _unitOfWork.Repository<Country>().Update(Country);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
