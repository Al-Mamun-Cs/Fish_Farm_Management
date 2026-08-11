using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands;
using SchoolManagement.Application.DTOs.FisheriesProductReturns.Validators;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Commands
{
    public class UpdateFisheriesProductReturnCommandHandler : IRequestHandler<UpdateFisheriesProductReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFisheriesProductReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFisheriesProductReturnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFisheriesProductReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesProductReturnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FisheriesProductReturn = await _unitOfWork.Repository<FisheriesProductReturn>().Get(request.FisheriesProductReturnDto.FisheriesProductReturnId);

            if (FisheriesProductReturn is null)
                throw new NotFoundException(nameof(FisheriesProductReturn), request.FisheriesProductReturnDto.FisheriesProductReturnId);

            _mapper.Map(request.FisheriesProductReturnDto, FisheriesProductReturn);

            await _unitOfWork.Repository<FisheriesProductReturn>().Update(FisheriesProductReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
