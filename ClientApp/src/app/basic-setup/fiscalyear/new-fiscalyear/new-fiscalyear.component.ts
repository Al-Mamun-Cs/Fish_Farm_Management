import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FiscalyearService} from '../../service/Fiscalyear.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-fiscalyear',
  templateUrl: './new-fiscalyear.component.html',
  styleUrls: ['./new-fiscalyear.component.sass']
})
export class NewFiscalyearComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  FiscalyearForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private FiscalyearService: FiscalyearService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('fiscalyearId'); 
    if (id) {
      this.pageTitle = 'Fiscal year Update ';
      this.destination='';
      this.buttonText="Update";
      this.FiscalyearService.find(+id).subscribe(
        res => {
          this.FiscalyearForm.patchValue({          

            fiscalyearId: res.fiscalyearId,
            name: res.name,
            startDate: res.startDate,
            endDate: res.endDate,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Fiscal year ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.FiscalyearForm = this.fb.group({
      fiscalyearId: [0],
      name: [''],
      startDate: [''],
      endDate: [''],
      isActive: [true],
     
    })
  }
  
  
  onSubmit() {
    const id = this.FiscalyearForm.get('fiscalyearId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FiscalyearService.update(+id,this.FiscalyearForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/fiscalyear-list');
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
      this.FiscalyearService.submit(this.FiscalyearForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/fiscalyear-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
