import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { DepositorListComponent } from './depositor/depositor-list/depositor-list.component';
import { NewDepositorComponent } from './depositor/new-depositor/new-depositor.component';
import { DepositorInstallmentListComponent } from './depositorinstallment/depositorinstallment-list/depositorinstallment-list.component';
import { NewDepositorInstallmentComponent } from './depositorinstallment/new-depositorinstallment/new-depositorinstallment.component';
import { DepositorInvestmentListComponent } from './depositorinvestment/depositorinvestment-list/depositorinvestment-list.component';
import { NewDepositorInvestmentComponent } from './depositorinvestment/new-depositorinvestment/new-depositorinvestment.component';
import { InvestmentIncomeListComponent } from './investmentincome/investmentincome-list/investmentincome-list.component';
import { NewInvestmentIncomeComponent } from './investmentincome/new-investmentincome/new-investmentincome.component';



const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'depositor-list',
    component: DepositorListComponent,
  },
  {
    path: 'update-depositor/:depositorId',
    component: NewDepositorComponent,
  },
  {
    path: 'add-depositor',
    component: NewDepositorComponent,
  },

  {
    path: 'depositorinstallment-list',
    component: DepositorInstallmentListComponent,
  },
  {
    path: 'update-depositorinstallment/:depositorInstallmentId',
    component: NewDepositorInstallmentComponent,
  },
  {
    path: 'add-depositorinstallment',
    component: NewDepositorInstallmentComponent,
  },

  {
    path: 'depositorinvestment-list',
    component: DepositorInvestmentListComponent,
  },
  {
    path: 'update-depositorinvestment/:depositorInvestmentId',
    component: NewDepositorInvestmentComponent,
  },
  {
    path: 'add-depositorinvestment',
    component: NewDepositorInvestmentComponent,
  },

  {
    path: 'investmentincome-list',
    component: InvestmentIncomeListComponent,
  },
  {
    path: 'update-investmentincome/:investmentIncomeId',
    component: NewInvestmentIncomeComponent,
  },
  {
    path: 'add-investmentincome',
    component: NewInvestmentIncomeComponent,
  },








  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class CapitalSixRoutingModule { }
