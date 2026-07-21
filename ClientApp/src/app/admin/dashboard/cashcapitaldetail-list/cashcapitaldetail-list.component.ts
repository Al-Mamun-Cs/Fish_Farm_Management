import { Component, OnInit } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Location } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';
import { DashboardService } from "../service/Dashboard.service";
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-cashcapitaldetail-list',
  templateUrl: './cashcapitaldetail-list.component.html',
  styleUrls: ['./cashcapitaldetail-list.component.sass']
})
export class CashCapitalDetailListComponent implements OnInit {
  photoBaseUrl = environment.fileUrl;
  masterData = MasterData;
  isLoading = false;
  showHideDiv: any;
  groupArrays: { warehouse: string; datas: any }[];
  pageTitle: any;
  role: any;
  branchId: any;
  supplierId: any;
  fisheriesProductTypeId: any;
  cashCapitalDetail: any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  grandTotalCash = 0;
  grandTotalBank = 0;
  grandTotalCapital = 0;
  searchText = "";
  permission: any;
  displayedColumns: string[] = ['sl', 'bankInfo', 'supplier', 'warehouse', 'transactionDate', 'amount', 'remarks', 'actions'];



  constructor(private snackBar: MatSnackBar, private authService: AuthService, private DashboardService: DashboardService, private _location: Location, private router: Router, private confirmService: ConfirmService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    this.supplierId = this.authService.currentUserValue.supplierId.toString().trim();
    console.log(this.role, this.branchId, this.supplierId, "employee Id")
    this.fisheriesProductTypeId = this.route.snapshot.paramMap.get('fisheriesProductTypeId');
    this.getCashCapitalDetailList();
  }
  backClicked() {
    this._location.back();
  }
  getCashCapitalDetailList() {
    this.isLoading = true;
    this.DashboardService.getCashCapitalDetailList(this.branchId,).subscribe(response => {
      this.cashCapitalDetail = response;
      this.getGrandTotal();
      console.log(this.cashCapitalDetail, "product Stock data")
      this.isLoading = false;
      //Group by category 
      // const groups = this.dataSource.data.reduce((groups, datas) => {
      //   const schoolName = datas.warehouse;
      //   if (!groups[schoolName]) {
      //     groups[schoolName] = [];
      //   }
      //   groups[schoolName].push(datas);
      //   return groups;
      // }, {});

      // // Edit: to add it in the array format instead
      // this.groupArrays = Object.keys(groups).map((warehouse) => {
      //   return {
      //     warehouse,
      //     datas: groups[warehouse],
      //   };
      // });
    })
  }
  // get grandTotalCash(): number {
  //   return this.cashCapitalDetail?.reduce((sum, item) => {
  //     return sum + Number(item.cashAmount || 0);
  //   }, 0) || 0;
  // }
  // get grandTotalBank(): number {
  //   return this.cashCapitalDetail?.reduce((sum, item) => {
  //     return sum + Number(item.bankBalance || 0);
  //   }, 0) || 0;
  // }
  getGrandTotal() {
  this.grandTotalCash = this.cashCapitalDetail.reduce(
    (sum, item) => sum + (item.cashAmount || 0), 0);

  this.grandTotalBank = this.cashCapitalDetail.reduce(
    (sum, item) => sum + (item.bankBalance || 0), 0);

  this.grandTotalCapital = this.cashCapitalDetail.reduce(
    (sum, item) => sum + (item.cashAmount || 0) + (item.bankBalance || 0), 0);
}

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }


  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getCashCapitalDetailList();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getCashCapitalDetailList();
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
            <h4 style="margin:0;">মূলধনের  বিবরণ</h4>
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

}
