using static SchoolManagement.Shared.Constants;

namespace SchoolManagement.Application
{
    public static class SMSRoutePrefix
    {
        private const string SMSRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "sms/";

        public const string Designation = SMSRoutePrefixBase + "designation";
        public const string Dashboard = SMSRoutePrefixBase + "dashboard";
        public const string Warehouse = SMSRoutePrefixBase + "warehouse";
        public const string Supplier = SMSRoutePrefixBase + "supplier";
        public const string PaymentStatus = SMSRoutePrefixBase + "payment-status";
        public const string Division = SMSRoutePrefixBase + "Division";
        public const string District = SMSRoutePrefixBase + "district";
        public const string Upozila = SMSRoutePrefixBase + "upozila";
        public const string SupplierType = SMSRoutePrefixBase + "supplier-type";
        public const string Brand = SMSRoutePrefixBase + "brand";
        public const string Country = SMSRoutePrefixBase + "country";
        public const string Religion = SMSRoutePrefixBase + "religion";
        public const string BloodGroup = SMSRoutePrefixBase + "blood-group";
        public const string Gender = SMSRoutePrefixBase + "gender";
        public const string Bank = SMSRoutePrefixBase + "bank";
        public const string Fiscalyear = SMSRoutePrefixBase + "fiscal-year";
        public const string FisheriesUnit = SMSRoutePrefixBase + "fisheries-unit";
        public const string FisheriesProductType = SMSRoutePrefixBase + "fisheries-product-type";
        public const string Pond = SMSRoutePrefixBase + "pond";
        public const string FisheriesInventory = SMSRoutePrefixBase + "fisheries-inventory";
        public const string FisheriesInventoryOut = SMSRoutePrefixBase + "fisheries-inventory-out";
        public const string ShopInventory = SMSRoutePrefixBase + "shop-inventory";
        public const string DailyCostVaucherReason = SMSRoutePrefixBase + "daily-cost-vaucher-reason";
        public const string DailyMiscellaneousCost = SMSRoutePrefixBase + "daily-miscellaneous-cost";
        public const string ShopGoodSale = SMSRoutePrefixBase + "shop-good-sale";

        public const string BackupDatabase = SMSRoutePrefixBase + "backup-database";
    }
} 
