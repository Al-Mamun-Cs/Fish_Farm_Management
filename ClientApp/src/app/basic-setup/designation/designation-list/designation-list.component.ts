import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Designation} from '../../models/Designation';
import { DesignationService} from '../../service/Designation.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-designation-list',
  templateUrl: './designation-list.component.html',
  styleUrls: ['./designation-list.component.sass']
})
export class DesignationListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Designation[] = [];
  groupArrays: { warehouse: string; datas: any }[];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','warehouse','name', 'serviceAge', 'actions'];
  dataSource: MatTableDataSource<Designation> = new MatTableDataSource();

  selection = new SelectionModel<Designation>(true, []);

  
  constructor(private snackBar: MatSnackBar,private DesignationService:DesignationService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getDesignations();
  }
  
  getDesignations() {
    this.isLoading = true;
    this.DesignationService.getDesignations(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
        console.log('API Response:', response.permission); 
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
      console.log(this.groupArrays)
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
    this.getDesignations();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDesignations();
  } 
  deleteItem(row) {
    const id = row.designationId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.DesignationService.delete(id).subscribe(() => {
          this.getDesignations();
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
