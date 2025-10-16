import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenStorage } from '../../services/token-storage';

@Component({
  selector: 'app-add-post',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-post.html',
  styleUrls: ['./add-post.css'],
})
export class AddPost implements OnInit {
  form: any = {
    title: null,
    body: null,
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private location: Location,
    private tokenStorage: TokenStorage,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    if (!this.tokenStorage.getToken()) {
      this.router.navigate(['/login']);
    }
  }

  onSubmit(): void {
    const { title, body } = this.form;

    const postData = {
      title,
      body,
    };

    const token = this.tokenStorage.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });

    console.log('Submitting post:', postData);

    this.http
      .post('https://localhost:7045/api/post/add', postData, {
        headers,
        responseType: 'text',
      })
      .subscribe({
        next: (response) => {
          console.log('Post created successfully:', response);
          alert('Post published successfully!');
          this.router.navigate(['/posts']);
        },
        error: (err) => {
          console.error('Failed to create post:', err);
          if (err.status === 401) {
            alert('You are not authorized. Please log in again.');
            this.router.navigate(['/login']);
          } else {
            alert('Failed to publish post. Please try again.');
          }
        },
      });
  }

  goBack(): void {
    this.location.back();
  }
}
