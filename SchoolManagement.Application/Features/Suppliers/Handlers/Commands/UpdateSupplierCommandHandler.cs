using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Commands;
using SchoolManagement.Application.DTOs.Suppliers.Validators;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Commands
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSupplierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSupplierDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.CreateSupplierDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Supplier = await _unitOfWork.Repository<Supplier>().Get(request.CreateSupplierDto.SupplierId);

            if (Supplier is null)
                throw new NotFoundException(nameof(Supplier), request.CreateSupplierDto.SupplierId);

            /////// Image Upload //////////
            string uniqueFileName = null;

            //Server PC
            if (request.CreateSupplierDto.Photo != null)
            {
                var fileName = Path.GetFileName(request.CreateSupplierDto.Photo.FileName);
                uniqueFileName = Guid.NewGuid() + "_" + fileName;

                var uploadRoot = @"D:\IthContent\files\suppliers";

                if (!Directory.Exists(uploadRoot))
                {
                    Directory.CreateDirectory(uploadRoot);
                }

                var filePath = Path.Combine(uploadRoot, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateSupplierDto.Photo.CopyToAsync(fileStream);
                }
            }

            ////Local PC
            //if (request.CreateSupplierDto.Photo != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateSupplierDto.Photo.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\suppliers", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateSupplierDto.Photo.CopyToAsync(fileSteam);
            //    }


            //}
            _mapper.Map(request.CreateSupplierDto, Supplier);
            Supplier.ClientsImage = request.CreateSupplierDto.Photo != null ? "files/suppliers/" + uniqueFileName : (Supplier.ClientsImage != null ? Supplier.ClientsImage.Replace("http://217.217.254.11:8080/IthContent/", String.Empty) : null);
            
            await _unitOfWork.Repository<Supplier>().Update(Supplier);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
