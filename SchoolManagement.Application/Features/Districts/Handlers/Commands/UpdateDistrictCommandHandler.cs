using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Districts.Requests.Commands;
using SchoolManagement.Application.DTOs.Districts.Validators;

namespace SchoolManagement.Application.Features.Districts.Handlers.Commands
{
    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDistrictDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DistrictDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var District = await _unitOfWork.Repository<District>().Get(request.DistrictDto.DistrictId);

            if (District is null)
                throw new NotFoundException(nameof(District), request.DistrictDto.DistrictId);

            _mapper.Map(request.DistrictDto, District);

            await _unitOfWork.Repository<District>().Update(District);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
