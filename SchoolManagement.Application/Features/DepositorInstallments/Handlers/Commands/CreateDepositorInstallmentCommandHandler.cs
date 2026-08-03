using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DepositorInstallments.Validators;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Commands
{
    public class CreateDepositorInstallmentCommandHandler : IRequestHandler<CreateDepositorInstallmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepositorInstallmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDepositorInstallmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDepositorInstallmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepositorInstallmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                string uniqueFileName = null;

                //// this method for Server Pc
                if (request.DepositorInstallmentDto.Photo != null)
                {
                    var fileName = Path.GetFileName(request.DepositorInstallmentDto.Photo.FileName);
                    uniqueFileName = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\depositor-installment";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.DepositorInstallmentDto.Photo.CopyToAsync(fileStream);
                    }
                }

                //// this method for Local Pc
                //if (request.DepositorInstallmentDto.Photo != null)
                //{

                //    var fileName = Path.GetFileName(request.DepositorInstallmentDto.Photo.FileName);
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\depositor-installment", uniqueFileName);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.DepositorInstallmentDto.Photo.CopyToAsync(fileSteam);
                //    }


                //}

                var DepositorInstallment = _mapper.Map<DepositorInstallment>(request.DepositorInstallmentDto);
                DepositorInstallment.Image = request.DepositorInstallmentDto.Image ?? "files/depositor-installment/" + uniqueFileName;
                DepositorInstallment = await _unitOfWork.Repository<DepositorInstallment>().Add(DepositorInstallment);
                
                var warehouse = await _unitOfWork.Repository<Warehouse>().Get(DepositorInstallment?.WarehouseId ?? 0);
                warehouse.CashInHand += (DepositorInstallment.InstallmentAmount);
                await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                var depositor = await _unitOfWork.Repository<Depositor>().Get(DepositorInstallment?.DepositorId ?? 0);
                depositor.PresentBalance += (DepositorInstallment.InstallmentAmount);
                await _unitOfWork.Repository<Depositor>().Update(depositor);


                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DepositorInstallment.DepositorInstallmentId;
            }

            return response;
        }
    }
}
