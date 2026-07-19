using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts.Validators;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Commands
{
    public class UpdateDailyMiscellaneousCostCommandHandler : IRequestHandler<UpdateDailyMiscellaneousCostCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDailyMiscellaneousCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDailyMiscellaneousCostCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDailyMiscellaneousCostDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DailyMiscellaneousCostDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DailyMiscellaneousCost = await _unitOfWork.Repository<DailyMiscellaneousCost>().Get(request.DailyMiscellaneousCostDto.DailyMiscellaneousCostId);

            if (DailyMiscellaneousCost is null)
                throw new NotFoundException(nameof(DailyMiscellaneousCost), request.DailyMiscellaneousCostDto.DailyMiscellaneousCostId);

            _mapper.Map(request.DailyMiscellaneousCostDto, DailyMiscellaneousCost);

            await _unitOfWork.Repository<DailyMiscellaneousCost>().Update(DailyMiscellaneousCost);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
