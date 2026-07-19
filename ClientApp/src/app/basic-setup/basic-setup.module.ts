import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';

import {NewWarehouseComponent} from './warehouse/new-warehouse/new-warehouse.component';
import {WarehouseListComponent} from './warehouse/warehouse-list/warehouse-list.component';

import {NewSupplierComponent} from './supplier/new-supplier/new-supplier.component';
import {SupplierListComponent} from './supplier/supplier-list/supplier-list.component';

import {NewPaymentStatusComponent} from './paymentstatus/new-paymentstatus/new-paymentstatus.component';
import {PaymentStatusListComponent} from './paymentstatus/paymentstatus-list/paymentstatus-list.component';
import { DivisionListComponent } from './division/division-list/division-list.component';
import { NewDivisionComponent } from './division/new-division/new-division.component';
import { DistrictListComponent } from './district/district-list/district-list.component';
import { NewDistrictComponent } from './district/new-district/new-district.component';
import { UpozilaListComponent } from './upozila/upozila-list/upozila-list.component';
import { NewUpozilaComponent } from './upozila/new-upozila/new-upozila.component';
import { BrandListComponent } from './brand/brand-list/brand-list.component';
import { NewBrandComponent } from './brand/new-brand/new-brand.component';
import { DesignationListComponent } from './designation/designation-list/designation-list.component';
import { NewDesignationComponent } from './designation/new-designation/new-designation.component';
import { CountryListComponent } from './country/country-list/country-list.component';
import { NewCountryComponent } from './country/new-country/new-country.component';
import { NewBankComponent } from './bank/new-bank/new-bank.component';
import { BankListComponent } from './bank/bank-list/bank-list.component';
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
import { DailyCostVaucherReasonListComponent } from './dailycostvaucherreason/dailycostvaucherreason-list/dailycostvaucherreason-list.component';
import { NewDailyCostVaucherReasonComponent } from './dailycostvaucherreason/new-dailycostvaucherreason/new-dailycostvaucherreason.component';


@NgModule({
  declarations: [
    WarehouseListComponent,
    NewWarehouseComponent,
    NewPaymentStatusComponent,
    PaymentStatusListComponent,
    FisheriesUnitListComponent,
    NewFisheriesUnitComponent,
    SupplierListComponent,
    NewSupplierComponent,
    DivisionListComponent,
    NewDivisionComponent,
    DistrictListComponent,
    NewDistrictComponent,
    UpozilaListComponent,
    NewUpozilaComponent,
    BrandListComponent,
    NewBrandComponent,
    DesignationListComponent,
    NewDesignationComponent,
    CountryListComponent,
    NewCountryComponent,
    BankListComponent,
    NewBankComponent,
    ReligionListComponent,
    NewReligionComponent,
    FiscalyearListComponent,
    NewFiscalyearComponent,
    FisheriesProductTypeListComponent,
    NewFisheriesProductTypeComponent,
    PondListComponent,
    NewPondComponent,
    DailyCostVaucherReasonListComponent,
    NewDailyCostVaucherReasonComponent,


    

  ],
  imports: [
    CommonModule,
    BasicSetupRoutingModule,
    CommonModule,
    FormsModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MaterialFileInputModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatRadioModule,
    MatCheckboxModule,
    
  ]
})
export class BasicSetupModule { }
