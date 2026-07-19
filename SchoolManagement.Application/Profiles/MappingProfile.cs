using AutoMapper;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Application.DTOs.Districts;
using SchoolManagement.Application.DTOs.Divisions;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.DTOs.PaymentStatuses;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Application.DTOs.Religions;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Domain;


namespace SchoolManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            #region Warehouse Mappings
            CreateMap<Warehouse, WarehouseDto>().ReverseMap();
            CreateMap<Warehouse, CreateWarehouseDto>().ReverseMap();
            #endregion

           

            #region PaymentStatus Mappings
            CreateMap<PaymentStatus, PaymentStatusDto>().ReverseMap();
            CreateMap<PaymentStatus, CreatePaymentStatusDto>().ReverseMap();
            #endregion

           

            #region Supplier Mappings
            CreateMap<SupplierDto, Supplier >().ReverseMap()
                .ForMember(d => d.SupplierTypeName, o => o.MapFrom(s => s.SupplierType.SupplierTypeName))
                .ForMember(d => d.DivisionName, o => o.MapFrom(s => s.Division.DivisionName))
                .ForMember(d => d.DistricName, o => o.MapFrom(s => s.District.DistrictName))
                .ForMember(d => d.UpazilaName, o => o.MapFrom(s => s.Upozila.UpazilaName))
                .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName));
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            #endregion


            #region Features Mapping    
            CreateMap<FeatureDto, Feature>().ReverseMap()
             .ForMember(d => d.ModuleName, o => o.MapFrom(s => s.Module.Title));

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            #endregion

            #region Modules Mapping    
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleFeatureDto>().ReverseMap();

            CreateMap<Module, CreateModuleDto>().ReverseMap();
            #endregion

            

            #region RoleFeature Mappings 
            CreateMap<RoleFeature, RoleFeatureDto>().ReverseMap();
            CreateMap<RoleFeature, CreateRoleFeatureDto>().ReverseMap();
            #endregion

            

            #region Division Mappings
            CreateMap<Division, DivisionDto>().ReverseMap();
            CreateMap<Division, CreateDivisionDto>().ReverseMap();
            #endregion

            #region District Mappings
            CreateMap<DistrictDto, District >().ReverseMap()
                .ForMember(d => d.Division, o => o.MapFrom(s => s.Division.DivisionName));
            CreateMap<District, CreateDistrictDto>().ReverseMap();
            #endregion

            #region Upozila Mappings
            CreateMap<UpozilaDto, Upozila>().ReverseMap()
                .ForMember(d => d.District, o => o.MapFrom(s => s.District.DistrictName));
            CreateMap<Upozila, CreateUpozilaDto>().ReverseMap();
            #endregion


            #region SupplierType Mappings
            CreateMap<SupplierType, SupplierTypeDto>().ReverseMap();
            CreateMap<SupplierType, CreateSupplierTypeDto>().ReverseMap();
            #endregion



            #region Brand Mappings 
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            #endregion

            #region Country Mappings 
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            #endregion


            #region Designation Mappings 
            CreateMap<DesignationDto, Designation >().ReverseMap()
                .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName));
            CreateMap<Designation, CreateDesignationDto>().ReverseMap();
            #endregion

            
            #region Religion Mappings 
            CreateMap<Religion, ReligionDto>().ReverseMap();
            CreateMap<Religion, CreateReligionDto>().ReverseMap();
            #endregion

            #region BloodGroup Mappings 
            CreateMap<BloodGroup, BloodGroupDto>().ReverseMap();
            CreateMap<BloodGroup, CreateBloodGroupDto>().ReverseMap();
            #endregion

            
            #region Gender Mappings 
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Gender, CreateGenderDto>().ReverseMap();
            #endregion


            #region Bank Mappings 
            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<Bank, CreateBankDto>().ReverseMap();
            #endregion

            #region Fiscalyear Mappings 
            CreateMap<Fiscalyear, FiscalyearDto>().ReverseMap();
            CreateMap<Fiscalyear, CreateFiscalyearDto>().ReverseMap();
            #endregion


            #region FisheriesUnit Mappings 
            CreateMap<FisheriesUnit, FisheriesUnitDto>().ReverseMap();
            CreateMap<FisheriesUnit, CreateFisheriesUnitDto>().ReverseMap();
            #endregion

            #region FisheriesProductType Mappings 
            CreateMap<FisheriesProductTypeDto,FisheriesProductType>().ReverseMap()
                .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName));
            CreateMap<FisheriesProductType, CreateFisheriesProductTypeDto>().ReverseMap();
            #endregion

            #region Pond Mappings 
            CreateMap<Pond, PondDto>().ReverseMap();
            CreateMap<Pond, CreatePondDto>().ReverseMap();
            #endregion

            #region FisheriesInventory Mappings
            CreateMap<FisheriesInventoryDto, FisheriesInventory>().ReverseMap()
                  .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName))
                  .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName + "-" + s.Supplier.PhoneNo + "-" + s.Supplier.Address))
                  .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.StatusName));
            CreateMap<FisheriesInventory, CreateFisheriesInventoryDto>().ReverseMap();
            CreateMap<CreateFisheriesInventoryDetailDto, FisheriesInventory>().ReverseMap();
            CreateMap<FisheriesInventoryDetailDto, FisheriesInventory>().ReverseMap()
                 .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName))
                 .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName + "-" + s.Supplier.PhoneNo + "-" + s.Supplier.Address))
                 .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.StatusName));
            CreateMap<FisheriesInventory, CreateFisheriesInventoryDetailDto>().ReverseMap();


            #endregion
            #region FisheriesInventoryDetail Mappings
            CreateMap<FisheriesInventoryDetailDto, SchoolManagement.Domain.FisheriesInventoryDetail>().ReverseMap()
                  .ForMember(d => d.ProductType, o => o.MapFrom(s => s.FisheriesProductType.NameBangla + "-" + s.FisheriesProductType.NameEnglish));
            CreateMap<SchoolManagement.Application.DTOs.FisheriesInventorys.FisheriesInventoryDetail, SchoolManagement.Domain.FisheriesInventoryDetail>();
            #endregion


            #region FisheriesInventoryOut Mappings 
            CreateMap<FisheriesInventoryOutDto, FisheriesInventoryOut>().ReverseMap()
            .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName))
            .ForMember(d => d.Pond, o => o.MapFrom(s => s.Pond.NameBangla))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.FisheriesInventoryDetail.ProductName))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.FisheriesProductType.NameBangla));
            CreateMap<FisheriesInventoryOut, CreateFisheriesInventoryOutDto>().ReverseMap();
            #endregion


            #region ShopInventory Mappings
            CreateMap<ShopInventoryDto, ShopInventory>().ReverseMap()
                  .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName))
                  .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName + "-" + s.Supplier.PhoneNo + "-" + s.Supplier.Address))
                  .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.StatusName));
            CreateMap<ShopInventory, CreateShopInventoryDto>().ReverseMap();
            CreateMap<CreateShopInventoryDetailDto, ShopInventory>().ReverseMap();
            CreateMap<ShopInventoryDetailDto, ShopInventory>().ReverseMap()
                 .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Warehouse.WarehouseName))
                 .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName + "-" + s.Supplier.PhoneNo + "-" + s.Supplier.Address))
                 .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.StatusName));
            CreateMap<ShopInventory, CreateShopInventoryDetailDto>().ReverseMap();

            #endregion
            #region ShopInventoryDetail Mappings
            CreateMap<ShopInventoryDetailDto, SchoolManagement.Domain.ShopInventoryDetail>().ReverseMap()
                  .ForMember(d => d.ProductType, o => o.MapFrom(s => s.FisheriesProductType.NameBangla + "-" + s.FisheriesProductType.NameEnglish));
            CreateMap<SchoolManagement.Application.DTOs.ShopInventorys.ShopInventoryDetail, SchoolManagement.Domain.ShopInventoryDetail>();
            #endregion




        }

    }

}
