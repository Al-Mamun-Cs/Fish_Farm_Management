import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { DepositorListComponent } from './depositor/depositor-list/depositor-list.component';
import { NewDepositorComponent } from './depositor/new-depositor/new-depositor.component';
import { DepositorInstallmentListComponent } from './depositorinstallment/depositorinstallment-list/depositorinstallment-list.component';
import { NewDepositorInstallmentComponent } from './depositorinstallment/new-depositorinstallment/new-depositorinstallment.component';



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








  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class CapitalSixRoutingModule { }
