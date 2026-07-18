import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { FisheriesInventoryOutListComponent } from './fisheriesinventoryout/fisheriesinventoryout-list/fisheriesinventoryout-list.component';
import { NewFisheriesInventoryOutComponent } from './fisheriesinventoryout/new-fisheriesinventoryout/new-fisheriesinventoryout.component';


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








  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class StockConsumptionRoutingModule { }
