using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace SchoolManagement.Persistence
{
    public class SchoolManagementDbContext : AuditableDbContext
    {
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<BloodGroup>(entity =>
            {

            });


            modelBuilder.Entity<Gender>(entity =>
            {

            });

            modelBuilder.Entity<Religion>(entity =>
            {

            });


            modelBuilder.Entity<Supplier>(entity =>
            {
                //entity.HasOne(d => d.CustomerType)
                //                    .WithMany(p => p.Suppliers)
                //                    .HasForeignKey(d => d.CustomerTypeId)
                //                    .HasConstraintName("FK_Supplier_CustomerType");

                entity.HasOne(d => d.SupplierType)
                                    .WithMany(p => p.Suppliers)
                                    .HasForeignKey(d => d.SupplierTypeId)
                                    .HasConstraintName("FK_Supplier_SupplierType");

                entity.HasOne(d => d.Division)
                                    .WithMany(p => p.Suppliers)
                                    .HasForeignKey(d => d.DivisionId)
                                    .HasConstraintName("FK_Supplier_Division");

                entity.HasOne(d => d.District)
                                    .WithMany(p => p.Suppliers)
                                    .HasForeignKey(d => d.DistrictId)
                                    .HasConstraintName("FK_Supplier_District");

                entity.HasOne(d => d.Upozila)
                                    .WithMany(p => p.Suppliers)
                                    .HasForeignKey(d => d.UpazilaId)
                                    .HasConstraintName("FK_Supplier_Upozila");

                //entity.HasOne(d => d.Category)
                //                    .WithMany(p => p.Suppliers)
                //                    .HasForeignKey(d => d.CategoryId)
                //                    .HasConstraintName("FK_Supplier_EasyBikeType");

                //entity.HasOne(d => d.ProductType)
                //                    .WithMany(p => p.Suppliers)
                //                    .HasForeignKey(d => d.ProductTypeId)
                //                    .HasConstraintName("FK_Supplier_ProductType");

                entity.HasOne(d => d.Warehouse)
                                   .WithMany(p => p.Suppliers)
                                   .HasForeignKey(d => d.WarehouseId)
                                   .HasConstraintName("FK_Supplier_Warehouse");

                entity.HasOne(d => d.Country)
                                   .WithMany(p => p.Suppliers)
                                   .HasForeignKey(d => d.CountryId)
                                   .HasConstraintName("FK_Supplier_Country");
            });
            

            modelBuilder.Entity<Warehouse>(entity =>
            {

            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {

            });



            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feature_Module");
            });


            modelBuilder.Entity<Module>(entity =>
            {

            });

           


            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FeatureKey });

                entity.ToTable("RoleFeature");


            });
            

            modelBuilder.Entity<Division>(entity =>
            {

            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_Division");

            });

            modelBuilder.Entity<Upozila>(entity =>
            {
                entity.HasOne(d => d.District)
                    .WithMany(p => p.Upozilas)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Upozila_District");

            });


            modelBuilder.Entity<Brand>(entity =>
            {

            });

            

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.Designations)
                                  .HasForeignKey(d => d.WarehouseId)
                                  .HasConstraintName("FK_Designation_Warehouse");

            });


            modelBuilder.Entity<Country>(entity =>
            {

            });


            

            modelBuilder.Entity<Bank>(entity =>
            {

            });

            modelBuilder.Entity<Fiscalyear>(entity =>
            {


            });
            


            modelBuilder.Entity<FisheriesUnit>(entity =>
            {

            });


            modelBuilder.Entity<FisheriesProductType>(entity =>
            {
                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.FisheriesProductTypes)
                                  .HasForeignKey(d => d.WarehouseId)
                                  .HasConstraintName("FK_FisheriesProductType_Warehouse");


            });

            modelBuilder.Entity<Pond>(entity =>
            {

            });

            modelBuilder.Entity<FisheriesInventory>(entity =>
            {
                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.FisheriesInventorys)
                                  .HasForeignKey(d => d.WarehouseId)
                                  .HasConstraintName("FK_FisheriesInventory_Warehouse");

                entity.HasOne(d => d.Supplier)
                                  .WithMany(p => p.FisheriesInventorys)
                                  .HasForeignKey(d => d.SupplierId)
                                  .HasConstraintName("FK_FisheriesInventory_Supplier");

                entity.HasOne(d => d.PaymentStatus)
                                  .WithMany(p => p.FisheriesInventorys)
                                  .HasForeignKey(d => d.PaymentStatusId)
                                  .HasConstraintName("FK_FisheriesInventory_PaymentStatus");

            });

            modelBuilder.Entity<FisheriesInventoryDetail>(entity =>
            {
                entity.HasOne(d => d.FisheriesInventory)
                                  .WithMany(p => p.FisheriesInventoryDetails)
                                  .HasForeignKey(d => d.FisheriesInventoryId)
                                  .HasConstraintName("FK_FisheriesInventoryDetail_FisheriesInventory");

                entity.HasOne(d => d.FisheriesProductType)
                                  .WithMany(p => p.FisheriesInventoryDetails)
                                  .HasForeignKey(d => d.FisheriesProductTypeId)
                                  .HasConstraintName("FK_FisheriesInventoryDetail_FisheriesProductType");

                entity.HasOne(d => d.FisheriesUnit)
                                  .WithMany(p => p.FisheriesInventoryDetails)
                                  .HasForeignKey(d => d.FisheriesUnitId)
                                  .HasConstraintName("FK_FisheriesInventoryDetail_FisheriesUnit");

            });

            modelBuilder.Entity<FisheriesInventoryOut>(entity =>
            {
                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.FisheriesInventoryOuts)
                                  .HasForeignKey(d => d.WarehouseId)
                                  .HasConstraintName("FK_FisheriesInventoryOut_Warehouse");

                entity.HasOne(d => d.Pond)
                                  .WithMany(p => p.FisheriesInventoryOuts)
                                  .HasForeignKey(d => d.PondId)
                                  .HasConstraintName("FK_FisheriesInventoryOut_Pond");

                entity.HasOne(d => d.FisheriesProductType)
                                  .WithMany(p => p.FisheriesInventoryOuts)
                                  .HasForeignKey(d => d.FisheriesProductTypeId)
                                  .HasConstraintName("FK_FisheriesInventoryOut_FisheriesProductType");

                entity.HasOne(d => d.FisheriesInventoryDetail)
                                  .WithMany(p => p.FisheriesInventoryOuts)
                                  .HasForeignKey(d => d.FisheriesInventoryDetailId)
                                  .HasConstraintName("FK_FisheriesInventoryOut_FisheriesInventoryDetail");



            });

            modelBuilder.Entity<ShopInventory>(entity =>
            {
                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.ShopInventorys)
                                  .HasForeignKey(d => d.WarehouseId)
                                  .HasConstraintName("FK_ShopInventory_Warehouse");

                entity.HasOne(d => d.Supplier)
                                  .WithMany(p => p.ShopInventorys)
                                  .HasForeignKey(d => d.SupplierId)
                                  .HasConstraintName("FK_ShopInventory_Supplier");

                entity.HasOne(d => d.PaymentStatus)
                                  .WithMany(p => p.ShopInventorys)
                                  .HasForeignKey(d => d.PaymentStatusId)
                                  .HasConstraintName("FK_ShopInventory_PaymentStatus");

            });

            modelBuilder.Entity<ShopInventoryDetail>(entity =>
            {
                entity.HasOne(d => d.ShopInventory)
                                  .WithMany(p => p.ShopInventoryDetails)
                                  .HasForeignKey(d => d.ShopInventoryId)
                                  .HasConstraintName("FK_ShopInventoryDetail_ShopInventory");

                entity.HasOne(d => d.Warehouse)
                                  .WithMany(p => p.ShopInventoryDetails)
                                  .HasForeignKey(d => d.ShopInventoryId)
                                  .HasConstraintName("FK_ShopInventoryDetail_Warehouse");

                entity.HasOne(d => d.FisheriesProductType)
                                  .WithMany(p => p.ShopInventoryDetails)
                                  .HasForeignKey(d => d.FisheriesProductTypeId)
                                  .HasConstraintName("FK_ShopInventoryDetail_FisheriesProductType");

                entity.HasOne(d => d.FisheriesUnit)
                                  .WithMany(p => p.ShopInventoryDetails)
                                  .HasForeignKey(d => d.FisheriesUnitId)
                                  .HasConstraintName("FK_ShopInventoryDetail_FisheriesUnit");

            });




        }

        public virtual DbSet<ShopInventory> ShopInventory { get; set; } = null!;
        public virtual DbSet<ShopInventoryDetail> ShopInventoryDetail { get; set; } = null!;
        public virtual DbSet<FisheriesInventoryOut> FisheriesInventoryOut { get; set; } = null!;
        public virtual DbSet<FisheriesInventory> FisheriesInventory { get; set; } = null!;
        public virtual DbSet<FisheriesInventoryDetail> FisheriesInventoryDetail { get; set; } = null!;
        public virtual DbSet<FisheriesUnit> FisheriesUnit { get; set; } = null!;
        public virtual DbSet<FisheriesProductType> FisheriesProductType { get; set; } = null!;
        public virtual DbSet<Pond> Pond { get; set; } = null!;
        public virtual DbSet<Fiscalyear> Fiscalyear { get; set; } = null!;
        public virtual DbSet<Bank> Bank { get; set; } = null!;
        public virtual DbSet<Religion> Religion { get; set; } = null!;
        public virtual DbSet<Gender> Gender { get; set; } = null!;
        public virtual DbSet<BloodGroup> BloodGroup { get; set; } = null!;
        public virtual DbSet<Country> Country { get; set; } = null!;
        public virtual DbSet<Designation> Designation { get; set; } = null!;
        public virtual DbSet<Brand> Brand { get; set; } = null!;
        public virtual DbSet<SupplierType> SupplierType { get; set; } = null!;
        public virtual DbSet<Division> Division { get; set; } = null!;
        public virtual DbSet<District> District { get; set; } = null!;
        public virtual DbSet<Upozila> Upozila { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; } = null!;
        public virtual DbSet<Supplier> Supplier { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouse { get; set; } = null!;
        public virtual DbSet<Feature> Feature { get; set; } = null!;
        public virtual DbSet<Module> Module { get; set; } = null!;
        public virtual DbSet<RoleFeature> RoleFeature { get; set; } = null!;
    }
}
