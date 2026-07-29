import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { FisheriesInventoryOutListComponent } from './fisheriesinventoryout/fisheriesinventoryout-list/fisheriesinventoryout-list.component';
import { NewFisheriesInventoryOutComponent } from './fisheriesinventoryout/new-fisheriesinventoryout/new-fisheriesinventoryout.component';
import { ShopGoodSaleVoucherListComponent } from './shopgoodsale/shopgoodsalevoucher-list/shopgoodsalevoucher-list.component';
import { ShopGoodSaleListComponent } from './shopgoodsale/shopgoodsale-list/shopgoodsale-list.component';
import { NewShopGoodSaleComponent } from './shopgoodsale/new-shopgoodsale/new-shopgoodsale.component';
import { FishSaleListComponent } from './fishsale/fishsale-list/fishsale-list.component';
import { NewFishSaleComponent } from './fishsale/new-fishsale/new-fishsale.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {
    path: 'fisheriesinventoryout-list',
    component: FisheriesInventoryOutListComponent,
  },
  {
    path: 'update-fisheriesinventoryout/:fisheriesInventoryOutId',
    component: NewFisheriesInventoryOutComponent,
  },
  {
    path: 'add-fisheriesinventoryout',
    component: NewFisheriesInventoryOutComponent,
  },

  {
    path: 'shopgoodsalevoucher-list/:shopGoodSaleId',
    component: ShopGoodSaleVoucherListComponent,
  },

  {
    path: 'shopgoodsale-list',
    component: ShopGoodSaleListComponent,
  },
  {
    path: 'add-shopgoodsale',
    component: NewShopGoodSaleComponent,
  },

  {
    path: 'fishsale-list',
    component: FishSaleListComponent,
  },
  {
    path: 'add-fishsale',
    component: NewFishSaleComponent,
  },







  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class StockConsumptionRoutingModule { }
