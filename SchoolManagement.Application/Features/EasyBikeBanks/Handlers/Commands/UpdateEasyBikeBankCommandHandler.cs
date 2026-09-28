using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using System.Threading;
using System.Threading.Tasks;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands;
using SchoolManagement.Application.DTOs.EasyBikeBanks.Validators;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Commands
{
    public class UpdateEasyBikeBankCommandHandler : IRequestHandler<UpdateEasyBikeBankCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEasyBikeBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEasyBikeBankCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEasyBikeBankDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EasyBikeBankDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var EasyBikeBank = await _unitOfWork.Repository<EasyBikeBank>().Get(request.EasyBikeBankDto.EasyBikeBankId);

            if (EasyBikeBank is null)
                throw new NotFoundException(nameof(EasyBikeBank), request.EasyBikeBankDto.EasyBikeBankId);

            _mapper.Map(request.EasyBikeBankDto, EasyBikeBank);

            await _unitOfWork.Repository<EasyBikeBank>().Update(EasyBikeBank);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
