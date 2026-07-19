import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShopInventory } from '../../models/ShopInventory';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { ShopInventoryService } from '../../service/ShopInventory.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';


@Component({
  selector: 'app-shopinventoryvoucher-list',
  templateUrl: './shopinventoryvoucher-list.component.html',
  styleUrls: ['./shopinventoryvoucher-list.component.sass']
})
export class ShopInventoryVoucherListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: ShopInventory[] = [];
  isLoading = false;
  MosariSaleForm: FormGroup;
  showHideDiv: any;
  showVoucher: boolean = false;
  pageTitle: any;
  groupArrays: { category: string; datas: any }[];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText = "";

  role: any;
  branchId: any;
  inventoryList: any[];
  supplier: any;
  supplierAddress: any;
  supplierPhone: any;
  voucherNo: any;
  purchaseDate: any;
  warehouse: any;
  transportCost: any;
  totalPurchasePrice: any;
  lessAmount: any;
  paidAmount: any;
  dueAmount: any;
  statusName: any;
  bankName: any;
  weightingScaleNo: any;
  remarks: any;

  displayedColumns: string[] = ['sl', 'category', 'productType', 'availableQty', 'warehouse', 'supplier', 'paymentStatus', 'purchasePrice', 'totalPurchasePrice', 'paidAmount', 'dueAmount', 'purchaseDate', 'actions'];
  dataSource: MatTableDataSource<ShopInventory> = new MatTableDataSource();
  permission: any;
  selection = new SelectionModel<ShopInventory>(true, []);


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private fb: FormBuilder, private ShopInventoryService: ShopInventoryService, private router: Router, private confirmService: ConfirmService, private route: ActivatedRoute, private _location: Location,) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('shopInventoryId');
    this.intitializeForm();
    this.SpGetShopInventoryVoucherById(id);
  }
  backClicked() {
    this._location.back();
  }

  SpGetShopInventoryVoucherById(shopInventoryId: any) {
    this.ShopInventoryService.SpGetShopInventoryVoucherById(shopInventoryId).subscribe(response => {
      this.inventoryList = response;
      console.log(this.inventoryList, "inventoryList list")
      this.supplier = response[0].supplierName;
      this.supplierAddress = response[0].supplierAddress;
      this.supplierPhone = response[0].supplierPhone;
      this.voucherNo = response[0].voucherNo;
      this.purchaseDate = response[0].purchaseDate;
      this.warehouse = response[0].warehouseName;
      this.transportCost = response[0].transportCost;
      this.totalPurchasePrice = response[0].totalPurchasePrice;
      this.lessAmount = response[0].lessAmount;
      this.paidAmount = response[0].paidAmount;
      this.dueAmount = response[0].dueAmount;
      this.statusName = response[0].statusName;
      this.bankName = response[0].bankName;
      this.weightingScaleNo = response[0].weightingScaleNo;
      this.remarks = response[0].remarks;

      this.printVoucher(shopInventoryId);
      console.log(this.inventoryList, "data")
      console.log("this.dataSource.data")

    })
  }
  intitializeForm() {
    this.MosariSaleForm = this.fb.group({
      shopInventoryId: [],

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
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
  }

  toggle() {
    this.showHideDiv = !this.showHideDiv;
  }
  printVoucher(value: number) {
    console.log("uuu");
    console.log(value);
    this.showHideDiv = false;
    this.showVoucher = true;
  }
  printSingle() {
    this.showHideDiv = false;
    this.print();
  }

  print() {
    let printContents, popupWin;
    printContents = document.getElementById("print-routine").innerHTML;
    popupWin = window.open("top=0,left=0,height=100%,width=auto");
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
        
                    tr td{
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
                  /* 🔽 Watermark styles */
          .watermark-container {
            position: relative;
          }

          .watermark-logo {
            position: absolute;
            top: 100px; /* Adjust as needed */
            left: 50%;
            transform: translateX(-50%);
            width: 300px;
            opacity: 0.06;
            z-index: 0;
            pointer-events: none;
          }
                  .footer {
            position: fixed;
            bottom: 0;
            left: 0;
            width: 100%;
            padding: 10px 20px;
            display: flex;
            justify-content: space-between;
            box-sizing: border-box;
            font-weight: bold;
            border-top: 1px solid #ccc;
            background: #fff;
          }
          @media print {
            body {
              margin: 0;
            }
              @media print {
                .watermark-logo {
                  position: fixed;
                  top: 50%;
                  left: 50%;
                  transform: translate(-50%, -50%);
                  width: 300px;
                  opacity: 0.06;
                  z-index: -1;
                  pointer-events: none;
                  page-break-inside: avoid;
                }
                  body::before {
                  content: "";
                  position: fixed;
                  top: 0;
                  left: 0;
                  width: 100vw;
                  height: 100vh;
                  background: url('assets/ITH_Logo.png') no-repeat center center;
                  background-size: 300px;
                  opacity: 0.06;
                  z-index: -1;
                  pointer-events: none;
                }

            .footer {
              position: fixed;
              bottom: 0;
            }
          }
          </style>
        </head>
        <body onload="window.print();window.close()">
       
          <div class="header-text">
          <img src="assets/images/ITH_Logo.png" alt="BMU Logo" style="height: 60px; margin-bottom: 5px; display: block; margin-left: auto; margin-right: auto;" />
            <h4 style="margin:0;"> ক্রয় রশিদ</h4>
          </div>
          <br>
          <hr>
          <div class="watermark-container">
          <!-- Watermark logo -->
          <img src="assets/images/ITH_Logo.png" class="watermark-logo" alt="Watermark Logo" />

          <!-- Actual printable content -->
          <div class="print-content">
            ${printContents}
          </div>
        </div>
          <div class="footer">
          <span>Buyer's Signature</span>
          <span>Seller's Signature</span>
        </div>
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
    var shopInventoryId = this.MosariSaleForm.value['shopInventoryId'];

    this.ShopInventoryService.SpGetShopInventoryVoucherById(shopInventoryId).subscribe(response => {
      this.inventoryList = response;
      console.log(this.inventoryList, "inventoryList list")
      this.supplier = response[0].supplierName;
      this.voucherNo = response[0].voucherNo;
      this.purchaseDate = response[0].purchaseDate;
      this.warehouse = response[0].warehouseName;
      this.transportCost = response[0].transportCost;
      this.totalPurchasePrice = response[0].totalPurchasePrice;
      this.lessAmount = response[0].lessAmount;
      this.paidAmount = response[0].paidAmount;
      this.dueAmount = response[0].dueAmount;

      this.printVoucher(shopInventoryId);
      console.log(this.inventoryList, "data")
      console.log("this.dataSource.data")

    })

  }

}

