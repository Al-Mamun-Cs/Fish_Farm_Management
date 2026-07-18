import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';

import { InvBillVoucherListComponent } from './inventory/invbillvoucher-list/invbillvoucher-list.component';
import { FisheriesInventoryListComponent } from './inventory/fisheriesinventory-list/fisheriesinventory-list.component';
import { NewFisheriesInventoryComponent } from './inventory/new-fisheriesinventory/new-fisheriesinventory.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },




  {
    path: 'invbillvoucher-list/:fisheriesInventoryId',
    component: InvBillVoucherListComponent,
  },

  {
    path: 'fisheriesinventory-list',
    component: FisheriesInventoryListComponent,
  },
  {
    path: 'add-fisheriesinventory',
    component: NewFisheriesInventoryComponent,
  },






  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class FishProductStockRoutingModule { }
