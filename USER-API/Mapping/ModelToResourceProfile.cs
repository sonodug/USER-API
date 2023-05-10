using AutoMapper;
using USER_API.Models;
using USER_API.AuxiliaryModels;

namespace USER_API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<UserGroup, UserGroupaAxiliary>();
        CreateMap<UserState, UserStateAuxiliary>();
        CreateMap<User, UserAuxiliary>();
    }
}