using BlogApi.Data;
using BlogApi.Interfaces;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreatePost(Post post)
        {
            _context.Posts.Add(post);
            return Save();

        }

        public bool DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            return Save();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Post> GetPosts()
        {
            return _context.Posts.ToList();
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(post => post.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            return Save();
        }
    }
}
