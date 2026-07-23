import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ShopHandCashWithdrow } from '../../models/ShopHandCashWithdrow';
import { ShopHandCashWithdrowService } from '../../service/ShopHandCashWithdrow.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-shophandcashwithdrow-list',
  templateUrl: './shophandcashwithdrow-list.component.html',
  styleUrls: ['./shophandcashwithdrow-list.component.sass']
})
export class ShopHandCashWithdrowListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: ShopHandCashWithdrow[] = [];
  isLoading = false;
  role: any;
  branchId: any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText = "";
  permission:any = {};
  displayedColumns: string[] = ['sl', 'useQty', 'actions'];
  dataSource: MatTableDataSource<ShopHandCashWithdrow> = new MatTableDataSource();

  selection = new SelectionModel<ShopHandCashWithdrow>(true, []);


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private ShopHandCashWithdrowService: ShopHandCashWithdrowService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getShopHandCashWithdrows();
  }

  getShopHandCashWithdrows() {
    this.isLoading = true;
    this.ShopHandCashWithdrowService.getShopHandCashWithdrows(this.paging.pageIndex, this.paging.pageSize, this.searchText)
      .subscribe(response => {
        console.log(response," Withdrow data")
        this.dataSource.data = response.items;
        
        // if (this.role === 'Super Admin') {
        //   // Super Admin সব data দেখবে
        //   this.dataSource.data = response.items;
        // } else {
        //   // অন্য role শুধুমাত্র আজকের data দেখবে
        //   const today = new Date();

        //   this.dataSource.data = response.items.filter(item => {
        //     const transferDate = new Date(item.transferDate);

        //     return (
        //       transferDate.getFullYear() === today.getFullYear() &&
        //       transferDate.getMonth() === today.getMonth() &&
        //       transferDate.getDate() === today.getDate()
        //     );
        //   });
        // }

        this.permission = response.permission;
        this.paging.length = this.dataSource.data.length;
        this.isLoading = false;
      });
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  inAcctiveWithdrow(row){
    const id = row.shopHandCashWithdrowId; 
          this.confirmService.confirm('Confirm  Approve message', 'Are You Sure Approve This Item?').subscribe(result => {
            if (result) {
              console.log(result)
          this.ShopHandCashWithdrowService.inAcctiveWithdrow(id).subscribe(() => {
            this.reloadCurrentRoute();
            this.snackBar.open('Information Approved Successfully ', '', {
              duration: 3000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          })
        }
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
    this.getShopHandCashWithdrows();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getShopHandCashWithdrows();
  }
  deleteItem(row) {
    const id = row.shopHandCashWithdrowId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ShopHandCashWithdrowService.delete(id).subscribe(() => {
          this.getShopHandCashWithdrows();
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
