import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BrandService } from '../../service/Brand.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-brand',
  templateUrl: './new-brand.component.html',
  styleUrls: ['./new-brand.component.sass']
})
export class NewBrandComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  BrandForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar, private confirmService: ConfirmService, private BrandService: BrandService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('brandId');
    if (id) {
      this.pageTitle = 'Brand Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.BrandService.find(+id).subscribe(
        res => {
          this.BrandForm.patchValue({

            brandId: res.brandId,
            fullName: res.fullName,
            shortName: res.shortName,
            brandImages: res.brandImages,
            eshopImages: res.eshopImages,
            isEshop: res.isEshop,
            isActive: res.isActive

          });
        }
      );
    } else {
      this.pageTitle = 'New Brand ';
      this.destination = 'Add ';
      this.buttonText = "Save";
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.BrandForm = this.fb.group({
      brandId: [0],
      fullName: [''],
      shortName: [''],
      brandImages: [''],
      photo: [''],
      eshopImages: [''],
      eshopImage: [''],
      isEshop: [false],
      isActive: [false],

    })
  }

  onFileChanged(event: any) {
    if (!event.target.files || event.target.files.length === 0) {
      return;
    }

    const file: File = event.target.files[0];
    this.compressImage(file, 100).then((compressedFile: File) => {
      this.BrandForm.patchValue({
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
      this.BrandForm.patchValue({
        eshopImage: compressedFile
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

        ctx.fillStyle = '#ffffff';
        ctx.fillRect(0, 0, canvas.width, canvas.height);


        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);

        let quality = 0.9;

        const compressLoop = () => {
          canvas.toBlob(
            (blob) => {
              if (!blob) return;

              if (blob.size / 1024 <= maxSizeKB || quality <= 0.1) {
                const compressedFile = new File([blob], file.name.replace(/\.\w+$/, '.jpg'), {
                  type: 'image/jpeg',
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
    const id = this.BrandForm.get('brandId').value;

    const formData = new FormData();
    for (const key of Object.keys(this.BrandForm.value)) {
      let value = this.BrandForm.value[key];
      if (value === null || value === undefined) {
        value = ''; // or 0 if number
      }
      formData.append(key, value);
    }  

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.BrandService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/brand-list');
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
      this.BrandService.submit(formData).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/brand-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
