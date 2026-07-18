import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';

import { WarehouseListComponent } from './warehouse/warehouse-list/warehouse-list.component';
import { NewWarehouseComponent } from './warehouse/new-warehouse/new-warehouse.component';
import { SupplierListComponent } from './supplier/supplier-list/supplier-list.component';
import { NewSupplierComponent } from './supplier/new-supplier/new-supplier.component';

import { NewPaymentStatusComponent } from './paymentstatus/new-paymentstatus/new-paymentstatus.component';
import { PaymentStatusListComponent } from './paymentstatus/paymentstatus-list/paymentstatus-list.component';
import { DivisionListComponent } from './division/division-list/division-list.component';
import { NewDivisionComponent } from './division/new-division/new-division.component';
import { DistrictListComponent } from './district/district-list/district-list.component';
import { NewDistrictComponent } from './district/new-district/new-district.component';
import { UpozilaListComponent } from './upozila/upozila-list/upozila-list.component';
import { NewUpozilaComponent } from './upozila/new-upozila/new-upozila.component';
import { BrandListComponent } from './brand/brand-list/brand-list.component';
import { NewBrandComponent } from './brand/new-brand/new-brand.component';
import { NewDesignationComponent } from './designation/new-designation/new-designation.component';
import { DesignationListComponent } from './designation/designation-list/designation-list.component';
import { CountryListComponent } from './country/country-list/country-list.component';
import { NewCountryComponent } from './country/new-country/new-country.component';
import { BankListComponent } from './bank/bank-list/bank-list.component';
import { NewBankComponent } from './bank/new-bank/new-bank.component';
import { NewReligionComponent } from './religion/new-religion/new-religion.component';
import { ReligionListComponent } from './religion/religion-list/religion-list.component';
import { FiscalyearListComponent } from './fiscalyear/fiscalyear-list/fiscalyear-list.component';
import { NewFiscalyearComponent } from './fiscalyear/new-fiscalyear/new-fiscalyear.component';
import { FisheriesUnitListComponent } from './fisheriesunit/fisheriesunit-list/fisheriesunit-list.component';
import { NewFisheriesUnitComponent } from './fisheriesunit/new-fisheriesunit/new-fisheriesunit.component';
import { FisheriesProductTypeListComponent } from './fisheriesproducttype/fisheriesproducttype-list/fisheriesproducttype-list.component';
import { NewFisheriesProductTypeComponent } from './fisheriesproducttype/new-fisheriesproducttype/new-fisheriesproducttype.component';
import { PondListComponent } from './pond/pond-list/pond-list.component';
import { NewPondComponent } from './pond/new-pond/new-pond.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },



  {
    path: 'fisheriesproducttype-list',
    component: FisheriesProductTypeListComponent,
  },
  {
    path: 'update-fisheriesproducttype/:fisheriesProductTypeId',
    component: NewFisheriesProductTypeComponent,
  },
  {
    path: 'add-fisheriesproducttype',
    component: NewFisheriesProductTypeComponent,
  },
  

  {
    path: 'fiscalyear-list',
    component: FiscalyearListComponent,
  },
  {
    path: 'update-fiscalyear/:fiscalyearId',
    component: NewFiscalyearComponent,
  },
  {
    path: 'add-fiscalyear',
    component: NewFiscalyearComponent,
  },

  {
    path: 'religion-list',
    component: ReligionListComponent,
  },
  {
    path: 'update-religion/:religionId',
    component: NewReligionComponent,
  },
  {
    path: 'add-religion',
    component: NewReligionComponent,
  },




  {
    path: 'country-list',
    component: CountryListComponent,
  },
  {
    path: 'update-country/:countryId',
    component: NewCountryComponent,
  },
  {
    path: 'add-country',
    component: NewCountryComponent,
  },

  {
    path: 'designation-list',
    component: DesignationListComponent,
  },
  {
    path: 'update-designation/:designationId',
    component: NewDesignationComponent,
  },
  {
    path: 'add-designation',
    component: NewDesignationComponent,
  },


  {
    path: 'bank-list',
    component: BankListComponent,
  },
  {
    path: 'update-bank/:bankId',
    component: NewBankComponent,
  },
  {
    path: 'add-bank',
    component: NewBankComponent,
  },

  {
    path: 'brand-list',
    component: BrandListComponent,
  },
  {
    path: 'update-brand/:brandId',
    component: NewBrandComponent,
  },
  {
    path: 'add-brand',
    component: NewBrandComponent,
  },

  {
    path: 'division-list',
    component: DivisionListComponent,
  },
  {
    path: 'update-division/:divisionId',
    component: NewDivisionComponent,
  },
  {
    path: 'add-division',
    component: NewDivisionComponent,
  },

  {
    path: 'district-list',
    component: DistrictListComponent,
  },
  {
    path: 'update-district/:districtId',
    component: NewDistrictComponent,
  },
  {
    path: 'add-district',
    component: NewDistrictComponent,
  },

  {
    path: 'upozila-list',
    component: UpozilaListComponent,
  },
  {
    path: 'update-upozila/:upazilaId',
    component: NewUpozilaComponent,
  },
  {
    path: 'add-upozila',
    component: NewUpozilaComponent,
  },

  

  {
    path: 'supplier-list',
    component: SupplierListComponent,
  },
  {
    path: 'update-supplier/:supplierId',
    component: NewSupplierComponent,
  },
  {
    path: 'add-supplier',
    component: NewSupplierComponent,
  },


  {
    path: 'warehouse-list',
    component: WarehouseListComponent,
  },
  {
    path: 'update-warehouse/:warehouseId',
    component: NewWarehouseComponent,
  },
  {
    path: 'add-warehouse',
    component: NewWarehouseComponent,
  },


  {
    path: 'paymentstatus-list',
    component: PaymentStatusListComponent,
  },
  {
    path: 'update-paymentstatus/:paymentStatusId',
    component: NewPaymentStatusComponent,
  },
  {
    path: 'add-paymentstatus',
    component: NewPaymentStatusComponent,
  },

  {
    path: 'fisheriesunit-list',
    component: FisheriesUnitListComponent,
  },
  {
    path: 'update-fisheriesunit/:fisheriesUnitId',
    component: NewFisheriesUnitComponent,
  },
  {
    path: 'add-fisheriesunit',
    component: NewFisheriesUnitComponent,
  },

  {
    path: 'pond-list',
    component: PondListComponent,
  },
  {
    path: 'update-pond/:pondId',
    component: NewPondComponent,
  },
  {
    path: 'add-pond',
    component: NewPondComponent,
  },







  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class BasicSetupRoutingModule { }
