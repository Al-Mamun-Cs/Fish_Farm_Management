import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main/main.component';
import { FisheriesProductStockListComponent } from './fisheriesproductstock-list/fisheriesproductstock-list.component';
import { FisheriesProductTypewiseCosetListComponent } from "./fisheriesproducttypewisecoset-list/fisheriesproducttypewisecoset-list.component";
import { SupplierDueAmountListComponent } from "./supplierdueamount-list/supplierdueamountList-list.component";
import { ShopProductStockListComponent } from './shopproductstock-list/shopproductstock-list.component';
import { DailySaleAmountListComponent } from './dailysaleamount-list/dailysaleamount-list.component';
import { DailyCostDetailListComponent } from './dailycostdetail-list/dailycostdetail-list.component';
import { CashCapitalDetailListComponent } from './cashcapitaldetail-list/cashcapitaldetail-list.component';
import { CashInHandDetailListComponent } from './cashinhanddetail-list/cashinhanddetail-list.component';


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

  {
    path: 'shopproductstock-list/:fisheriesProductTypeId',
    component: ShopProductStockListComponent,
  },

  {
    path: 'dailysaleamount-list',
    component: DailySaleAmountListComponent,
  },

  {
    path: 'dailycostdetail-list',
    component: DailyCostDetailListComponent,
  },

  {
    path: 'cashcapitaldetail-list',
    component: CashCapitalDetailListComponent,
  },

  {
    path: 'cashinhanddetail-list',
    component: CashInHandDetailListComponent,
  },


  
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
