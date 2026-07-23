import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierService } from '../../service/Supplier.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-supplier',
  templateUrl: './new-supplier.component.html',
  styleUrls: ['./new-supplier.component.sass']
})
export class NewSupplierComponent implements OnInit {

  buttonText: string;
  pageTitle: string;
  destination: string;
  SupplierForm: FormGroup;
  selectedWarehousList: SelectedModel[];
  selectedDivisionList: SelectedModel[];
  districtList: SelectedModel[];
  upozilaList: SelectedModel[];
  selectedCountryList: SelectedModel[];
  selectedCustomerTypeList: SelectedModel[];
  selectedSupplierTypeList: SelectedModel[];
  validationErrors: string[] = [];
  isExist: boolean;
  options = [];
  filteredOptions;
  filteredUpazilaOptions;
  districtId: number;
  upazilaId: number;
  role: any;
  branchId: any;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private confirmService: ConfirmService, private SupplierService: SupplierService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.intitializeForm();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    const id = this.route.snapshot.paramMap.get('supplierId');
    if (id) {
      this.pageTitle = 'Customer Update ';
      this.destination = 'Update';
      this.buttonText = "Update";
      this.SupplierService.find(+id).subscribe(
        res => {
          this.SupplierForm.patchValue({

            supplierId: res.supplierId,
            customerTypeId: res.customerTypeId,
            supplierTypeId: res.supplierTypeId,
            divisionId: res.divisionId,
            districtId: res.districtId,
            upazilaId: res.upazilaId,
            // categoryId: res.categoryId,
            productTypeId: res.productTypeId,
            warehouseId: res.warehouseId,
            supplierName: res.supplierName,
            shopName: res.shopName,
            address: res.address,
            tin: res.tin,
            tradeLicenseNo: res.tradeLicenseNo,
            phoneNo: res.phoneNo,
            email: res.email,
            countryId: res.countryId,
            contactPerson: res.contactPerson,
            supplierStatus: res.supplierStatus,
            commissionPercent: res.commissionPercent,
            commissionPaidDate: res.commissionPaidDate,
            creditLimitAmount: res.creditLimitAmount,
            monthClosingDate: res.monthClosingDate,
            remarks: res.remarks,
            totalAdvanceAmount: res.totalAdvanceAmount,
            totalDueAmount: res.totalDueAmount,
            totalPaidAmount: res.totalPaidAmount,
            districtName: res.districName,
            upazilaName: res.upazilaName,
            isActive: res.isActive
          });
        }
      );
    } else {
      this.pageTitle = 'New Customer';
      this.destination = 'Add ';
      this.buttonText = "Save";
    }

