import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {UpozilaService} from '../../service/Upozila.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-upozila',
  templateUrl: './new-upozila.component.html',
  styleUrls: ['./new-upozila.component.sass']
})
export class NewUpozilaComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  UpozilaForm: FormGroup;
  validationErrors: string[] = [];
  selectedDistrictList:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private UpozilaService: UpozilaService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('upazilaId'); 
    if (id) {
      this.pageTitle = 'Upazila Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.UpozilaService.find(+id).subscribe(
        res => {
          this.UpozilaForm.patchValue({  
            upazilaId: res.upazilaId,        
            districtId: res.districtId,
            upazilaName: res.upazilaName,
            upazilaNameBangla: res.upazilaNameBangla,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Upazila ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    this.getSelectedDistrictList();
  }
  intitializeForm() {
    this.UpozilaForm = this.fb.group({
      upazilaId: [0],
      districtId: [],
      upazilaName: [''],
      upazilaNameBangla: [''],
      //menuPosition: ['', Validators.required],
      isActive: [true],
     
    })
  }
  getSelectedDistrictList(){
    this.UpozilaService.getSelectedDistrictList().subscribe(res=>{
      this.selectedDistrictList=res
      
    });
  }
  onSubmit() {
    const id = this.UpozilaForm.get('upazilaId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.UpozilaService.update(+id,this.UpozilaForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/upozila-list');
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
      this.UpozilaService.submit(this.UpozilaForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/upozila-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
