import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Post } from '../../models/post.model';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-list-posts',
  imports: [CommonModule],
  templateUrl: './list-posts.html',
  styleUrl: './list-posts.css',
})
export class ListPosts {
  posts?: Post[] = [];

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

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
    });
  }
}
