import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {PaymentStatus} from '../../models/PaymentStatus';
import {PaymentStatusService} from '../../service/PaymentStatus.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-paymentstatus-list',
  templateUrl: './paymentstatus-list.component.html',
  styleUrls: ['./paymentstatus-list.component.sass']
})
export class PaymentStatusListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: PaymentStatus[] = [];
  isLoading = false;
  window = window;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','statusName', 'actions'];
  dataSource: MatTableDataSource<PaymentStatus> = new MatTableDataSource();

  selection = new SelectionModel<PaymentStatus>(true, []);

  
  constructor(private snackBar: MatSnackBar,private paymentstatusService:PaymentStatusService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getpaymentstatuss();
  }
  
  getpaymentstatuss() {
    this.isLoading = true;
    this.paymentstatusService.getPaymentStatuss(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
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
    this.getpaymentstatuss();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getpaymentstatuss();
  } 
  deleteItem(row) {
    const id = row.paymentStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.paymentstatusService.delete(id).subscribe(() => {
          this.getpaymentstatuss();
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