    this.getSelectedWarehousesList();
    if (this.branchId > 0) {
      this.SupplierForm.get('warehouseId').setValue(this.branchId);
    }
    this.getSelectedDivisionList();
    // this.getSelectedCustomerTypeList();
    this.getSelectedSupplierTypeList();
    this.getSelectedCountryList();
  }
  intitializeForm() {
    this.SupplierForm = this.fb.group({
      supplierId: [0],
      customerTypeId: [''],
      //supplierTypeId: [''],
      divisionId: [''],
      districtId: [''],
      districtName: [""],
      upazilaId: [''],
      upazilaName: [""],
      //categoryId: [''],
      //productTypeId: [''],
      warehouseId: [''],
      supplierName: [''],
      shopName: [''],
      address: [''],
      tin: [''],
      tradeLicenseNo: [''],
      phoneNo: [''],
      email: [''],
      contactPerson: [''],
      countryId: [1],
      supplierStatus: [],
      commissionPercent: [0],
      commissionPaidDate: [''],
      monthClosingDate: [""],
      creditLimitAmount: [0],
      clientsImage: [''],
      photo: [''],
      remarks: [''],
      totalAdvanceAmount: [0],
      totalDueAmount: [0],
      totalPaidAmount: [0],
      isActive: [false],

    })
    //autocomplete for districtName
    this.SupplierForm.get('districtName').valueChanges
      .subscribe(value => {
        this.getSelectedDistrictName(value);
      })

    //autocomplete for upazilaName
    this.SupplierForm.get('upazilaName').valueChanges
      .subscribe(value => {
        this.getSelectedUpazilaName(value);
      })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('dddd')
      console.log(file);
      this.SupplierForm.patchValue({
        photo: file,
      });
    }
  }

  // listenToSupplierStatus() {
  //   this.SupplierForm.get('supplierStatus').valueChanges.subscribe(status => {
  //     const division = this.SupplierForm.get('divisionId');
  //     const district = this.SupplierForm.get('districtId');
  //     const upazila = this.SupplierForm.get('upazilaId');

  //     if (status !== 2) {
  //       division.setValidators([Validators.required]);
  //       district.setValidators([Validators.required]);
  //       upazila.setValidators([Validators.required]);
  //     } else {
  //       division.clearValidators();
  //       district.clearValidators();
  //       upazila.clearValidators();
  //     }

  //     division.updateValueAndValidity();
  //     district.updateValueAndValidity();
  //     upazila.updateValueAndValidity();
  //   });
  // }

  //autocomplete for DistrictName
  onDistrictNameSelectionChanged(item) {
    this.districtId = item.value
    this.SupplierForm.get('districtId').setValue(item.value);
    this.SupplierForm.get('districtName').setValue(item.text);
  }

  //autocomplete for DistrictName
  getSelectedDistrictName(districtName) {
    this.SupplierService.getSelectedDistrictName(districtName).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }

  //autocomplete for upazilaName
  onUpazilaNameSelectionChanged(upitem) {
    this.upazilaId = upitem.value
    this.SupplierForm.get('upazilaId').setValue(upitem.value);
    this.SupplierForm.get('upazilaName').setValue(upitem.text);
  }

  //autocomplete for upazilaName
  getSelectedUpazilaName(upazilaName) {
    this.SupplierService.getSelectedUpazilaName(upazilaName).subscribe(response => {
      this.options = response;
      this.filteredUpazilaOptions = response;
    })
  }

  getSelectedWarehousesList() {
    this.SupplierService.getSelectedWarehousesList().subscribe(res => {
      this.selectedWarehousList = res
    });
  }
  // getSelectedCustomerTypeList(){
  //   this.SupplierService.getSelectedCustomerTypeList().subscribe(res=>{
  //     this.selectedCustomerTypeList=res
  //   });
  // }
  getSelectedSupplierTypeList() {
    this.SupplierService.getSelectedSupplierTypeList().subscribe(res => {
      this.selectedSupplierTypeList = res
    });
  }
  getSelectedDivisionList() {
    this.SupplierService.getSelectedDivisionList().subscribe(res => {
      this.selectedDivisionList = res
      this.getSelectedDistrictList();
    });
  }
  getSelectedDistrictList() {
    let divisionId = this.SupplierForm.get('divisionId').value;
    this.SupplierService.getSelectedDistrictList(divisionId).subscribe(res => {
      this.districtList = res;
      this.getSelectedUpozilaList()
    });
  }
  getSelectedUpozilaList() {
    let districtId = this.SupplierForm.get('districtId').value;
    this.SupplierService.getSelectedUpozilaList(districtId).subscribe(res => {
      this.upozilaList = res;
    });
  }
  getSelectedCountryList() {
    this.SupplierService.getSelectedCountryList().subscribe(res => {
      this.selectedCountryList = res
    });
  }
  onPhoneNoChange(value) {
    console.log("Phone no");
    console.log(value);
    this.SupplierService.getPhoneNOIsExistCheck(value).subscribe(response => {
      this.isExist = response;
      console.log("phone no");
      console.log(this.isExist);
    })
  }
  formatDate(date: Date): string {
    date = new Date(date);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  onSubmit() {
    const id = this.SupplierForm.get('supplierId').value;
    // const supplierStatus = this.SupplierForm.get('supplierStatus').value;
    // if (supplierStatus === 1) {
    //   this.SupplierForm.get('monthClosingDate').setValue(this.formatDate(this.SupplierForm.get('monthClosingDate').value));
    // }
    const formData = new FormData();

    for (const key of Object.keys(this.SupplierForm.value)) {
      let value = this.SupplierForm.value[key];

      // handle null/undefined values safely
      if (value === null || value === undefined) {
        value = '';
      }

      // handle File input separately (photo)
      if (key === 'photo' && value instanceof File) {
        formData.append(key, value, value.name);
      } else {
        formData.append(key, value);
      }
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.SupplierService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/supplier-list');
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
      this.SupplierService.submit(formData).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/supplier-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
