using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesInventorys.Validators;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Commands
{
    public class CreateFisheriesInventoryCommandHandler : IRequestHandler<CreateFisheriesInventoryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFisheriesInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFisheriesInventoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFisheriesInventoryDetailDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FisheriesInventoryDetailDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            try
            {
                // Step 1: Map and Add FisheriesInventory
                var FisheriesInventory = _mapper.Map<FisheriesInventory>(request.FisheriesInventoryDetailDto);
                FisheriesInventory = await _unitOfWork.Repository<FisheriesInventory>().Add(FisheriesInventory);
                var FInventory = await _unitOfWork.Repository<Supplier>().Get(FisheriesInventory?.SupplierId ?? 0);
                FInventory.TotalDueAmount += (FisheriesInventory.DueAmount);
                await _unitOfWork.Repository<Supplier>().Update(FInventory);

                // Step 2: Save to get FisheriesInventoryId from DB
                await _unitOfWork.Save();

                // Step 3: Map and Add FisheriesInventoryDetails
                if (request.FisheriesInventoryDetailDto.FisheriesInventoryDetail != null && request.FisheriesInventoryDetailDto.FisheriesInventoryDetail.Any())
                {
                    foreach (var detailDto in request.FisheriesInventoryDetailDto.FisheriesInventoryDetail)
                    {
                        var FisheriesInventoryDetail = _mapper.Map<FisheriesInventoryDetail>(detailDto);
                        FisheriesInventoryDetail.FisheriesInventoryId = FisheriesInventory.FisheriesInventoryId; // Set FK
                        FisheriesInventoryDetail.WarehouseId = FisheriesInventory.WarehouseId;
                        FisheriesInventoryDetail.AvailableQty = FisheriesInventoryDetail.TotalUnitQty;
                        await _unitOfWork.Repository<FisheriesInventoryDetail>().Add(FisheriesInventoryDetail);

                        
                    }
                }

                // Step 4: Save all FisheriesInventoryDetail entries
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = FisheriesInventory.FisheriesInventoryId;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Creation Failed due to an exception.";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
