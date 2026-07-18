import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormArray  } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReligionService } from '../../service/Religion.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { Religion } from '../../models/Religion';
import { WarehouseService } from '../../../basic-setup/service/Warehouse.service';
import { MatTableDataSource } from '@angular/material/table';
import { MasterData } from 'src/assets/data/master-data';

@Component({
  selector: 'app-new-religion',
  templateUrl: './new-religion.component.html',
  styleUrls: ['./new-religion.component.sass']
})
export class NewReligionComponent implements OnInit {
  masterData = MasterData;
  buttonText:string;
  pageTitle: string;
  destination:string;
  ReligionForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList:SelectedModel[];
  selectEmployeeRelation:SelectedModel[];
  religionList:SelectedModel[];
  countryList:SelectedModel[];
  isLoading = false;
  ELEMENT_DATA: Religion[] = [];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  role:any;
  branchId:any;
  empolyeeId:any;
  groupArrays: { employee: string; datas: any }[];
  unitPaymentType: boolean = false;
  cashAmount:any;
  bankBalance:any;

  dataSource: MatTableDataSource<Religion> = new MatTableDataSource();

  constructor(private snackBar: MatSnackBar, private datePipe: DatePipe,private ReligionService: ReligionService,private WarehouseService: WarehouseService,private authService: AuthService,private confirmService: ConfirmService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    this.empolyeeId = this.route.snapshot.paramMap.get('empolyeeId');
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('religionId'); 
    
    if (id) {
      this.pageTitle = 'Religion Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.ReligionService.find(+id).subscribe(
        res => {
          this.ReligionForm.patchValue({  
            religionId:res.religionId,        
            fullName: res.fullName,
            shortName:res.shortName,
            isActive: res.isActive          
          });   
          
        }
      );
    } else {
      this.pageTitle = 'New Religion';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.ReligionForm = this.fb.group({
      religionId: [0],
      fullName: [''],
      shortName:[''],
      isActive: [true],
    });
  }

  
  
reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }

  
  onSubmit() {
    const id = this.ReligionForm.get('religionId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ReligionService.update(+id,this.ReligionForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/religion-list');
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    }
    else {
      this.ReligionService.submit(this.ReligionForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/religion-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
