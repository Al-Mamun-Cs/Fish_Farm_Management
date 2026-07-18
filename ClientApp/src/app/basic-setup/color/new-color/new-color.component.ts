import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ColorService} from '../../service/Color.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-color',
  templateUrl: './new-color.component.html',
  styleUrls: ['./new-color.component.sass']
})
export class NewColorComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  ColorForm: FormGroup;
  validationErrors: string[] = [];
  categoryData:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ColorService: ColorService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('colorId'); 
    if (id) {
      this.pageTitle = 'Color Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.ColorService.find(+id).subscribe(
        res => {
          this.ColorForm.patchValue({          

            colorId: res.colorId,
            categoryId:res.categoryId,
            name: res.name,
            shortName: res.shortName,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Color';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    this.getSelectedFinishGoods();
  }
  intitializeForm() {
    this.ColorForm = this.fb.group({
      colorId: [0],
      categoryId:[],
      name: [''],
      shortName: [],
      isActive: [true],
     
    })
  }

  getSelectedFinishGoods(){
    this.ColorService.getSelectedFinishGoods().subscribe(res=>{
      this.categoryData=res
      
    });
  }
  
  onSubmit() {
    const id = this.ColorForm.get('colorId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ColorService.update(+id,this.ColorForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/color-list');
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
      this.ColorService.submit(this.ColorForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/color-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
