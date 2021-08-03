namespace SIS.Second.Features.Users.Profiles
{
    using AutoMapper;

    using SIS.Domain.Model.Entity;
    using SIS.Second.Features.Users.Models;

    public class UserListProfile : Profile
    {
        public UserListProfile()
        {
            CreateMap<User, UserListDto>();
        }
    }
}