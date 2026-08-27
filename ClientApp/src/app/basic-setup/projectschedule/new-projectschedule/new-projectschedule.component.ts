import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectScheduleService} from '../../service/ProjectSchedule.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-new-projectschedule',
  templateUrl: './new-projectschedule.component.html',
  styleUrls: ['./new-projectschedule.component.sass']
})
export class NewProjectScheduleComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  ProjectScheduleForm: FormGroup;
  validationErrors: string[] = [];
  warehouseData:SelectedModel[];
  pondData:SelectedModel[];
  role: any;
  branchId: any;

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private datePipe: DatePipe,private confirmService: ConfirmService,private ProjectScheduleService: ProjectScheduleService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)

    const id = this.route.snapshot.paramMap.get('projectScheduleId'); 
    if (id) {
      this.pageTitle = 'ProjectSchedule Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.ProjectScheduleService.find(+id).subscribe(
        res => {
          this.ProjectScheduleForm.patchValue({          

            projectScheduleId: res.projectScheduleId,
            warehouseId:res.warehouseId,
            pondId:res.pondId,
            dateFrom: res.dateFrom,
            dateTo: res.dateTo,
            activeStatus: res.activeStatus,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New ProjectSchedule';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    this.getSelectedWarehousesList();
    this.getSelectedPondList();
    if (this.branchId > 0) {
      this.ProjectScheduleForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    this.ProjectScheduleForm = this.fb.group({
      projectScheduleId: [0],
      warehouseId:[],
      pondId:[],
      dateFrom: [''],
      dateTo: [],
      activeStatus: [0],
      isActive: [true],
     
    })
  }

  getSelectedWarehousesList(){
    this.ProjectScheduleService.getSelectedWarehousesList().subscribe(res=>{
      this.warehouseData=res
      
    });
  }
  getSelectedPondList(){
    this.ProjectScheduleService.getSelectedPondList().subscribe(res=>{
      this.pondData=res
      
    });
  }
  
  onSubmit() {
    const id = this.ProjectScheduleForm.get('projectScheduleId').value;  
    
    const formValue = { ...this.ProjectScheduleForm.value };
    if (formValue.dateFrom) {formValue.dateFrom = this.datePipe.transform( formValue.dateFrom,'yyyy-MM-dd');}
    if (formValue.dateTo) {formValue.dateTo = this.datePipe.transform( formValue.dateTo,'yyyy-MM-dd');}

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ProjectScheduleService.update(+id,formValue).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/projectschedule-list');
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
      this.ProjectScheduleService.submit(formValue).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/projectschedule-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
