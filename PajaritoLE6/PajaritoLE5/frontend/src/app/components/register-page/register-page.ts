import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { first } from 'rxjs';

@Component({
  selector: 'app-register-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css',
})
export class RegisterPage implements OnInit {
  form: any = {
    username: null,
    password: null,
    firstName: null,
    lastName: null,
  };

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {}

  onSubmit() {
    const { username, password, firstName, lastName } = this.form;

    console.log(this.form);

    this.http
      .post('https://localhost:7045/api/Login/register', this.form, { responseType: 'text' })
      .subscribe((data) => {
        this.router.navigate(['/login']);
      });
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
}
