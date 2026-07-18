import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {DistrictService} from '../../service/District.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-district',
  templateUrl: './new-district.component.html',
  styleUrls: ['./new-district.component.sass']
})
export class NewDistrictComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  DistrictForm: FormGroup;
  validationErrors: string[] = [];
  selectedDivisionList:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private DistrictService: DistrictService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('districtId'); 
    if (id) {
      this.pageTitle = 'District Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.DistrictService.find(+id).subscribe(
        res => {
          this.DistrictForm.patchValue({          
            districtId: res.districtId,
            divisionId: res.divisionId,
            districtName: res.districtName,
            districtNameBangla: res.districtNameBangla,
            shippingCharge:res.shippingCharge,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New District ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    this.getSelectedDivisionList();
  }
  intitializeForm() {
    this.DistrictForm = this.fb.group({
      districtId: [0],
      divisionId: [],
      districtName: [''],
      districtNameBangla: [''],
      shippingCharge: [0],
      isActive: [true],
     
    })
  }
  getSelectedDivisionList(){
    this.DistrictService.getSelectedDivisionList().subscribe(res=>{
      this.selectedDivisionList=res
      
    });
  }
  onSubmit() {
    const id = this.DistrictForm.get('districtId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DistrictService.update(+id,this.DistrictForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/district-list');
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
      this.DistrictService.submit(this.DistrictForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/district-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
