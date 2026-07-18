import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {PondService} from '../../service/Pond.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-pond',
  templateUrl: './new-pond.component.html',
  styleUrls: ['./new-pond.component.sass']
})
export class NewPondComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  PondForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private PondService: PondService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('pondId'); 
    if (id) {
      this.pageTitle = 'Product Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.PondService.find(+id).subscribe(
        res => {
          this.PondForm.patchValue({          

            pondId: res.pondId,
            nameEnglish: res.nameEnglish,
            nameBangla: res.nameBangla,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Product Type ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.PondForm = this.fb.group({
      pondId: [0],
      nameEnglish: [''],
      nameBangla: [],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.PondForm.get('pondId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.PondService.update(+id,this.PondForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/pond-list');
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
      this.PondService.submit(this.PondForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/pond-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
