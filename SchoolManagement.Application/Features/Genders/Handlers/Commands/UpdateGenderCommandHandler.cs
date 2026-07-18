using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Genders.Requests.Commands;
using SchoolManagement.Application.DTOs.Genders.Validators;

namespace SchoolManagement.Application.Features.Genders.Handlers.Commands
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateGenderDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GenderDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Gender = await _unitOfWork.Repository<Gender>().Get(request.GenderDto.GenderId);

            if (Gender is null)
                throw new NotFoundException(nameof(Gender), request.GenderDto.GenderId);

            _mapper.Map(request.GenderDto, Gender);

            await _unitOfWork.Repository<Gender>().Update(Gender);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
