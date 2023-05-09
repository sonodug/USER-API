using USER_API.Models;

namespace USER_API.Services.Communication;

public class RegisterUserResponse : BaseResponse
{
    public User User { get; private set; }
    
    public RegisterUserResponse(bool succes, string message, User user) : base(succes, message)
    {
        User = user;
    }
    
    public RegisterUserResponse(User user) : this(true, string.Empty, user)
    { }
    
    public RegisterUserResponse(string message) : this(false, message, null)
    { }
}