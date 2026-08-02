import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepositorInstallmentService } from '../../service/DepositorInstallment.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';


@Component({
  selector: 'app-new-depositorinstallment',
  templateUrl: './new-depositorinstallment.component.html',
  styleUrls: ['./new-depositorinstallment.component.sass']
})
export class NewDepositorInstallmentComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  DepositorInstallmentForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  depositorList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;
  lastInstallmentMonth: string = '';
  lastInstallmentYear: number = null;
  expectedMonth: number;
  expectedYear: number;

  monthError = '';
  yearError = '';
  selectedPhoto: File = null;

  months = [
    { value: 1, name: 'January' },
    { value: 2, name: 'February' },
    { value: 3, name: 'March' },
    { value: 4, name: 'April' },
    { value: 5, name: 'May' },
    { value: 6, name: 'June' },
    { value: 7, name: 'July' },
    { value: 8, name: 'August' },
    { value: 9, name: 'September' },
    { value: 10, name: 'October' },
    { value: 11, name: 'November' },
    { value: 12, name: 'December' }
  ];

  years: number[] = [];


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private DepositorInstallmentService: DepositorInstallmentService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const currentYear = new Date().getFullYear();
    for (let i = currentYear - 10; i <= currentYear + 10; i++) {
      this.years.push(i);
    }
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('depositorInstallmentId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.DepositorInstallmentService.find(+id).subscribe(
        res => {
          this.DepositorInstallmentForm.patchValue({
            depositorInstallmentId: res.depositorInstallmentId,
            warehouseId: res.warehouseId,
            depositorId: res.depositorId,
            installmentDate: res.installmentDate,
            installmentAmount: res.installmentAmount,
            month: res.month,
            year: res.year,
            image: res.image,
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
      this.DepositorInstallmentForm.get('warehouseId').setValue(this.branchId);
      this.getSelectedDepositorList();
    }
  }
  intitializeForm() {
    const today = new Date();
    this.DepositorInstallmentForm = this.fb.group({
      depositorInstallmentId: [0],
      warehouseId: [],
      depositorId: [],
      installmentDate: [this.datePipe.transform(today, 'dd-MMM-yyyy')],
      installmentAmount: [],
      month: [],
      year: [today.getFullYear()],
      image: [''],
      Photo : [''],
      approveStatus: [0],
      approveBy: [],
      //approveDate: [],
      isActive: [true],

    });
  }

  onFileChanged(event: any) {

  console.log('Event:', event);

  const file = event?.target?.files?.[0];

  if (file) {
    this.selectedPhoto = file;
    console.log('Selected File:', this.selectedPhoto);
  }

}


  getWarehouseList() {
    this.DepositorInstallmentService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedDepositorList() {
    const warehouseId = this.DepositorInstallmentForm.get('warehouseId').value;
    this.DepositorInstallmentService.getSelectedDepositorList(warehouseId).subscribe(res => {
      this.depositorList = res;
    });
  }

  SpGetLastInstallmentMonthAndYear() {
    const depositorId = this.DepositorInstallmentForm.get('depositorId').value;

    if (!depositorId || depositorId == 0) {
      this.lastInstallmentMonth = null;
      this.lastInstallmentYear = null;
      this.expectedMonth = null;
      this.expectedYear = null;
      return;
    }

    this.DepositorInstallmentService
      .SpGetLastInstallmentMonthAndYear(depositorId).subscribe((res: any) => {
        if (res && res.length > 0) {
          this.lastInstallmentMonth = res[0].month;
          this.lastInstallmentYear = Number(res[0].year);
          const monthNo = this.months.find(x => x.name === res[0].month)?.value;
          if (!monthNo) {
            return;
          }
          if (monthNo === 12) {
            this.expectedMonth = 1;
            this.expectedYear = this.lastInstallmentYear + 1;
          } else {
            this.expectedMonth = monthNo + 1;
            this.expectedYear = this.lastInstallmentYear;
          }

        } else {
          this.lastInstallmentMonth = null;
          this.lastInstallmentYear = null;
          this.expectedMonth = null;
          this.expectedYear = null;
        }
      });
  }

  validateInstallmentMonthYear() {
    this.monthError = '';
    this.yearError = '';
    const month = Number(this.DepositorInstallmentForm.get('month')?.value);
    const year = Number(this.DepositorInstallmentForm.get('year')?.value);
    if (this.expectedMonth && month !== this.expectedMonth) {
      this.monthError = `শুধুমাত্র ${this.months[this.expectedMonth - 1].name} মাস নির্বাচন করুন।`;
      this.DepositorInstallmentForm.get('month')
        ?.setErrors({ invalidMonth: true });

    } else {
      this.DepositorInstallmentForm.get('month')
        ?.setErrors(null);
    }
    if (this.expectedYear && year !== this.expectedYear) {
      this.yearError = `শুধুমাত্র ${this.expectedYear} বছর নির্বাচন করুন।`;
      this.DepositorInstallmentForm.get('year')
        ?.setErrors({ invalidYear: true });
    } else {
      this.DepositorInstallmentForm.get('year')
        ?.setErrors(null);
    }
  }



  onSubmit() {
    const id = this.DepositorInstallmentForm.get('depositorInstallmentId').value;
    this.DepositorInstallmentForm.get('installmentDate').setValue((new Date(this.DepositorInstallmentForm.get('installmentDate').value)).toUTCString());

    const formData = new FormData();

    Object.keys(this.DepositorInstallmentForm.controls).forEach(key => {

      if (key !== 'photo') {
        const value = this.DepositorInstallmentForm.get(key)?.value;

        if (value != null) {
          formData.append(key, value);
        }
      }

    });

    // Photo আলাদা করে append করুন
    if (this.selectedPhoto) {
      formData.append('Photo', this.selectedPhoto, this.selectedPhoto.name);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DepositorInstallmentService.update(+id, this.DepositorInstallmentForm.value).subscribe(response => {
            this.router.navigateByUrl('/capital-six/depositorinstallment-list');
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
      console.log(this.selectedPhoto,"phato");
      this.DepositorInstallmentService.submit(formData).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/capital-six/depositorinstallment-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
