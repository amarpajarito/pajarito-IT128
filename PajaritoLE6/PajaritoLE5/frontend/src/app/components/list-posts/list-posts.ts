import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { Post } from '../../models/post.model';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import { TokenStorage } from '../../services/token-storage';

@Component({
  selector: 'app-list-posts',
  imports: [CommonModule, RouterModule],
  templateUrl: './list-posts.html',
  styleUrls: ['./list-posts.css'],
})
export class ListPosts implements OnInit {
  posts: Post[] = [];

  constructor(
    private http: HttpClient,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private tokenStorage: TokenStorage
  ) {}

  ngOnInit(): void {
    this.initData();
  }

  initData(): void {
    this.http.get<Post[]>('https://localhost:7045/api/post').subscribe({
      next: (data: Post[]) => {
        this.posts = data;
        this.cdr.detectChanges();
        console.log(this.posts);
      },
      error: (err) => {
        console.error('Failed to load posts:', err);
      },
    });
  }

  addPost(): void {
    this.router.navigate(['/add-post']);
  }

  logout(): void {
    this.tokenStorage.signout();
    this.router.navigate(['/login']);
  }
}
