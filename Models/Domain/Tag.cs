namespace Blog_web_app.Models.Domain;

public class Tag
{
    public Guid Name { get; set; }
    public string name { get; set; }
    public string DisplayName { get; set; }

    public ICollection<BlogPost> BlogPosts { get; set; }

}