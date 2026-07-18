import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {DivisionService} from '../../service/Division.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-division',
  templateUrl: './new-division.component.html',
  styleUrls: ['./new-division.component.sass']
})
export class NewDivisionComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  DivisionForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private DivisionService: DivisionService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('divisionId'); 
    if (id) {
      this.pageTitle = 'Division Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.DivisionService.find(+id).subscribe(
        res => {
          this.DivisionForm.patchValue({          

            divisionId: res.divisionId,
            divisionName: res.divisionName,
            nameBangla: res.nameBangla,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Division ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.DivisionForm = this.fb.group({
      divisionId: [0],
      divisionName: [''],
      nameBangla: [''],
      //menuPosition: ['', Validators.required],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.DivisionForm.get('divisionId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DivisionService.update(+id,this.DivisionForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/division-list');
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
      this.DivisionService.submit(this.DivisionForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/division-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
