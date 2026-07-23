using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows.Validators;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Commands
{
    public class CreateShopHandCashWithdrowCommandHandler : IRequestHandler<CreateShopHandCashWithdrowCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShopHandCashWithdrowCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShopHandCashWithdrowCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShopHandCashWithdrowDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShopHandCashWithdrowDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ShopHandCashWithdrow = _mapper.Map<ShopHandCashWithdrow>(request.ShopHandCashWithdrowDto);

                ShopHandCashWithdrow = await _unitOfWork.Repository<ShopHandCashWithdrow>().Add(ShopHandCashWithdrow);
                
                

                if (ShopHandCashWithdrow.Type == 1)
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopHandCashWithdrow.WarehouseId ?? 0);
                    warehouse.CashInHand -= Convert.ToInt64(ShopHandCashWithdrow.TransferAmount);
                    warehouse.CashAmount += Convert.ToInt64(ShopHandCashWithdrow.TransferAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(ShopHandCashWithdrow.WarehouseId ?? 0);
                    warehouse.CashInHand += Convert.ToInt64(ShopHandCashWithdrow.TransferAmount);
                    warehouse.CashAmount -= Convert.ToInt64(ShopHandCashWithdrow.TransferAmount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ShopHandCashWithdrow.ShopHandCashWithdrowId;
            }

            return response;
        }
    }
}
