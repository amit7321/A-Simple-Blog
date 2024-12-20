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

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        return await blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var existingBlog = await blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (existingBlog != null)
        {
            existingBlog.Id = blogPost.Id;
            existingBlog.Heading = blogPost.Heading;
            existingBlog.PageTitle = blogPost.PageTitle;
            existingBlog.Content = blogPost.Content;
            existingBlog.ShortDescription = blogPost.ShortDescription;
            existingBlog.FeatureImageUrl = blogPost.FeatureImageUrl;
            existingBlog.UrlHandle = blogPost.UrlHandle;
            existingBlog.PublishedDate = blogPost.PublishedDate;
            existingBlog.Author = blogPost.Author;
            existingBlog.Visible = blogPost.Visible;
            existingBlog.Tags = blogPost.Tags;

            await blogDbContext.SaveChangesAsync();

            return existingBlog;
        }

        return null;
    }
}