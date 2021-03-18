import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../core/services/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User;

  constructor(public fb: FormBuilder,
              private toastr: ToastrService,
              private userService: UserService,
              public router: Router,
              ) { }

  ngOnInit(): void {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword: ['', Validators.required]
      },
      { validator : this.comparePasswords }
      )
    });
  }

  subscribe() {
    if (this.registerForm.value){
      this.user = {
        email: this.registerForm.get('email').value,
        password: this.registerForm.get('passwords.password').value,
        admin: false
      }

      this.userService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('User subscribed')
        },
        error => {
          const messages = error.error
          messages.forEach(element => {
            console.log(element);
            switch (element.Code){
              case 'DuplicateUserName':
                this.toastr.error('This email has already been used');
                break;
            }
          });

        }
      );
    }
  }

  comparePasswords(fb: FormGroup) {
    console.log()
    const confirmPasswordCtrl = fb.get('confirmPassword');
    if (confirmPasswordCtrl.errors == null || 'mismatch' in confirmPasswordCtrl){
      if (fb.get('password').value !== confirmPasswordCtrl.value){
        confirmPasswordCtrl.setErrors({ mismatch: true});
      }else {
        confirmPasswordCtrl.setErrors(null);
      }
    }
  }

}
