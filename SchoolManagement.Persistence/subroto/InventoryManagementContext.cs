using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolManagement.Persistence.subroto
{
    public partial class InventoryManagementContext : DbContext
    {
        public InventoryManagementContext()
        {
        }

        public InventoryManagementContext(DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdvancePayment> AdvancePayments { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<BankTransaction> BankTransactions { get; set; } = null!;
        public virtual DbSet<BankTransactionType> BankTransactionTypes { get; set; } = null!;
        public virtual DbSet<Bulletin> Bulletins { get; set; } = null!;
        public virtual DbSet<CashDebit> CashDebits { get; set; } = null!;
        public virtual DbSet<CashInHand> CashInHands { get; set; } = null!;
        public virtual DbSet<CashInHandLog> CashInHandLogs { get; set; } = null!;
        public virtual DbSet<DuePaid> DuePaids { get; set; } = null!;
        public virtual DbSet<EasyBikeBank> EasyBikeBanks { get; set; } = null!;
        public virtual DbSet<EasyBikeType> EasyBikeTypes { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<GoodSale> GoodSales { get; set; } = null!;
        public virtual DbSet<GoodSaleDetail> GoodSaleDetails { get; set; } = null!;
        public virtual DbSet<GoodSalePayDueLog> GoodSalePayDueLogs { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;
        public virtual DbSet<NoticeInfo> NoticeInfos { get; set; } = null!;
        public virtual DbSet<NoticeType> NoticeTypes { get; set; } = null!;
        public virtual DbSet<PayablePaid> PayablePaids { get; set; } = null!;
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<ProductionLog> ProductionLogs { get; set; } = null!;
        public virtual DbSet<RoleFeature> RoleFeatures { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=45.114.84.157\\sql14;Database=InventoryManagement;User Id=sa;Password=123456;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvancePayment>(entity =>
            {
                entity.ToTable("AdvancePayment");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CustomerReaceveDate).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.GoodsQty).HasMaxLength(50);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(550);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AdvancePayments)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_AdvancePayment_EasyBikeType");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.AdvancePayments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_AdvancePayment_PaymentStatus");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.AdvancePayments)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_AdvancePayment_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.AdvancePayments)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_AdvancePayment_Warehouse");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.BranchId).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PhotoPath).HasMaxLength(500);

                entity.Property(e => e.Pno).HasColumnName("PNo");

                entity.Property(e => e.RoleName).HasMaxLength(100);

                entity.Property(e => e.SignaturePath).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BankTransaction>(entity =>
            {
                entity.ToTable("BankTransaction");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ChequeDate).HasColumnType("datetime");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1500);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.BankInfo)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.BankInfoId)
                    .HasConstraintName("FK_BankTransaction_EasyBikeBank");

                entity.HasOne(d => d.BankTransactionType)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.BankTransactionTypeId)
                    .HasConstraintName("FK_BankTransaction_BankTransactionType");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_BankTransaction_Inventory");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_BankTransaction_PaymentStatus");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_BankTransaction_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_BankTransaction_Warehouse");
            });

            modelBuilder.Entity<BankTransactionType>(entity =>
            {
                entity.ToTable("BankTransactionType");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Bulletin>(entity =>
            {
                entity.ToTable("Bulletin");

                entity.Property(e => e.BuletinDetails).HasMaxLength(4000);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CashDebit>(entity =>
            {
                entity.ToTable("CashDebit");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.HasOne(d => d.CashInHand)
                    .WithMany(p => p.CashDebits)
                    .HasForeignKey(d => d.CashInHandId)
                    .HasConstraintName("FK_CashDebit_CashInHand");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.CashDebits)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_CashDebit_Warehouse");
            });

            modelBuilder.Entity<CashInHand>(entity =>
            {
                entity.ToTable("CashInHand");

                entity.Property(e => e.AvailableAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.CashInHands)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_CashInHand_PaymentStatus");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.CashInHands)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_CashInHand_Warehouse");
            });

            modelBuilder.Entity<CashInHandLog>(entity =>
            {
                entity.ToTable("CashInHandLog");

                entity.Property(e => e.AvailableAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiveAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ReceiveDate).HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.HasOne(d => d.BankInfo)
                    .WithMany(p => p.CashInHandLogs)
                    .HasForeignKey(d => d.BankInfoId)
                    .HasConstraintName("FK_CashInHandLog_EasyBikeBank");

                entity.HasOne(d => d.CashInHand)
                    .WithMany(p => p.CashInHandLogs)
                    .HasForeignKey(d => d.CashInHandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashInHandLog_CashInHand");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.CashInHandLogs)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_CashInHandLog_PaymentStatus");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.CashInHandLogs)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_CashInHandLog_Warehouse");
            });

            modelBuilder.Entity<DuePaid>(entity =>
            {
                entity.ToTable("DuePaid");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.DuePaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.HasOne(d => d.BankInfo)
                    .WithMany(p => p.DuePaids)
                    .HasForeignKey(d => d.BankInfoId)
                    .HasConstraintName("FK_DuePaid_EasyBikeBank");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.DuePaids)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_DuePaid_Inventory");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.DuePaids)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_DuePaid_PaymentStatus");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DuePaids)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_DuePaid_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.DuePaids)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_DuePaid_Warehouse");
            });

            modelBuilder.Entity<EasyBikeBank>(entity =>
            {
                entity.ToTable("EasyBikeBank");

                entity.Property(e => e.BankAccountNo).HasMaxLength(250);

                entity.Property(e => e.BankBalance).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.BankName).HasMaxLength(1500);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNo).HasMaxLength(15);
            });

            modelBuilder.Entity<EasyBikeType>(entity =>
            {
                entity.ToTable("EasyBikeType");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName).HasMaxLength(500);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("Feature");

                entity.Property(e => e.Class).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FeatureName).HasMaxLength(450);

                entity.Property(e => e.GroupTitle).HasMaxLength(250);

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.IconName).HasMaxLength(250);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feature_Module");
            });

            modelBuilder.Entity<GoodSale>(entity =>
            {
                entity.ToTable("GoodSale");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.Property(e => e.SaleDate).HasColumnType("date");

                entity.Property(e => e.SaleDueAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SaleDuePaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SaleLessAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SalePaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SaleQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TolyRent).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TotalSalePrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.VoucherNo).HasMaxLength(50);

                entity.Property(e => e.WeightingScaleNo).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.GoodSales)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_GoodSale_EasyBikeType");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.GoodSales)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_GoodSale_Inventory");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.GoodSales)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_GoodSale_PaymentStatus");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.GoodSales)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_GoodSale_EasyBikeBank");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.GoodSales)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_GoodSale_Supplier");
            });

            modelBuilder.Entity<GoodSaleDetail>(entity =>
            {
                entity.ToTable("GoodSaleDetail");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SaleQty).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.GoodSale)
                    .WithMany(p => p.GoodSaleDetails)
                    .HasForeignKey(d => d.GoodSaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GoodSaleDetail_GoodSale");
            });

            modelBuilder.Entity<GoodSalePayDueLog>(entity =>
            {
                entity.ToTable("GoodSalePayDueLog");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SaleDate).HasColumnType("date");

                entity.Property(e => e.SaleDueAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SaleQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.VoucherNo).HasMaxLength(50);

                entity.HasOne(d => d.GoodSale)
                    .WithMany(p => p.GoodSalePayDueLogs)
                    .HasForeignKey(d => d.GoodSaleId)
                    .HasConstraintName("FK_GoodSalePayDueLog_GoodSale");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.GoodSalePayDueLogs)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_GoodSalePayDueLog_Supplier");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.AvailableQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DamageQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.DuePaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LessAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ProductionQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PurchaseQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.Property(e => e.ReturnQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.SellQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TolyRent).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TotalPurchasePrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.VoucherNo).HasMaxLength(50);

                entity.Property(e => e.WeightingScaleNo).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.BankInfo)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.BankInfoId)
                    .HasConstraintName("FK_Inventory_EasyBikeBank");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Inventory_EasyBikeType");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_Inventory_PaymentStatus");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_Inventory_ProductType");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Inventory_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_Inventory_Warehouse");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("Module");

                entity.Property(e => e.Class).HasMaxLength(250);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.GroupTitle).HasMaxLength(250);

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.IconName).HasMaxLength(250);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleName).HasMaxLength(450);

                entity.Property(e => e.Title).HasMaxLength(450);
            });

            modelBuilder.Entity<NoticeInfo>(entity =>
            {
                entity.ToTable("NoticeInfo");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NoticeEndDate).HasColumnType("datetime");

                entity.Property(e => e.NoticeTitle).HasMaxLength(450);

                entity.Property(e => e.Remarks).HasMaxLength(450);

                entity.Property(e => e.UploadPdffile)
                    .HasMaxLength(550)
                    .HasColumnName("UploadPDFFile");

                entity.HasOne(d => d.NoticeType)
                    .WithMany(p => p.NoticeInfos)
                    .HasForeignKey(d => d.NoticeTypeId)
                    .HasConstraintName("FK_NoticeInfo_NoticeType");
            });

            modelBuilder.Entity<NoticeType>(entity =>
            {
                entity.ToTable("NoticeType");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<PayablePaid>(entity =>
            {
                entity.ToTable("PayablePaid");

                entity.Property(e => e.ChequeNo).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DuePaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.HasOne(d => d.BankInfo)
                    .WithMany(p => p.PayablePaids)
                    .HasForeignKey(d => d.BankInfoId)
                    .HasConstraintName("FK_PayablePaid_EasyBikeBank");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.PayablePaids)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .HasConstraintName("FK_PayablePaid_PaymentStatus");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PayablePaids)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_PayablePaid_Supplier");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.PayablePaids)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_PayablePaid_Warehouse");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusName).HasMaxLength(500);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName).HasMaxLength(500);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductTypes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ProductType_EasyBikeType");
            });

            modelBuilder.Entity<ProductionLog>(entity =>
            {
                entity.ToTable("ProductionLog");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductionQty).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UseQty).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductionLogs)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ProductionLog_EasyBikeType");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ProductionLogs)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_ProductionLog_Inventory");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductionLogs)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_ProductionLog_ProductType");
            });

            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FeatureKey })
                    .HasName("PK_Company.RoleFeature");

                entity.ToTable("RoleFeature");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Address).HasMaxLength(550);

                entity.Property(e => e.ContactPerson).HasMaxLength(550);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.Property(e => e.ShopName).HasMaxLength(550);

                entity.Property(e => e.SupplierName).HasMaxLength(1550);

                entity.Property(e => e.Tin)
                    .HasMaxLength(50)
                    .HasColumnName("TIN");

                entity.Property(e => e.TotalAdvanceAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TotalDueAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TotalPaidAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.TradeLicenseNo).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Supplier_EasyBikeType");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.ProductTypeId)
                    .HasConstraintName("FK_Supplier_ProductType");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_Supplier_Warehouse");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.Property(e => e.CashAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ContactNo).HasMaxLength(50);

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(450);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(500);

                entity.Property(e => e.Remarks).HasMaxLength(1550);

                entity.Property(e => e.WarehouseAddress).HasMaxLength(550);

                entity.Property(e => e.WarehouseName).HasMaxLength(550);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
