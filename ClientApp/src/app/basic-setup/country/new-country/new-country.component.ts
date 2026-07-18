import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CountryService} from '../../service/Country.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-country',
  templateUrl: './new-country.component.html',
  styleUrls: ['./new-country.component.sass']
})
export class NewCountryComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  CountryForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private CountryService: CountryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('countryId'); 
    if (id) {
      this.pageTitle = 'Country Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.CountryService.find(+id).subscribe(
        res => {
          this.CountryForm.patchValue({          

            countryId: res.countryId,
            name: res.name,
            status: res.status,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Country';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.CountryForm = this.fb.group({
      countryId: [0],
      name: [''],
      status: [0],
      isActive: [true],
     
    })
  }
  
  onSubmit() {
    const id = this.CountryForm.get('countryId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.CountryService.update(+id,this.CountryForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/country-list');
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
      this.CountryService.submit(this.CountryForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/country-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
