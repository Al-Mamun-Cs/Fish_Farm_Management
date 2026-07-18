using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Application.DTOs.Warehouses.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Warehouses.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Commands
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWarehouseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateWarehouseDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.CreateWarehouseDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Warehouse = await _unitOfWork.Repository<Warehouse>().Get(request.CreateWarehouseDto.WarehouseId);

            if (Warehouse is null)
                throw new NotFoundException(nameof(Warehouse), request.CreateWarehouseDto.WarehouseId);


            /////// Image Upload //////////
            string uniqueFileName = null;
            string uniqueFile = null;

            ////Server PC
            if (request.CreateWarehouseDto.Photo != null)
            {
                var fileName = Path.GetFileName(request.CreateWarehouseDto.Photo.FileName);
                uniqueFileName = Guid.NewGuid() + "_" + fileName;

                var uploadRoot = @"D:\IthContent\files\business-units";

                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }

                var filePath = Path.Combine(uploadRoot, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateWarehouseDto.Photo.CopyToAsync(fileStream);
                }
            }

            if (request.CreateWarehouseDto.Image != null)
            {
                var fileName = Path.GetFileName(request.CreateWarehouseDto.Image.FileName);
                uniqueFile = Guid.NewGuid() + "_" + fileName;

                var uploadRoot = @"D:\IthContent\files\business-units";

                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }

                var filePath = Path.Combine(uploadRoot, uniqueFile);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateWarehouseDto.Image.CopyToAsync(fileStream);
                }
            }

            ////Local PC
            //if (request.CreateWarehouseDto.Photo != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateWarehouseDto.Photo.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\business-units", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateWarehouseDto.Photo.CopyToAsync(fileSteam);
            //    }


            //}
            //if (request.CreateWarehouseDto.Image != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateWarehouseDto.Image.FileName);
            //    uniqueFile = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\business-units", uniqueFile);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateWarehouseDto.Image.CopyToAsync(fileSteam);
            //    }


            //}


            _mapper.Map(request.CreateWarehouseDto, Warehouse);
            //Warehouse.BusinessImages = request.CreateWarehouseDto.Photo != null ? "files/business-units/" + uniqueFileName : Warehouse.BusinessImages.Replace("http://localhost:44395/Content/", String.Empty);
            //Warehouse.ProductImages = request.CreateWarehouseDto.Image != null ? "files/business-units/" + uniqueFile : Warehouse.ProductImages.Replace("http://localhost:44395/Content/", String.Empty);
            Warehouse.BusinessImages = request.CreateWarehouseDto.Photo != null ? "files/business-units/" + uniqueFileName : Warehouse.BusinessImages.Replace("http://217.217.254.11:8080/IthContent/", String.Empty);
            Warehouse.ProductImages = request.CreateWarehouseDto.Image != null ? "files/business-units/" + uniqueFile : Warehouse.ProductImages.Replace("http://217.217.254.11:8080/IthContent/", String.Empty);
            await _unitOfWork.Repository<Warehouse>().Update(Warehouse);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
