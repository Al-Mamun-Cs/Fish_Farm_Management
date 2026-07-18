using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Brands.Validators;
using SchoolManagement.Application.Features.Brands.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Brands.Handlers.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBrandDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BrandDto);

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
                //// this method for Server Pc
                if (request.BrandDto.Photo != null)
                {
                    var fileName = Path.GetFileName(request.BrandDto.Photo.FileName);
                    uniqueFileName = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\brand";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.BrandDto.Photo.CopyToAsync(fileStream);
                    }
                }

                if (request.BrandDto.EshopImage != null)
                {
                    var fileName = Path.GetFileName(request.BrandDto.EshopImage.FileName);
                    uniqueFile = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\brand";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFile);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.BrandDto.EshopImage.CopyToAsync(fileStream);
                    }
                }

                //// this method for Local Pc
                //if (request.BrandDto.Photo != null)
                //{

                //    var fileName = Path.GetFileName(request.BrandDto.Photo.FileName);
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\brand", uniqueFileName);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.BrandDto.Photo.CopyToAsync(fileSteam);
                //    }


                //}

                //if (request.BrandDto.EshopImage != null)
                //{

                //    var fileName = Path.GetFileName(request.BrandDto.EshopImage.FileName);
                //    uniqueFile = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\brand", uniqueFile);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.BrandDto.EshopImage.CopyToAsync(fileSteam);
                //    }


                //}
                var Brand = _mapper.Map<Brand>(request.BrandDto);
                Brand.BrandImages = request.BrandDto.BrandImages ?? "files/brand/" + uniqueFileName;
                Brand.EshopImages = request.BrandDto.EshopImages ?? "files/brand/" + uniqueFile;
                Brand = await _unitOfWork.Repository<Brand>().Add(Brand);
                
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Brand.BrandId;
            }

            return response;
        }
    }
}
