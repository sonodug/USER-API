using USER_API.Models;

namespace USER_API.Services.Communication;

public class ControlUserResponse : BaseResponse
{
    public User User { get; private set; }
    
    public ControlUserResponse(bool succes, string message, User user) : base(succes, message)
    {
        User = user;
    }
    
    public ControlUserResponse(User user) : this(true, string.Empty, user)
    { }
    
    public ControlUserResponse(string message) : this(false, message, null)
    { }
}