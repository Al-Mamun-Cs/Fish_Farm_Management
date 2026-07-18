using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Queries
{
    public class GetRoleFeaturesByRoleRequestHandler : IRequestHandler<GetRoleFeaturesByRoleRequest, List<RoleFeatureDto>>
    { 

        private readonly ISchoolManagementRepository<RoleFeature> _roleFeatureRepository; 
        private readonly ISchoolManagementRepository<Feature> _featureRepository; 

        private readonly IMapper _mapper;

        public GetRoleFeaturesByRoleRequestHandler(ISchoolManagementRepository<Feature> featureRepository,ISchoolManagementRepository<RoleFeature> roleFeatureRepository, IMapper mapper)
        {
            _roleFeatureRepository = roleFeatureRepository;
            _featureRepository = featureRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleFeatureDto>> Handle(GetRoleFeaturesByRoleRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Feature> features = _featureRepository.FilterWithInclude(x => x.IsActive, "Module");
            // List<int> roleFeatures = roleFeatureRepository.Where(x => x.RoleId == roleId).Select(x => x.FeatureKey).ToList();
            var roleFeatures = _roleFeatureRepository.Where(x => x.RoleId == request.roleId);

            List<RoleFeatureDto> roleFeatureList = features.Select(x => new RoleFeatureDto
            {
                FeatureId = x.FeatureId,
                FeatureName = x.FeatureName,
                ModuleName = x.Module.Title,
                IsAssigned = roleFeatures.Any(y => x.FeatureId == y.FeatureKey),
                Add = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Add),
                Update = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Update),
                Delete = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Delete),
                Report = roleFeatures.Any(y => x.FeatureId == y.FeatureKey && y.Report),
                FeatureTypeId = x.FeatureTypeId,
                IsReport = x.IsReport
            }).OrderBy(x=> x.ModuleName).ToList();

            return roleFeatureList;
        }
    }
}
