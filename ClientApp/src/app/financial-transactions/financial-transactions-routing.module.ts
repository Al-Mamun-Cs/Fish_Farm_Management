import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { DailyMiscellaneousCostListComponent } from './dailymiscellaneouscost/dailymiscellaneouscost-list/dailymiscellaneouscost-list.component';
import { NewDailyMiscellaneousCostComponent } from './dailymiscellaneouscost/new-dailymiscellaneouscost/new-dailymiscellaneouscost.component';
import { ShopHandCashWithdrowListComponent } from './shophandcashwithdrow/shophandcashwithdrow-list/shophandcashwithdrow-list.component';
import { NewShopHandCashWithdrowComponent } from './shophandcashwithdrow/new-shophandcashwithdrow/new-shophandcashwithdrow.component';
import { InvestmentListComponent } from './investment/investment-list/investment-list.component';
import { NewInvestmentComponent } from './investment/new-investment/new-investment.component';
import { CompanyInvestorListComponent } from './companyinvestor/companyinvestor-list/companyinvestor-list.component';
import { NewCompanyInvestorComponent } from './companyinvestor/new-companyinvestor/new-companyinvestor.component';
import { CompanyInvestorReturnListComponent } from './companyinvestorreturn/companyinvestorreturn-list/companyinvestorreturn-list.component';
import { NewCompanyInvestorReturnComponent } from './companyinvestorreturn/new-companyinvestorreturn/new-companyinvestorreturn.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'dailymiscellaneouscost-list',
    component: DailyMiscellaneousCostListComponent,
  },
  {
    path: 'update-dailymiscellaneouscost/:dailyMiscellaneousCostId',
    component: NewDailyMiscellaneousCostComponent,
  },
  {
    path: 'add-dailymiscellaneouscost',
    component: NewDailyMiscellaneousCostComponent,
  },

  {
    path: 'shophandcashwithdrow-list',
    component: ShopHandCashWithdrowListComponent,
  },
  {
    path: 'update-shophandcashwithdrow/:shopHandCashWithdrowId',
    component: NewShopHandCashWithdrowComponent,
  },
  {
    path: 'add-shophandcashwithdrow',
    component: NewShopHandCashWithdrowComponent,
  },

  {
    path: 'investment-list',
    component: InvestmentListComponent,
  },
  {
    path: 'update-investment/:shopHandCashWithdrowId',
    component: NewInvestmentComponent,
  },
  {
    path: 'add-investment',
    component: NewInvestmentComponent,
  },

  {
    path: 'companyinvestor-list',
    component: CompanyInvestorListComponent,
  },
  {
    path: 'update-companyinvestor/:companyInvestorId',
    component: NewCompanyInvestorComponent,
  },
  {
    path: 'add-companyinvestor',
    component: NewCompanyInvestorComponent,
  },

  {
    path: 'companyinvestorreturn-list',
    component: CompanyInvestorReturnListComponent,
  },
  {
    path: 'update-companyinvestorreturn/:companyInvestorReturnId',
    component: NewCompanyInvestorReturnComponent,
  },
  {
    path: 'add-companyinvestorreturn',
    component: NewCompanyInvestorReturnComponent,
  },







  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class FinancialTransactionsRoutingModule { }
