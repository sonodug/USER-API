namespace USER_API.Resources;

public class UserResource
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
    public UserGroupResource UserGroup { get; set; }
    public UserStateResource UserState { get; set; }
}