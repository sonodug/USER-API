using AutoMapper;
using USER_API.Models;
using USER_API.Resources;

namespace USER_API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<UserGroup, UserGroupResource>();
        CreateMap<UserState, UserStateResource>();
        CreateMap<User, UserResource>();
    }
}