import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BankService} from '../../service/Bank.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-bank',
  templateUrl: './new-bank.component.html',
  styleUrls: ['./new-bank.component.sass']
})
export class NewBankComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  BankForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private BankService: BankService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('bankId'); 
    if (id) {
      this.pageTitle = 'Bank Update ';
      this.destination='';
      this.buttonText="Update";
      this.BankService.find(+id).subscribe(
        res => {
          this.BankForm.patchValue({          

            bankId: res.bankId,
            bankName: res.bankName,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Bank ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.BankForm = this.fb.group({
      bankId: [0],
      bankName: [''],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.BankForm.get('bankId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.BankService.update(+id,this.BankForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/bank-list');
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
      this.BankService.submit(this.BankForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/bank-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
