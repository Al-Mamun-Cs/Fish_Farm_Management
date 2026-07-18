import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Supplier } from '../../models/Supplier';
import { SupplierService } from '../../service/Supplier.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.sass']
})
export class SupplierListComponent implements OnInit {
  photoBaseUrl = environment.fileUrl;
  masterData = MasterData;
  ELEMENT_DATA: Supplier[] = [];
  isLoading = false;
  showHideDiv: any;
  pageTitle: any;
  groupArrays: { warehouse: string; datas: any }[];
  SupplierForm: FormGroup;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText = "";
  permission: any;
  role: any;
  branchId: any;
  supplierStatus: any;
  displayedColumns: string[] = ['sl', 'warehouse', 'supplierName', 'shopName', 'address', 'email', 'actions'];
  dataSource: MatTableDataSource<Supplier> = new MatTableDataSource();
  selection = new SelectionModel<Supplier>(true, []);

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private SupplierService: SupplierService, private router: Router, private fb: FormBuilder, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getSuppliers();

    this.intitializeForm();
  }

  intitializeForm() {
    this.SupplierForm = this.fb.group({
      supplierId: [0],
      supplierStatus: [],

    })

  }

  getSuppliers() {
    this.isLoading = true;
    this.SupplierService.getSuppliers(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {
      this.dataSource.data = response.items;
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      console.log(this.dataSource.data, "Data")
      //Group by warehouse 
      const groups = this.dataSource.data.reduce((groups, datas) => {
        const schoolName = datas.warehouse;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(datas);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((warehouse) => {
        return {
          warehouse,
          datas: groups[warehouse],
        };
      });
      console.log(this.groupArrays, "Group Data")
    })
  }
  getEmployeeImage(path: string): string {
    if (!path) return 'assets/no-image.png'; // fallback
    return this.photoBaseUrl + path;
  }
  // getSupplierByStatus() {
  //   this.isLoading = true;
  //   let supplierStatus = this.SupplierForm.get('supplierStatus').value;
  //   this.SupplierService.getSupplierByStatus(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId, supplierStatus).subscribe(response => {
  //     this.dataSource.data = response.items;
  //     this.permission = response.permission;
  //     this.paging.length = response.totalItemsCount
  //     this.isLoading = false;
  //     console.log(this.dataSource.data, "Customer Supplier")
  //     //Group by warehouse 
  //     const groups = this.dataSource.data.reduce((groups, datas) => {
  //       const schoolName = datas.warehouse;
  //       if (!groups[schoolName]) {
  //         groups[schoolName] = [];
  //       }
  //       groups[schoolName].push(datas);
  //       return groups;
  //     }, {});

  //     // Edit: to add it in the array format instead
  //     this.groupArrays = Object.keys(groups).map((warehouse) => {
  //       return {
  //         warehouse,
  //         datas: groups[warehouse],
  //       };
  //     });
  //     console.log(this.groupArrays, "status data")
  //   })
  // }

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
    this.getSuppliers();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getSuppliers();
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
            <h2 style="margin:0;">খুলনা সিটি কর্পোরেশন</h2>
            <h4 style="margin:0;">খুলনা</h4>
            <h3>${this.pageTitle} লিষ্ট </h3>
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
    const id = row.supplierId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SupplierService.delete(id).subscribe(() => {
          this.getSuppliers();
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
