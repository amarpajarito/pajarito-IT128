import { Routes } from '@angular/router';
import { ListPosts } from './components/list-posts/list-posts';
import { PostDetail } from './components/post-detail/post-detail';
import { LoginPage } from './components/login-page/login-page';
import { RegisterPage } from './components/register-page/register-page';
import { AddPost } from './components/add-post/add-post';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPage },
  { path: 'register', component: RegisterPage },
  { path: 'posts', component: ListPosts },
  { path: 'post/:id', component: PostDetail },
  { path: 'add-post', component: AddPost },
];
