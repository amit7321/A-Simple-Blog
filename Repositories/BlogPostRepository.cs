using Blog_web_app.Data;
using Blog_web_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_web_app.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext blogDbContext;

    public BlogPostRepository(BlogDbContext blogDbContext)
    {
        this.blogDbContext = blogDbContext;
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await blogDbContext.BlogPosts.AddAsync(blogPost);
        await blogDbContext.SaveChangesAsync();

        return blogPost;
    }

    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var existingBlog = await blogDbContext.BlogPosts.FindAsync(id);

        if (existingBlog != null)
        {
            blogDbContext.BlogPosts.Remove(existingBlog);
            await blogDbContext.SaveChangesAsync();

            return existingBlog;
        }

        return null;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await blogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
    }

    public Task<BlogPost?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        throw new NotImplementedException();
    }
}