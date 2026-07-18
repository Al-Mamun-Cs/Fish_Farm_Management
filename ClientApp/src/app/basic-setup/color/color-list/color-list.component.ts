import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Color} from '../../models/Color';
import { ColorService} from '../../service/Color.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-color-list',
  templateUrl: './color-list.component.html',
  styleUrls: ['./color-list.component.sass']
})
export class ColorListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Color[] = [];
  isLoading = false;
  groupArrays: { category: string; datas: any }[];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','name','shortName', 'actions'];
  dataSource: MatTableDataSource<Color> = new MatTableDataSource();

  selection = new SelectionModel<Color>(true, []);

  
  constructor(private snackBar: MatSnackBar,private ColorService:ColorService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getColors();
  }
  
  getColors() {
    this.isLoading = true;
    this.ColorService.getColors(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
        console.log('API Response:', response.permission); 

        //Group by category 
      const groups = this.dataSource.data.reduce((groups, datas) => {
        const schoolName = datas.category;
        if (!groups[schoolName]) {
          groups[schoolName] = [];
        }
        groups[schoolName].push(datas);
        return groups;
      }, {});

      // Edit: to add it in the array format instead
      this.groupArrays = Object.keys(groups).map((category) => {
        return {
          category,
          datas: groups[category],
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
    this.getColors();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getColors();
  } 
  deleteItem(row) {
    const id = row.colorId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.ColorService.delete(id).subscribe(() => {
          this.getColors();
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
