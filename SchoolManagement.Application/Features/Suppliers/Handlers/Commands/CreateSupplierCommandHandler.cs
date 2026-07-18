using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Suppliers.Validators;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Features.Suppliers.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Commands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, BaseCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSupplierDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SupplierDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }
            else
            {
                /////// Image Upload //////////
                string uniqueFileName = null;

                ////Server PC
                if (request.SupplierDto.Photo != null)
                {
                    var fileName = Path.GetFileName(request.SupplierDto.Photo.FileName);
                    uniqueFileName = Guid.NewGuid() + "_" + fileName;

                    var uploadRoot = @"D:\IthContent\files\suppliers";

                    if (!Directory.Exists(uploadRoot))
                    {
                        Directory.CreateDirectory(uploadRoot);
                    }

                    var filePath = Path.Combine(uploadRoot, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.SupplierDto.Photo.CopyToAsync(fileStream);
                    }
                }

                ////Local PC
                //if (request.SupplierDto.Photo != null)
                //{

                //    var fileName = Path.GetFileName(request.SupplierDto.Photo.FileName);
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\suppliers", uniqueFileName);
                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.SupplierDto.Photo.CopyToAsync(fileSteam);
                //    }


                //}
                var Supplier = _mapper.Map<Supplier>(request.SupplierDto);
                Supplier.ClientsImage = request.SupplierDto.ClientsImage ?? "files/suppliers/" + uniqueFileName;
                Supplier = await _unitOfWork.Repository<Supplier>().Add(Supplier);
                
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Supplier.SupplierId;
            }
            

            return response;
        }
    }
}
