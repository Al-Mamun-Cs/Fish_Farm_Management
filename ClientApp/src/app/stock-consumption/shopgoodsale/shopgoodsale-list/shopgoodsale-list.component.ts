import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {ShopGoodSale} from '../../models/ShopGoodSale';
import { ShopGoodSaleService } from '../../service/ShopGoodSale.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-shopgoodsale-list',
  templateUrl: './shopgoodsale-list.component.html',
  styleUrls: ['./shopgoodsale-list.component.sass']
})
export class ShopGoodSaleListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: ShopGoodSale[] = [];
  isLoading = false;
  showHideDiv:any;
  pageTitle:any;
  groupArrays: { saleDate: string; datas: any }[];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  role:any;
  branchId:any;

  displayedColumns: string[] = [ 'sl','category','productType', 'actions'];
  dataSource: MatTableDataSource<ShopGoodSale> = new MatTableDataSource();
  permission: any;
  selection = new SelectionModel<ShopGoodSale>(true, []);

  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private ShopGoodSaleService:ShopGoodSaleService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)

    this.getShopGoodSales();
  }
  
  getShopGoodSales() {
    this.isLoading = true;
    this.ShopGoodSaleService.getShopGoodSales(this.paging.pageIndex, this.paging.pageSize,this.searchText, this.branchId).subscribe(response => {
      this.dataSource.data = response.items; 
      this.permission = response.permission; 

      console.log(response,"Data kroy");
      console.log(this.permission);
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;

      //Group by saleDate 
      const groups = this.dataSource.data.reduce((groups, datas) => {
        const schoolName = datas.saleDate;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(datas);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((saleDate) => {
        return {
          saleDate,
          datas: groups[saleDate],
        };
      });
      console.log(this.groupArrays,"Data kroy group");
      
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
  addNew(){
    
  }
 
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getShopGoodSales();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getShopGoodSales();
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
            <h2 style="margin:0;">Intimate Techno Hub</h2>
            <h4 style="margin:0;">Dhaka</h4>
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
  deleteItem(row) {
    const id = row.shopGoodSaleId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.ShopGoodSaleService.delete(id).subscribe(() => {
          this.getShopGoodSales();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });

        })
      }
      
    })
    
  }
}
