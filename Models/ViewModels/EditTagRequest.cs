namespace Blog_web_app.Models.ViewModels;

public class EditTagRequest 
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String DisplayName { get; set; }
}