using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows.Validators;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Commands
{
    public class UpdateShopHandCashWithdrowCommandHandler : IRequestHandler<UpdateShopHandCashWithdrowCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShopHandCashWithdrowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShopHandCashWithdrowCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShopHandCashWithdrowDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShopHandCashWithdrowDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShopHandCashWithdrow = await _unitOfWork.Repository<ShopHandCashWithdrow>().Get(request.ShopHandCashWithdrowDto.ShopHandCashWithdrowId);

            if (ShopHandCashWithdrow is null)
                throw new NotFoundException(nameof(ShopHandCashWithdrow), request.ShopHandCashWithdrowDto.ShopHandCashWithdrowId);

            _mapper.Map(request.ShopHandCashWithdrowDto, ShopHandCashWithdrow);

            await _unitOfWork.Repository<ShopHandCashWithdrow>().Update(ShopHandCashWithdrow);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
