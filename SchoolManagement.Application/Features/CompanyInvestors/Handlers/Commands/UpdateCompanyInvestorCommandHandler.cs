using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands;
using SchoolManagement.Application.DTOs.CompanyInvestors.Validators;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Commands
{
    public class UpdateCompanyInvestorCommandHandler : IRequestHandler<UpdateCompanyInvestorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyInvestorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCompanyInvestorCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCompanyInvestorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompanyInvestorDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var CompanyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(request.CompanyInvestorDto.CompanyInvestorId);

            if (CompanyInvestor is null)
                throw new NotFoundException(nameof(CompanyInvestor), request.CompanyInvestorDto.CompanyInvestorId);

            _mapper.Map(request.CompanyInvestorDto, CompanyInvestor);

            await _unitOfWork.Repository<CompanyInvestor>().Update(CompanyInvestor);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
