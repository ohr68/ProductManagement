namespace Auth.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public User()
    {
        CreatedOn = DateTime.UtcNow;
    }

    public void Updated()
    {
        ModifiedOn = DateTime.UtcNow;
    }
}