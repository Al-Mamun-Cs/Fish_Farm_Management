import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CompanyInvestor } from '../../models/CompanyInvestor';
import { CompanyInvestorService } from '../../service/CompanyInvestor.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-companyinvestor-list',
  templateUrl: './companyinvestor-list.component.html',
  styleUrls: ['./companyinvestor-list.component.sass']
})
export class CompanyInvestorListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: CompanyInvestor[] = [];
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
  dataSource: MatTableDataSource<CompanyInvestor> = new MatTableDataSource();

  selection = new SelectionModel<CompanyInvestor>(true, []);


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private CompanyInvestorService: CompanyInvestorService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getCompanyInvestors();
  }

  getCompanyInvestors() {
    this.isLoading = true;
    this.CompanyInvestorService.getCompanyInvestors(this.paging.pageIndex, this.paging.pageSize, this.searchText,this.branchId)
      .subscribe(response => {
        console.log(response," cost data")
        this.dataSource.data = response.items;

        // if (this.role === 'Super Admin') {
        //   // Super Admin সব data দেখবে
        //   this.dataSource.data = response.items;
        // } else {
        //   // অন্য role শুধুমাত্র আজকের data দেখবে
        //   const today = new Date();

        //   this.dataSource.data = response.items.filter(item => {
        //     const transactionDate = new Date(item.transactionDate);

        //     return (
        //       transactionDate.getFullYear() === today.getFullYear() &&
        //       transactionDate.getMonth() === today.getMonth() &&
        //       transactionDate.getDate() === today.getDate()
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
    this.getCompanyInvestors();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getCompanyInvestors();
  }
  deleteItem(row) {
    const id = row.companyInvestorId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.CompanyInvestorService.delete(id).subscribe(() => {
          this.getCompanyInvestors();
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
