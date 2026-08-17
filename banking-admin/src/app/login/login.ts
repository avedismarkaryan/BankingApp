import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../services/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  email = '';
  password = '';
  errorMessage = '';

  constructor(private authService: Auth, private router: Router) {}

  onSubmit(): void {
    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: () => {
        this.router.navigate(['/customers']);
      },
      error: () => {
        this.errorMessage = 'Giriş başarısız. Email veya şifre hatalı.';
      },
    });
  }
}