import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FisheriesInventoryOutService } from '../../service/FisheriesInventoryOut.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-fisheriesinventoryout',
  templateUrl: './new-fisheriesinventoryout.component.html',
  styleUrls: ['./new-fisheriesinventoryout.component.sass']
})
export class NewFisheriesInventoryOutComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  FisheriesInventoryOutForm: FormGroup;
  validationErrors: string[] = [];
  productTypeList: SelectedModel[];
  pondList: SelectedModel[];
  warehouseList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId:any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar,private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private FisheriesInventoryOutService: FisheriesInventoryOutService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('fisheriesInventoryOutId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.FisheriesInventoryOutService.find(+id).subscribe(
        res => {
          this.FisheriesInventoryOutForm.patchValue({
            fisheriesInventoryOutId: res.fisheriesInventoryOutId,
            warehouseId:res.warehouseId,
            pondId: res.pondId,
            fisheriesProductTypeId: res.fisheriesProductTypeId,
            fisheriesInventoryDetailId: res.fisheriesInventoryDetailId,
            date: res.date,
            useQty: res.useQty,
            unitPurchasePrice:res.unitPurchasePrice,
            approveStatus: res.approveStatus,
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
    this.getSelectedPondList();
    this.getSelectedProductTypeList();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.FisheriesInventoryOutForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.FisheriesInventoryOutForm = this.fb.group({
      fisheriesInventoryOutId: [0],
      warehouseId:[],
      pondId: [],
      fisheriesProductTypeId: [],
      fisheriesInventoryDetailId: [],
      productName: [""],
      date: [today],
      useQty: [''],
      unitPurchasePrice:[],
      approveStatus: [false],
      isActive: [true],

    });
    //autocomplete for productName
    this.FisheriesInventoryOutForm.get('productName').valueChanges
      .subscribe(value => {
        this.getSelectedProduct(value);
      })
  }

  getWarehouseList() {
    this.FisheriesInventoryOutService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedPondList() {
    this.FisheriesInventoryOutService.getSelectedPondList().subscribe(res => {
      this.pondList = res;
    });
  }
  getSelectedProductTypeList() {
    this.FisheriesInventoryOutService.getSelectedProductTypeList(this.branchId).subscribe(res => {
      this.productTypeList = res;
    });
  }
  //autocomplete for ProductName
  onProductNameSelectionChanged(upitem) {
    this.fisheriesInventoryDetailId = upitem.value
    this.FisheriesInventoryOutForm.get('fisheriesInventoryDetailId').setValue(upitem.value);
    this.FisheriesInventoryOutForm.get('productName').setValue(upitem.text);
  }

  //autocomplete for ProductName
  getSelectedProduct(productName) {
    const warehouseId = this.FisheriesInventoryOutForm.get('warehouseId').value;
    const fisheriesProductTypeId = this.FisheriesInventoryOutForm.get('fisheriesProductTypeId').value;
    this.FisheriesInventoryOutService.getSelectedProduct(productName,warehouseId,fisheriesProductTypeId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }

  onSubmit() {
    const id = this.FisheriesInventoryOutForm.get('fisheriesInventoryOutId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FisheriesInventoryOutService.update(+id, this.FisheriesInventoryOutForm.value).subscribe(response => {
            this.router.navigateByUrl('/stock-consumption/fisheriesinventoryout-list');
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
      this.FisheriesInventoryOutService.submit(this.FisheriesInventoryOutForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/stock-consumption/fisheriesinventoryout-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
