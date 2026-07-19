import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { DailyMiscellaneousCostListComponent } from './dailymiscellaneouscost/dailymiscellaneouscost-list/dailymiscellaneouscost-list.component';
import { NewDailyMiscellaneousCostComponent } from './dailymiscellaneouscost/new-dailymiscellaneouscost/new-dailymiscellaneouscost.component';


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








  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class FinancialTransactionsRoutingModule { }
