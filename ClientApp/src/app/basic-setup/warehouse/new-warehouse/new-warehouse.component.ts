import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {WarehouseService} from '../../service/Warehouse.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-warehouse',
  templateUrl: './new-warehouse.component.html',
  styleUrls: ['./new-warehouse.component.sass']
})
export class NewWarehouseComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  WarehouseForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private warehouseService: WarehouseService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('warehouseId'); 
    if (id) {
      this.pageTitle = 'Warehouse Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.warehouseService.find(+id).subscribe(
        res => {
          this.WarehouseForm.patchValue({          

            warehouseId: res.warehouseId,
            warehouseName: res.warehouseName,
            warehouseAddress: res.warehouseAddress,
            location: res.location,
            contactPerson: res.contactPerson,
            contactNo: res.contactNo,
            cashAmount: res.cashAmount,
            bankBalance:res.bankBalance,
            cashInHand:res.cashInHand,
            remarks: res.remarks,
            productImages:res.productImages,
            businessImages: res.businessImages,
            businessUnitName: res.businessUnitName,
            bussinessUnitDescriptions: res.bussinessUnitDescriptions,
            isWebsite: res.isWebsite,
            isEshop: res.isEshop,
            isActive: res.isActive

           
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Warehouse ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.WarehouseForm = this.fb.group({
      warehouseId: [0],
      warehouseName: [''],
      warehouseAddress: [''],
      location: [''],
      contactPerson: [''],
      contactNo: [''],
      cashAmount: [0],
      bankBalance:[0],
      cashInHand:[0],
      remarks: [''],
      productImages:[''],
      image:[''],
      businessImages: [''],
      photo:[''],
      businessUnitName: [],
      bussinessUnitDescriptions: [],
      isWebsite: [false],
      isEshop: [false],
      isActive: [true],
     
    })
  }

  onFileChanged(event: any) {
  if (!event.target.files || event.target.files.length === 0) {
    return;
  }

  const file: File = event.target.files[0];
  this.compressImage(file, 100).then((compressedFile: File) => {
    this.WarehouseForm.patchValue({
      photo: compressedFile
    });

    this.snackBar.open('Image compressed and background set to white', '', {
      duration: 2000,
      verticalPosition: 'bottom',
      horizontalPosition: 'right'
    });
  });
}
onImageFileChanged(event: any) {
  if (!event.target.files || event.target.files.length === 0) {
    return;
  }

  const file: File = event.target.files[0];
  this.compressImage(file, 100).then((compressedFile: File) => {
    this.WarehouseForm.patchValue({
      image: compressedFile
    });

    this.snackBar.open('Image compressed and background set to white', '', {
      duration: 2000,
      verticalPosition: 'bottom',
      horizontalPosition: 'right'
    });
  });
}


compressImage(file: File, maxSizeKB: number): Promise<File> {
  return new Promise((resolve) => {
    const reader = new FileReader();
    const img = new Image();

    reader.readAsDataURL(file);
    reader.onload = (e: any) => {
      img.src = e.target.result;
    };

    img.onload = () => {
      const canvas = document.createElement('canvas');
      const ctx = canvas.getContext('2d');

      const MAX_WIDTH = 800;
      const scale = MAX_WIDTH / img.width;

      canvas.width = MAX_WIDTH;
      canvas.height = img.height * scale;

      // White background fill
      ctx.fillStyle = '#ffffff';
      ctx.fillRect(0, 0, canvas.width, canvas.height);

      // Draw the image on top
      ctx.drawImage(img, 0, 0, canvas.width, canvas.height);

      let quality = 0.9;

      const compressLoop = () => {
        canvas.toBlob(
          (blob) => {
            if (!blob) return;

            if (blob.size / 1024 <= maxSizeKB || quality <= 0.1) {
              const compressedFile = new File([blob], file.name.replace(/\.\w+$/, '.jpg'), {
                type: 'image/jpeg', // সবকিছু JPEG তে convert হবে কারণ white bg দরকার
                lastModified: Date.now()
              });
              resolve(compressedFile);
            } else {
              quality -= 0.1;
              compressLoop();
            }
          },
          'image/jpeg',
          quality
        );
      };

      compressLoop();
    };
  });
}
  
  onSubmit() {
    const id = this.WarehouseForm.get('warehouseId').value; 

    const formData = new FormData();
    for (const key of Object.keys(this.WarehouseForm.value)) {
      let value = this.WarehouseForm.value[key];
      if (value === null || value === undefined) {
        value = ''; // or 0 if number
      }
      formData.append(key, value);
    }  
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.warehouseService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/warehouse-list');
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
      this.warehouseService.submit(formData).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/warehouse-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
