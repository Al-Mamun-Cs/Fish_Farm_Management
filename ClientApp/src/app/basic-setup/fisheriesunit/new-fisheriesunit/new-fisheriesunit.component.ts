import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {FisheriesUnitService} from '../../service/FisheriesUnit.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-fisheriesunit',
  templateUrl: './new-fisheriesunit.component.html',
  styleUrls: ['./new-fisheriesunit.component.sass']
})
export class NewFisheriesUnitComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  FisheriesUnitForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private FisheriesUnitService: FisheriesUnitService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('fisheriesUnitId'); 
    if (id) {
      this.pageTitle = 'Payment Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.FisheriesUnitService.find(+id).subscribe(
        res => {
          this.FisheriesUnitForm.patchValue({          

            fisheriesUnitId: res.fisheriesUnitId,
            fullName: res.fullName,
            shortName: res.shortName,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Payment Type ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.FisheriesUnitForm = this.fb.group({
      fisheriesUnitId: [0],
      fullName: [''],
      shortName: [],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.FisheriesUnitForm.get('fisheriesUnitId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FisheriesUnitService.update(+id,this.FisheriesUnitForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/fisheriesunit-list');
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
      this.FisheriesUnitService.submit(this.FisheriesUnitForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/fisheriesunit-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
