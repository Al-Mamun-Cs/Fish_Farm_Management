import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { FisheriesProductReturn } from '../../models/FisheriesProductReturn';
import { FisheriesProductReturnService } from '../../service/FisheriesProductReturn.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-fisheriesproductreturn-list',
  templateUrl: './fisheriesproductreturn-list.component.html',
  styleUrls: ['./fisheriesproductreturn-list.component.sass']
})
export class FisheriesProductReturnListComponent implements OnInit {
  photoBaseUrl = environment.fileUrl;
  masterData = MasterData;
  ELEMENT_DATA: FisheriesProductReturn[] = [];
  isLoading = false;
  role: any;
  branchId: any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText = "";
  permission: any;
  

  displayedColumns: string[] = ['sl', 'useQty', 'actions'];
  dataSource: MatTableDataSource<FisheriesProductReturn> = new MatTableDataSource();

  selection = new SelectionModel<FisheriesProductReturn>(true, []);


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private FisheriesProductReturnService: FisheriesProductReturnService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getFisheriesProductReturns();
  }

  getFisheriesProductReturns() {
    this.isLoading = true;
    this.FisheriesProductReturnService.getFisheriesProductReturns(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {

      console.log('API Response:', response);
      console.log('Permission Object:', response.permission);
      this.dataSource.data = response.items;
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      console.log('API Response:', response.permission);
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
    this.getFisheriesProductReturns();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getFisheriesProductReturns();
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  inAcctiveFisheriesProductReturn(row) {
    const id = row.fisheriesProductReturnId;
    this.confirmService.confirm('Confirm  Approve message', 'Are You Sure Approve This Item?').subscribe(result => {
      if (result) {
        console.log(result)
        this.FisheriesProductReturnService.inAcctiveFisheriesProductReturn(id).subscribe(() => {
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

  deleteItem(row) {
    const id = row.fisheriesProductReturnId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.FisheriesProductReturnService.delete(id).subscribe(() => {
          this.getFisheriesProductReturns();
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
