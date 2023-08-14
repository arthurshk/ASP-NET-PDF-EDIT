namespace AspNetLesson01.Models;

public class AboutMe
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public List<Skill> Skills { get; set; }
}
