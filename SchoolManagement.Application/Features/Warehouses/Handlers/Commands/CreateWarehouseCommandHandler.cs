using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Warehouses.Validators;
using SchoolManagement.Application.Features.Warehouses.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Commands
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWarehouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateWarehouseDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WarehouseDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                /////// Image Upload //////////
                string uniqueFileName = null;
                string uniqueFile = null;

                ////Server PC
                if (request.WarehouseDto.Photo != null)
                {
                    var fileName = Path.GetFileName(request.WarehouseDto.Photo.FileName);
                    uniqueFileName = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\business-units";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.WarehouseDto.Photo.CopyToAsync(fileStream);
                    }
                }

                if (request.WarehouseDto.Image != null)
                {
                    var fileName = Path.GetFileName(request.WarehouseDto.Image.FileName);
                    uniqueFile = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\business-units";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFile);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.WarehouseDto.Image.CopyToAsync(fileStream);
                    }
                }

                ////Local PC
                //if (request.WarehouseDto.Photo != null)
                //{

                //    var fileName = Path.GetFileName(request.WarehouseDto.Photo.FileName);
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\business-units", uniqueFileName);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.WarehouseDto.Photo.CopyToAsync(fileSteam);
                //    }


                //}

                //if (request.WarehouseDto.Image != null)
                //{

                //    var fileName = Path.GetFileName(request.WarehouseDto.Image.FileName);
                //    uniqueFile = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\business-units", uniqueFile);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.WarehouseDto.Image.CopyToAsync(fileSteam);
                //    }


                //}


                var Warehouse = _mapper.Map<Warehouse>(request.WarehouseDto);
                Warehouse.BusinessImages = request.WarehouseDto.BusinessImages ?? "files/business-units/" + uniqueFileName;
                Warehouse.ProductImages = request.WarehouseDto.ProductImages ?? "files/business-units/" + uniqueFile;
                Warehouse = await _unitOfWork.Repository<Warehouse>().Add(Warehouse);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Warehouse.WarehouseId;
            }

            return response;
        }
    }
}
