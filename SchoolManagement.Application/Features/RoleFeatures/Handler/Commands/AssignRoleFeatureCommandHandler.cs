using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Commands
{
    public class AssignRoleFeatureCommandHandler : IRequestHandler<AssignRoleFeatureCommand, Unit>
    {
        private readonly ISchoolManagementRepository<RoleFeature> _roleFeatureRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignRoleFeatureCommandHandler(ISchoolManagementRepository<RoleFeature> roleFeatureRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleFeatureRepository = roleFeatureRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignRoleFeatureCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RoleId))
            {
                throw new BadRequestException("Invalid Role !");
            }
            if (request.RoleFeatureDto.IsAssigned)
            {
                var isExist = _roleFeatureRepository.Exists(x => x.RoleId == request.RoleId && x.FeatureKey == request.RoleFeatureDto.FeatureId);
                RoleFeature roleFeature = _roleFeatureRepository.FindOne(x => x.RoleId == request.RoleId && x.FeatureKey == request.RoleFeatureDto.FeatureId) ?? new RoleFeature();
                roleFeature.RoleId = request.RoleId;
                roleFeature.FeatureKey = request.RoleFeatureDto.FeatureId;
                roleFeature.Add = request.RoleFeatureDto.Add;
                roleFeature.Update = request.RoleFeatureDto.Update;
                roleFeature.Delete = request.RoleFeatureDto.Delete;
                roleFeature.Report = request.RoleFeatureDto.Report;
                if (isExist)
                {
                    await _unitOfWork.Repository<RoleFeature>().Update(roleFeature);
                    await _unitOfWork.Save();
                }
                else
                {
                    await _unitOfWork.Repository<RoleFeature>().Add(roleFeature);
                    await _unitOfWork.Save();
                }
            }
            else
            {
                var roleFeature = _roleFeatureRepository.FindOne(x => x.RoleId == request.RoleId && x.FeatureKey == request.RoleFeatureDto.FeatureId);
                await _unitOfWork.Repository<RoleFeature>().Delete(roleFeature);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}