using AutoMapper;
using USER_API.Models;
using USER_API.Repositories.Interfaces;
using USER_API.AuxiliaryModels;

namespace USER_API.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterUserAuxiliary, User>();
    }
}