namespace USER_API.Services.Communication;

public abstract class BaseResponse
{
    public bool Succes { get; protected set; }
    public string Message { get; protected set; }

    protected BaseResponse(bool succes, string message)
    {
        Succes = succes;
        Message = message;
    }
}