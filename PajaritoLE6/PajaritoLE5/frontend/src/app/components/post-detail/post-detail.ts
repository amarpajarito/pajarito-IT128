import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Post } from '../../models/post.model';
import { timeout, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-post-detail',
  imports: [CommonModule],
  templateUrl: './post-detail.html',
  styleUrls: ['./post-detail.css'],
})
export class PostDetail implements OnInit {
  post?: Post;
  error: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private location: Location,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    console.log('Post ID from route:', id);

    if (id) {
      this.http
        .get<Post>(`https://localhost:7045/api/post/${id}`)
        .pipe(
          timeout(10000),
          catchError((err) => {
            console.error('HTTP Error:', err);
            this.error = true;
            this.cdr.detectChanges();
            return of(null);
          })
        )
        .subscribe({
          next: (data) => {
            console.log('Post data received:', data);
            if (data) {
              this.post = data;
              this.cdr.detectChanges();
            } else {
              this.error = true;
              this.cdr.detectChanges();
            }
          },
          error: (err) => {
            console.error('Subscribe error:', err);
            this.error = true;
            this.cdr.detectChanges();
          },
        });
    } else {
      console.error('No post ID found in route');
      this.error = true;
      this.cdr.detectChanges();
    }
  }

  goBack(): void {
    this.location.back();
  }
}
