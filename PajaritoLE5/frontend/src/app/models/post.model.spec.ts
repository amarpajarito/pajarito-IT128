import { Post } from './post.model';

describe('Post', () => {
  it('should create an instance', () => {
    const post: Post = {
      id: 1,
      title: 'Test Title',
      body: 'Test Content',
      dateCreated: new Date(),
      userName: 'testuser',
      firstName: 'Test',
      lastName: 'User',
    };
    expect(post).toBeTruthy();
  });
});
