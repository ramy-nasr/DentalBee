import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
                                     
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMsg = '';

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      this.authService.login(formData).subscribe(result => {
        const token = result;
        this.authService.saveToken(token);
        this.router.navigate(['/notes']);
      },err=>{
        this.errorMsg = 'Invalid login credentials';
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
