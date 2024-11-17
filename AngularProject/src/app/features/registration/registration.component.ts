import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  registerForm: FormGroup;
  errorMsg = '';

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const formData = this.registerForm.value;
      this.authService.register(formData).subscribe(result => {
        this.router.navigate(['/login']);
      },err=>{
        this.errorMsg = err.error;
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
