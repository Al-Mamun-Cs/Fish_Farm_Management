import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Salary } from '../../models/Salary';
import { PurchaseService } from '../../service/Purchase.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SelectedModel } from 'src/app/core/models/selectedModel';


@Component({
  selector: 'app-supplierledger-list',
  templateUrl: './supplierledger-list.component.html',
  styleUrls: ['./supplierledger-list.component.sass']
})
export class SupplierLedgerListComponent implements OnInit {
  masterData = MasterData;
  purchaseList: any;
  CountPurchaseList: any;
  isLoading = false;
  showHideDiv: any;
  PurchaseForm: FormGroup;
  warehouseList: SelectedModel[];
  supplierList: SelectedModel[];
  groupArrays: { warehouse: string; datas: any }[];
  pageTitle: any;
  totalPaidAmount: number = 0;
  totalPurchaseAmount: number = 0;
  role: any;
  branchId: any;
  isShown: boolean = false;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 1000,
    length: 1
  }
  btnText: string;
  searchText = "";
  permission: any;
  dataSource: MatTableDataSource<any> = new MatTableDataSource();

  selection = new SelectionModel<any>(true, []);
  dataTable: any[] = [];
  groupedDataTable = [];
  totalSums = { availableQty: 0, purchasePrice: 0, totalPurchasePrice: 0, totalPaidAmount: 0, totalDueAmount: 0 };


  constructor(private snackBar: MatSnackBar, private datepipe: DatePipe, private fb: FormBuilder, private authService: AuthService, private PurchaseService: PurchaseService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.SPSupplierLedgerReport();

    this.intitializeForm();
    this.PurchaseForm.get('dateFrom').setValue(new Date);
    this.PurchaseForm.get('dateTo').setValue(new Date);
    this.GetSupplierList();
    this.btnText = 'Submit';
  }

  GetSupplierList() {
    this.PurchaseService.GetSupplierList(this.branchId, 2).subscribe(res => {
      this.supplierList = res;
    });
  }


  SPSupplierLedgerReport() {
    let currentDateTime = this.datepipe.transform((new Date), 'MM/dd/yyyy');
    let supplierId = 0

    this.PurchaseService.SPSupplierLedgerReport(supplierId, currentDateTime, currentDateTime).subscribe(response => {
      this.purchaseList = response;
      console.log(this.purchaseList, "category slist22")

      this.calculateGrandTotal();

      console.log('Amount Paid Grand Total:', this.totalPaidAmount);
      console.log('Total Purchase Grand Total:', this.totalPurchaseAmount);
    })
  }

  calculateGrandTotal() {
    this.totalPaidAmount = this.purchaseList.reduce(
      (total, item) => total + Number(item.amountPaid || 0),
      0
    );

    this.totalPurchaseAmount = this.purchaseList.reduce(
      (total, item) => total + Number(item.totalPurched || 0),
      0
    );
  }


  intitializeForm() {
    this.PurchaseForm = this.fb.group({

      supplierId: [0],
      dateFrom: [''],
      dateTo: ['']

    })
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.filteredData.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.filteredData.forEach((row) =>
        this.selection.select(row)
      );
  }
  addNew() {

  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.SPSupplierLedgerReport();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.SPSupplierLedgerReport();
  }

  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }
  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open("", "_blank", "top=0,left=0,height=100%,width=auto");
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
          body{  width: 99%;}
            label { font-weight: 400;
                    font-size: 13px;
                    padding: 2px;
                    margin-bottom: 5px;
                  }
            table, td, th {
                  border: 1px solid silver;
                    }
                    table td {
                  font-size: 13px;
                    }
                  
                    .table.table.tbl-by-group.db-li-s-in tr .cl-action{
                      display: none;
                    }  
        
                    .table.table.tbl-by-group.db-li-s-in tr td{
                      text-align:center;
                      padding: 0px 5px;
                    }
                    table th {
                  font-size: 13px;
                    }
              table {
                    border-collapse: collapse;
                    width: 98%;
                    }
                th {
                    height: 26px;
                    }
                .header-text{
                  text-align:center;
                }
                .header-text h3{
                  margin:0;
                }
          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
            <h3>Supplier Ledger Report </h3>
          </div>
          <br>
          <hr>
          ${printContents}
          <script type="text/javascript">
            window.onload = function() {
              window.print();
              setTimeout(function() { window.close(); }, 100);
            }
          </script>
        </body>
      </html>`);
    popupWin.document.close();
  }
  onSubmit() {
    var supplierId = this.PurchaseForm.value['supplierId'];
    var dateFrom = this.PurchaseForm.value['dateFrom'];
    var dateTo = this.PurchaseForm.value['dateTo'];

    let newDateFrom = new Date(dateFrom);
    let newDateTo = new Date(dateTo);
    let checkdateFrom = this.datepipe.transform((newDateFrom), 'MM/dd/yyyy');
    let checkdateTo = this.datepipe.transform((newDateTo), 'MM/dd/yyyy');
    console.log(checkdateFrom, checkdateTo)
    this.PurchaseService.SPSupplierLedgerReport(supplierId, checkdateFrom, checkdateTo,).subscribe(response => {
      this.purchaseList = response;
      console.log("List")
      console.log(this.purchaseList)

      this.calculateGrandTotal();

      console.log('Amount Paid Grand Total:', this.totalPaidAmount);
      console.log('Total Purchase Grand Total:', this.totalPurchaseAmount);
    })

  }
}
