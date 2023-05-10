namespace USER_API.AuxiliaryModels;

public class UserAuxiliary
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
    public UserGroupaAxiliary UserGroup { get; set; }
    public UserStateAuxiliary UserState { get; set; }
}