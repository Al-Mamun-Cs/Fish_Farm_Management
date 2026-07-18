import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent
  extends UnsubscribeOnDestroyAdapter
  implements OnInit
{
  authForm: FormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;
  lastPublishDate:any;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,private snackBar: MatSnackBar
  ) {
    super();
  }

  ngOnInit() {
    this.lastPublishDate = '04/27/2026';
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
    // const role = this.authService.currentUserValue.role.trim();
    //   if (role === null) {
                  // this.router.navigate(['/member-management/add-memberregistration']);
                // }
  }
  get f() {
    return this.authForm.controls;
  }
  // adminSet() {
  //   this.authForm.get('username').setValue('admin@school.org');
  //   this.authForm.get('password').setValue('admin@123');
  // }
  // teacherSet() {
  //   this.authForm.get('username').setValue('teacher@school.org');
  //   this.authForm.get('password').setValue('teacher@123');
  // }
  // studentSet() {
  //   this.authForm.get('username').setValue('student@school.org');
  //   this.authForm.get('password').setValue('student@123');
  // }
  onBtnClick(){
    console.log("eeeee");
    this.router.navigateByUrl('/member-management/add-memberregistration');
  }
  onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.error = '';
    if (this.authForm.invalid) {

      this.snackBar.open('Email and Password not valid !', '', {
        duration: 2000,
        verticalPosition: 'bottom',
        horizontalPosition: 'right',
        panelClass: 'snackbar-danger'
      });
     
      return;
    } else {
      this.subs.sink = this.authService
        .login(this.f.email.value, this.f.password.value)
        .subscribe(
          (res) => {
            if (res) {
              this.snackBar.open('login successfull.', '', {
                duration: 3000,
                verticalPosition: 'bottom',
                horizontalPosition: 'right',
                panelClass: 'snackbar-success'
              });
             // setTimeout(() => {
              const role = this.authService.currentUserValue.role.trim();


                //const role = this.authService.currentUserValue.role;
                if (role === Role.SuperAdmin || role === Role.Admin || role === Role.GodownManager || role === Role.Dealer || 
                  role === Role.Sr || role === Role.Production || role === Role.Payroll || role === Role.Salary || role === Role.HoD 
                  || role === Role.Accounts || role === Role.CFO || role === Role.HR || role === Role.Employee|| role === Role.EshopMembership || role === Role.FisheriesManager) {
                  this.router.navigate(['/admin/dashboard/main']);
                } 
                // else if (role === Role.MasterAdmin) {
                //   this.router.navigate(['/admin/dashboard/main']);
                // }
              

               else {
                  this.router.navigate(['/authentication/signin']);
                }
                this.loading = false;
            //  }, 1000);
            } else {
              this.error = 'Invalid Login';
            }
          },
          (error) => {
            this.error = error;
            this.submitted = false;
            this.loading = false;
          }
        );
    }
  }
}
