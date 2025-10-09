import { Component, OnInit } from '@angular/core';
import { Auth } from '../../services/auth';
import { HttpClient } from '@angular/common/http';
import { TokenStorage } from '../../services/token-storage';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage implements OnInit {
  form: any = {
    username: null,
    password: null,
  };

  constructor(
    private authService: Auth,
    private tokenStorage: TokenStorage,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.authService.isLoggedIn = true;
      this.router.navigate([this.authService.redirectUrl]);
    }
  }

  onSubmit() {
    const { username, password } = this.form;

    this.http
      .post<LoginPostData>('https://localhost:7045/api/Login/login', { username, password })
      .subscribe({
        next: (data) => {
          this.tokenStorage.saveToken(data.id_token);
          this.tokenStorage.saveUser(data.id);
          this.router.navigate(['/posts']);
        },
        error: (err) => {
          console.error('Login failed:', err);
          alert('Login failed. Please check your credentials.');
        },
      });
  }
}

export interface LoginPostData {
  id_token: string;
  id: number;
}
