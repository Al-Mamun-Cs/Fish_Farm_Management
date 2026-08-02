import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DepositorInstallment } from '../../models/DepositorInstallment';
import { DepositorInstallmentService } from '../../service/DepositorInstallment.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-depositorinstallment-list',
  templateUrl: './depositorinstallment-list.component.html',
  styleUrls: ['./depositorinstallment-list.component.sass']
})
export class DepositorInstallmentListComponent implements OnInit {
  photoBaseUrl = environment.fileUrl;
  masterData = MasterData;
  ELEMENT_DATA: DepositorInstallment[] = [];
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
  monthNames = [
    '',
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December'
  ];

  displayedColumns: string[] = ['sl', 'useQty', 'actions'];
  dataSource: MatTableDataSource<DepositorInstallment> = new MatTableDataSource();

  selection = new SelectionModel<DepositorInstallment>(true, []);


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private DepositorInstallmentService: DepositorInstallmentService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getDepositorInstallments();
  }

  getDepositorInstallments() {
    this.isLoading = true;
    this.DepositorInstallmentService.getDepositorInstallments(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.branchId).subscribe(response => {

      console.log('API Response:', response);
      console.log('Permission Object:', response.permission);
      this.dataSource.data = response.items;
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
      console.log('API Response:', response.permission);
    })
  }
  getEmployeeImage(path: string): string {
    if (!path) return 'assets/no-image.png'; // fallback
    return this.photoBaseUrl + path;
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
    this.getDepositorInstallments();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getDepositorInstallments();
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  inAcctiveDepositorInstallment(row) {
    const id = row.depositorInstallmentId;
    this.confirmService.confirm('Confirm  Approve message', 'Are You Sure Approve This Item?').subscribe(result => {
      if (result) {
        console.log(result)
        this.DepositorInstallmentService.inAcctiveDepositorInstallment(id).subscribe(() => {
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
    const id = row.depositorInstallmentId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DepositorInstallmentService.delete(id).subscribe(() => {
          this.getDepositorInstallments();
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
