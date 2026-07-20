import { Page404Component } from "./authentication/page404/page404.component";
import { AuthLayoutComponent } from "./layout/app-layout/auth-layout/auth-layout.component";
import { MainLayoutComponent } from "./layout/app-layout/main-layout/main-layout.component";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./core/guard/auth.guard";
import { Role } from "./core/models/role";
const routes: Routes = [
  {
    path: "",
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: "admin",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./admin/admin.module").then((m) => m.AdminModule),
      },
      {
        path: "basic-setup",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./basic-setup/basic-setup.module").then(
            (m) => m.BasicSetupModule
          ),
      },


      {
        path: "fishproduct-stock",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./fishproduct-stock/fishproduct-stock.module").then(
            (m) => m.FishProductStockModule
          ),
      },
      


      {
        path: "report",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./report/report.module").then(
            (m) => m.ReportModule
          ),
      },

      {
        path: 'notification',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import('./notification/notification.module').then((m) => m.NotificationModule),
      },


      {
        path: "stock-consumption",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./stock-consumption/stock-consumption.module").then(
            (m) => m.StockConsumptionModule
          ),
      },

      {
        path: "financial-transactions",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./financial-transactions/financial-transactions.module").then(
            (m) => m.FinancialTransactionsModule
          ),
      },



      {
        path: "report-management",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import("./report-management/report-management.module").then(
            (m) => m.ReportManagementModule
          ),
      },

      {
        path: 'password',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager, Role.Dealer, Role.Sr, Role.Production, Role.Payroll, Role.Salary, Role.HoD, Role.Accounts, Role.CFO, Role.HR, Role.Employee, Role.EshopMembership, Role.FisheriesManager],
        },
        loadChildren: () =>
          import('./password/password.module').then((m) => m.PasswordModule),
      },


      {
        path: "security",
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin, Role.SuperAdmin, Role.GodownManager],
        },
        loadChildren: () =>
          import("./security/security.module").then((m) => m.SecurityModule),
      },

      { path: "", redirectTo: "/authentication/signin", pathMatch: "full" },


    ],
  },
  {
    path: "authentication",
    component: AuthLayoutComponent,
    loadChildren: () =>
      import("./authentication/authentication.module").then(
        (m) => m.AuthenticationModule
      ),
  },
  { path: "**", component: Page404Component },
];
@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
