using Blog_web_app.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_web_app.Models.ViewModels;

public class EditBlogPostRequest()
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    public string PageTitle { get; set; }
    public string Content { get; set; }
    public string ShortDescription { get; set; }
    public string FeatureImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; }
    public bool Visible { get; set; }

    public IEnumerable<SelectListItem> Tags { get; set; }

    //Collect tags
    public string[] SelectedTags { get; set; } = Array.Empty<string>();
}