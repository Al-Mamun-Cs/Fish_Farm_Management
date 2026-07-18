import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {PaymentStatusService} from '../../service/PaymentStatus.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-paymentstatus',
  templateUrl: './new-paymentstatus.component.html',
  styleUrls: ['./new-paymentstatus.component.sass']
})
export class NewPaymentStatusComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  paymentstatusForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private paymentstatusService: PaymentStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('paymentStatusId'); 
    if (id) {
      this.pageTitle = 'Payment Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.paymentstatusService.find(+id).subscribe(
        res => {
          this.paymentstatusForm.patchValue({          

            paymentStatusId: res.paymentStatusId,
            statusName: res.statusName,
            priorityNo: res.priorityNo,
            isActive: res.isActive

            // paymentstatusId: res.paymentstatusId,
            // paymentstatusName: res.paymentstatusName,
            //menuPosition: res.menuPosition,
          
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
    this.paymentstatusForm = this.fb.group({
      paymentStatusId: [0],
      statusName: [''],
      priorityNo: [],
      //menuPosition: ['', Validators.required],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.paymentstatusForm.get('paymentStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.paymentstatusService.update(+id,this.paymentstatusForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/paymentstatus-list');
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
      this.paymentstatusService.submit(this.paymentstatusForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/paymentstatus-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
