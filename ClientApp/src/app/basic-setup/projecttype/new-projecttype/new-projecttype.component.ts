import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {ProjectTypeService} from '../../service/ProjectType.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-projecttype',
  templateUrl: './new-projecttype.component.html',
  styleUrls: ['./new-projecttype.component.sass']
})
export class NewProjectTypeComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  ProjectTypeForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProjectTypeService: ProjectTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('projectTypeId'); 
    if (id) {
      this.pageTitle = 'Project Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.ProjectTypeService.find(+id).subscribe(
        res => {
          this.ProjectTypeForm.patchValue({          
            projectTypeId: res.projectTypeId,
            nameEnglish: res.nameEnglish,
            nameBangla: res.nameBangla,
            status:res.status,
            manuPosition:res.manuPosition,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Project Type ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ProjectTypeForm = this.fb.group({
      projectTypeId: [0],
      nameEnglish: [''],
      nameBangla: [],
      status:[0],
      manuPosition:[0],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.ProjectTypeForm.get('projectTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ProjectTypeService.update(+id,this.ProjectTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/projecttype-list');
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
      this.ProjectTypeService.submit(this.ProjectTypeForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/projecttype-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
