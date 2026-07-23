import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopHandCashWithdrowService } from '../../service/ShopHandCashWithdrow.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { WarehouseService } from '../../../basic-setup/service/Warehouse.service';

@Component({
  selector: 'app-new-shophandcashwithdrow',
  templateUrl: './new-shophandcashwithdrow.component.html',
  styleUrls: ['./new-shophandcashwithdrow.component.sass']
})
export class NewShopHandCashWithdrowComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  ShopHandCashWithdrowForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  warehouseData: any;
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private WarehouseService: WarehouseService, private confirmService: ConfirmService, private ShopHandCashWithdrowService: ShopHandCashWithdrowService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('shopHandCashWithdrowId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.ShopHandCashWithdrowService.find(+id).subscribe(
        res => {
          this.ShopHandCashWithdrowForm.patchValue({
            shopHandCashWithdrowId: res.shopHandCashWithdrowId,
            warehouseId: res.warehouseId,
            presentAmount: res.presentAmount,
            transferAmount: res.transferAmount,
            remainingAmount: res.remainingAmount,
            transferDate: res.transferDate,
            transferReason: res.transferReason,
            type:res.type,
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
      this.ShopHandCashWithdrowForm.get('warehouseId').setValue(this.branchId);
      this.getWarehouseData()
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.ShopHandCashWithdrowForm = this.fb.group({
      shopHandCashWithdrowId: [0],
      warehouseId: [],
      presentAmount: [],
      transferAmount: [],
      remainingAmount: [],
      transferDate: [today],
      transferReason: [],
      type:[1], // Type 1 for CashWithdrew
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });

    this.ShopHandCashWithdrowForm.get('transferAmount')?.valueChanges.subscribe(value => {
      const presentAmount = Number(this.ShopHandCashWithdrowForm.get('presentAmount')?.value) || 0;
      const transferAmount = Number(value) || 0;
      if (transferAmount > presentAmount) {

        this.snackBar.open(
          `হাতে নগদ ${presentAmount.toFixed(2)} টাকার বেশি ট্রান্সফার করা যাবে না।`,
          '',
          {
            duration: 4000,
            verticalPosition: 'bottom',
            horizontalPosition: 'left',
            panelClass: 'snackbar-danger'
          }
        );

        this.ShopHandCashWithdrowForm.patchValue({
          transferAmount: presentAmount,
          remainingAmount: 0
        }, { emitEvent: false });

        return;
      }
      this.ShopHandCashWithdrowForm.patchValue(
        {
          remainingAmount: presentAmount - transferAmount
        },
        { emitEvent: false }
      );

    });

  }

  getWarehouseList() {
    this.ShopHandCashWithdrowService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getWarehouseData() {
    this.WarehouseService.find(this.branchId).subscribe(res => {
      console.log(res, "warehouseData")
      this.warehouseData = res;
      this.ShopHandCashWithdrowForm.patchValue({
        presentAmount: res.cashInHand
      });
    });
  }




  onSubmit() {
    const id = this.ShopHandCashWithdrowForm.get('shopHandCashWithdrowId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ShopHandCashWithdrowService.update(+id, this.ShopHandCashWithdrowForm.value).subscribe(response => {
            this.router.navigateByUrl('/financial-transactions/shophandcashwithdrow-list');
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
      this.ShopHandCashWithdrowService.submit(this.ShopHandCashWithdrowForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/financial-transactions/shophandcashwithdrow-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
