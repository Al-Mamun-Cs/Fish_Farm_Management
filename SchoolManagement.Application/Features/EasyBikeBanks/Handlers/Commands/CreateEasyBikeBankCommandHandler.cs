using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EasyBikeBanks.Validators;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Commands
{
    public class CreateEasyBikeBankCommandHandler : IRequestHandler<CreateEasyBikeBankCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEasyBikeBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEasyBikeBankCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEasyBikeBankDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EasyBikeBankDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EasyBikeBank = _mapper.Map<EasyBikeBank>(request.EasyBikeBankDto);

                EasyBikeBank = await _unitOfWork.Repository<EasyBikeBank>().Add(EasyBikeBank);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = EasyBikeBank.EasyBikeBankId;
            }

            return response;
        }
    }
}
