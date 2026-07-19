import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main/main.component';
import { FisheriesProductStockListComponent } from './fisheriesproductstock-list/fisheriesproductstock-list.component';
import { FisheriesProductTypewiseCosetListComponent } from "./fisheriesproducttypewisecoset-list/fisheriesproducttypewisecoset-list.component";
import { SupplierDueAmountListComponent } from "./supplierdueamount-list/supplierdueamountList-list.component";


const routes: Routes = [
  {
    path: '',
    redirectTo: 'main',
    pathMatch: 'full',
  },


  {
    path: 'main',
    component: MainComponent,
  },

  {
    path: 'fisheriesproductstock-list/:fisheriesProductTypeId',
    component: FisheriesProductStockListComponent,
  },

  {
    path: 'fisheriesproducttypewisecoset-list/:pondId',
    component: FisheriesProductTypewiseCosetListComponent,
  },

  {
    path: 'supplierdueamount-list',
    component: SupplierDueAmountListComponent,
  },


  
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
