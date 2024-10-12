using Blog_web_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog_web_app.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Tag> Tags { get; set; }


}