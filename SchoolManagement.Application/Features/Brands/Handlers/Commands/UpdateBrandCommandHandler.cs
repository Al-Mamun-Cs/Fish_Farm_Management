using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Brands.Requests.Commands;
using SchoolManagement.Application.DTOs.Brands.Validators;

namespace SchoolManagement.Application.Features.Brands.Handlers.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBrandDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateBrandDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Brand = await _unitOfWork.Repository<Brand>().Get(request.CreateBrandDto.BrandId);

            if (Brand is null)
                throw new NotFoundException(nameof(Brand), request.CreateBrandDto.BrandId);

            /////// Image Upload //////////
            string uniqueFileName = null;
            string uniqueFile = null;

            ////Server PC
            if (request.CreateBrandDto.Photo != null)
            {
                var fileName = Path.GetFileName(request.CreateBrandDto.Photo.FileName);
                uniqueFileName = Guid.NewGuid() + "_" + fileName;

                var uploadRoot = @"D:\IthContent\files\brand";

                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }

                var filePath = Path.Combine(uploadRoot, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateBrandDto.Photo.CopyToAsync(fileStream);
                }
            }

            if (request.CreateBrandDto.EshopImage != null)
            {
                var fileName = Path.GetFileName(request.CreateBrandDto.EshopImage.FileName);
                uniqueFile = Guid.NewGuid() + "_" + fileName;

                var uploadRoot = @"D:\IthContent\files\brand";

                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }

                var filePath = Path.Combine(uploadRoot, uniqueFile);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateBrandDto.EshopImage.CopyToAsync(fileStream);
                }
            }

            ////Local PC
            //if (request.CreateBrandDto.Photo != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateBrandDto.Photo.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\brand", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateBrandDto.Photo.CopyToAsync(fileSteam);
            //    }


            //}

            //if (request.CreateBrandDto.EshopImage != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateBrandDto.EshopImage.FileName);
            //    uniqueFile = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\brand", uniqueFile);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateBrandDto.EshopImage.CopyToAsync(fileSteam);
            //    }


            //}

            _mapper.Map(request.CreateBrandDto, Brand);
            //Brand.BrandImages = request.CreateBrandDto.Photo != null ? "files/brand/" + uniqueFileName : Brand.BrandImages.Replace("http://localhost:44395/Content/", String.Empty);
            //Brand.EshopImages = request.CreateBrandDto.EshopImage != null ? "files/brand/" + uniqueFile : Brand.EshopImages.Replace("http://localhost:44395/Content/", String.Empty);
            Brand.BrandImages = request.CreateBrandDto.Photo != null ? "files/brand/" + uniqueFileName : Brand.BrandImages.Replace("http://217.217.254.11:8080/IthContent/", String.Empty);
            Brand.EshopImages = request.CreateBrandDto.EshopImage != null ? "files/brand/" + uniqueFile : Brand.EshopImages.Replace("http://217.217.254.11:8080/IthContent/", String.Empty);
            await _unitOfWork.Repository<Brand>().Update(Brand);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
