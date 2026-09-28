using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Commands
{
    public class DeleteEasyBikeBankCommandHandler : IRequestHandler<DeleteEasyBikeBankCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEasyBikeBankCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEasyBikeBankCommand request, CancellationToken cancellationToken)
        {
            var EasyBikeBank = await _unitOfWork.Repository<EasyBikeBank>().Get(request.EasyBikeBankId);

            if (EasyBikeBank == null)
                throw new NotFoundException(nameof(EasyBikeBank), request.EasyBikeBankId);

            

            try
            {
                await _unitOfWork.Repository<EasyBikeBank>().Delete(EasyBikeBank);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.EasyBikeBankId);
            }

            return Unit.Value;
        }
    }
}
