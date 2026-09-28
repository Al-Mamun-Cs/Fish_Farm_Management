import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { SupplierLedgerListComponent } from './supplierledger/supplierledger-list/supplierledger-list.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

{
    path: 'supplierledger-list',
    component: SupplierLedgerListComponent,
  },




  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ReportManagementRoutingModule { }
