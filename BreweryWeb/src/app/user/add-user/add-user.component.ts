import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
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
          this.router.navigate(['/user/user-list']);
          this.toastr.success('User Added')
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
