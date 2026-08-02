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
import { CapitalSixRoutingModule } from './capital-six-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NgxBarcodeModule } from "ngx-barcode";
import { MatRadioModule } from '@angular/material/radio';
import { DepositorListComponent } from './depositor/depositor-list/depositor-list.component';
import { NewDepositorComponent } from './depositor/new-depositor/new-depositor.component';
import { DepositorInstallmentListComponent } from './depositorinstallment/depositorinstallment-list/depositorinstallment-list.component';
import { NewDepositorInstallmentComponent } from './depositorinstallment/new-depositorinstallment/new-depositorinstallment.component';
import { DepositorInvestmentListComponent } from './depositorinvestment/depositorinvestment-list/depositorinvestment-list.component';
import { NewDepositorInvestmentComponent } from './depositorinvestment/new-depositorinvestment/new-depositorinvestment.component';
import { InvestmentIncomeListComponent } from './investmentincome/investmentincome-list/investmentincome-list.component';
import { NewInvestmentIncomeComponent } from './investmentincome/new-investmentincome/new-investmentincome.component';


@NgModule({
  declarations: [
    DepositorListComponent,
    NewDepositorComponent,
    DepositorInstallmentListComponent,
    NewDepositorInstallmentComponent,
    DepositorInvestmentListComponent,
    NewDepositorInvestmentComponent,
    InvestmentIncomeListComponent,
    NewInvestmentIncomeComponent,


  ],
  imports: [
    CommonModule,
    CapitalSixRoutingModule,
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
    NgxBarcodeModule

  ]
})
export class CapitalSixModule { }
