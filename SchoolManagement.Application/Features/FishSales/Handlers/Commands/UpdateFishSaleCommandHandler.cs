using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FishSales.Requests.Commands;
using SchoolManagement.Application.DTOs.FishSales.Validators;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Commands
{
    public class UpdateFishSaleCommandHandler : IRequestHandler<UpdateFishSaleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFishSaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFishSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFishSaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FishSaleDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FishSale = await _unitOfWork.Repository<FishSale>().Get(request.FishSaleDto.FishSaleId);

            if (FishSale is null)
                throw new NotFoundException(nameof(FishSale), request.FishSaleDto.FishSaleId);

            _mapper.Map(request.FishSaleDto, FishSale);

            await _unitOfWork.Repository<FishSale>().Update(FishSale);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
