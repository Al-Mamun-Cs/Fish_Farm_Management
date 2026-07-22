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
import { FinancialTransactionsRoutingModule } from './financial-transactions-routing.module';
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
import { DailyMiscellaneousCostListComponent } from './dailymiscellaneouscost/dailymiscellaneouscost-list/dailymiscellaneouscost-list.component';
import { NewDailyMiscellaneousCostComponent } from './dailymiscellaneouscost/new-dailymiscellaneouscost/new-dailymiscellaneouscost.component';
import { ShopHandCashWithdrowListComponent } from './shophandcashwithdrow/shophandcashwithdrow-list/shophandcashwithdrow-list.component';
import { NewShopHandCashWithdrowComponent } from './shophandcashwithdrow/new-shophandcashwithdrow/new-shophandcashwithdrow.component';


@NgModule({
  declarations: [
    DailyMiscellaneousCostListComponent,
    NewDailyMiscellaneousCostComponent,
    ShopHandCashWithdrowListComponent,
    NewShopHandCashWithdrowComponent,
    
  ],
  imports: [
    CommonModule,
    FinancialTransactionsRoutingModule,
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
export class FinancialTransactionsModule { }
