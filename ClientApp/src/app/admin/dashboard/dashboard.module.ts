// //import { DashboardComponent as userDashboard } from "./../../user-dashboard/dashboard/dashboard.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgxDatatableModule } from "@swimlane/ngx-datatable";
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatStepperModule } from "@angular/material/stepper";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatSelectModule } from "@angular/material/select";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MaterialFileInputModule } from "ngx-material-file-input";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { NgxEchartsModule } from "ngx-echarts";
import { NgApexchartsModule } from "ng-apexcharts";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatMenuModule } from "@angular/material/menu";
import {NgMarqueeModule} from 'ng-marquee';
import { NgxBarcodeModule } from "ngx-barcode";
import { MatCheckboxModule } from '@angular/material/checkbox';

import { MainComponent } from "./main/main.component";
import { MatDialogModule } from '@angular/material/dialog';
import { FisheriesProductStockListComponent } from "./fisheriesproductstock-list/fisheriesproductstock-list.component";
import { FisheriesProductTypewiseCosetListComponent } from "./fisheriesproducttypewisecoset-list/fisheriesproducttypewisecoset-list.component";
import { SupplierDueAmountListComponent } from "./supplierdueamount-list/supplierdueamountList-list.component";
import { ShopProductStockListComponent } from "./shopproductstock-list/shopproductstock-list.component";
import { DailySaleAmountListComponent } from "./dailysaleamount-list/dailysaleamount-list.component";
import { DailyCostDetailListComponent } from "./dailycostdetail-list/dailycostdetail-list.component";
import { CashCapitalDetailListComponent } from "./cashcapitaldetail-list/cashcapitaldetail-list.component";
import { CashInHandDetailListComponent } from "./cashinhanddetail-list/cashinhanddetail-list.component";


@NgModule({
  declarations: [
    MainComponent,
    FisheriesProductStockListComponent,
    FisheriesProductTypewiseCosetListComponent,
    SupplierDueAmountListComponent,
    ShopProductStockListComponent,
    DailySaleAmountListComponent,
    DailyCostDetailListComponent,
    CashCapitalDetailListComponent,
    CashInHandDetailListComponent,

  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    NgxEchartsModule.forRoot({
      echarts: () => import("echarts"),
    }),
    PerfectScrollbarModule,
    MatIconModule,
    NgApexchartsModule,
    MatButtonModule,
    MatMenuModule,
    PerfectScrollbarModule,
    MatIconModule,
    NgApexchartsModule,
    MatButtonModule,
    MatMenuModule,
    CommonModule,
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
    MatSelectModule,
    MatDatepickerModule,
    MaterialFileInputModule,
    NgMarqueeModule,
    NgxBarcodeModule,
    MatDialogModule,
    MatCheckboxModule,
  ],
})
export class DashboardModule {}
