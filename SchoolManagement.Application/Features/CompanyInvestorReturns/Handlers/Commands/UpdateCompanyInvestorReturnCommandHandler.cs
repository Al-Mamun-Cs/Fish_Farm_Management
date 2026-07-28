using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns.Validators;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Commands
{
    public class UpdateCompanyInvestorReturnCommandHandler : IRequestHandler<UpdateCompanyInvestorReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyInvestorReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCompanyInvestorReturnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCompanyInvestorReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompanyInvestorReturnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var CompanyInvestorReturn = await _unitOfWork.Repository<CompanyInvestorReturn>().Get(request.CompanyInvestorReturnDto.CompanyInvestorReturnId);

            if (CompanyInvestorReturn is null)
                throw new NotFoundException(nameof(CompanyInvestorReturn), request.CompanyInvestorReturnDto.CompanyInvestorReturnId);

            _mapper.Map(request.CompanyInvestorReturnDto, CompanyInvestorReturn);

            await _unitOfWork.Repository<CompanyInvestorReturn>().Update(CompanyInvestorReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
