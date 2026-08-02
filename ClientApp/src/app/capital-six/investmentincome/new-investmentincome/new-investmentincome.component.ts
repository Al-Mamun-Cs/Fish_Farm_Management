import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvestmentIncomeService } from '../../service/InvestmentIncome.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepositorInvestmentService } from '../../service/DepositorInvestment.service'


@Component({
  selector: 'app-new-investmentincome',
  templateUrl: './new-investmentincome.component.html',
  styleUrls: ['./new-investmentincome.component.sass']
})
export class NewInvestmentIncomeComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  InvestmentIncomeForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  businessOperatorList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;
  investmenAmount: number = 0;
  principalReturn: number = 0;
  dueReturnAmount: number = 0
  amountError = '';



  constructor(private snackBar: MatSnackBar, private authService: AuthService, private DepositorInvestmentService: DepositorInvestmentService, private datePipe: DatePipe, private confirmService: ConfirmService, private InvestmentIncomeService: InvestmentIncomeService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('investmentIncomeId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.InvestmentIncomeService.find(+id).subscribe(
        res => {
          this.InvestmentIncomeForm.patchValue({
            investmentIncomeId: res.investmentIncomeId,
            warehouseId: res.warehouseId,
            depositorInvestmentId: res.depositorInvestmentId,
            type: res.type,
            date: res.date,
            amount: res.amount,
            approveStatus: res.approveStatus,
            approveBy: res.approveBy,
            approveDate: res.approveDate,
            isActive: res.isActive

          });
        }
      );
    } else {
      this.pageTitle = 'New Stock Out ';
      this.destination = 'Add ';
      this.buttonText = "Save";
    }
    this.intitializeForm();

    this.getWarehouseList();
    if (this.branchId > 0) {
      this.InvestmentIncomeForm.get('warehouseId').setValue(this.branchId);
      this.getSelectedDepositorInvestmentList();
    };
    this.InvestmentIncomeForm.get('amount')?.valueChanges.subscribe(() => {
    this.validateAmount();
  });

  this.InvestmentIncomeForm.get('type')?.valueChanges.subscribe(() => {
    this.validateAmount();
  });
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.InvestmentIncomeForm = this.fb.group({
      investmentIncomeId: [0],
      warehouseId: [],
      depositorInvestmentId: [],
      type: [],
      date: [today],
      amount: [],
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });
  }

  getWarehouseList() {
    this.InvestmentIncomeService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedDepositorInvestmentList() {
    const warehouseId = this.InvestmentIncomeForm.get('warehouseId').value;
    this.InvestmentIncomeService.getSelectedDepositorInvestmentList(warehouseId).subscribe(res => {
      this.businessOperatorList = res;
      this.getInvestmentData();
    });
  }

  getInvestmentData() {
    const depositorInvestmentId = this.InvestmentIncomeForm.get('depositorInvestmentId')?.value;

    this.DepositorInvestmentService.find(depositorInvestmentId).subscribe(res => {
      console.log(res, "Investor Data");

      this.investmenAmount = Number(res.investmenAmount) || 0;
      this.principalReturn = Number(res.principalReturn) || 0;

      this.dueReturnAmount = this.investmenAmount - this.principalReturn;

      console.log("Investment Amount:", this.investmenAmount);
      console.log("Principal Return:", this.principalReturn);
      console.log("Due Return Amount:", this.dueReturnAmount);

      this.validateAmount();
    });
  }
  validateAmount() {

    this.amountError = '';

    const amountControl = this.InvestmentIncomeForm.get('amount');

    const type = Number(this.InvestmentIncomeForm.get('type')?.value);
    const amount = Number(amountControl?.value);

    if (type === 1 && amount > this.dueReturnAmount) {

      this.amountError =
        `বাকি মূলধন ${this.dueReturnAmount.toFixed(2)} টাকা। এর বেশি ফেরত দেওয়া যাবে না।`;

      amountControl.setErrors({ exceedAmount: true });

    } else {

      amountControl.setErrors(null);
    }

    amountControl.markAsTouched();
  }






  onSubmit() {
    const id = this.InvestmentIncomeForm.get('investmentIncomeId').value;

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.InvestmentIncomeService.update(+id, this.InvestmentIncomeForm.value).subscribe(response => {
            this.router.navigateByUrl('/capital-six/investmentincome-list');
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
      this.InvestmentIncomeService.submit(this.InvestmentIncomeForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/capital-six/investmentincome-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
