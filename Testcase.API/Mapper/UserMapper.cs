using AutoMapper;
using Testcase.API.DTOs;
using Testcase.User.Domain;

namespace Testcase.API
{
    public class UserMapper: Profile

    {
        public UserMapper()
        {
            CreateMap<Users, UserDto>().ReverseMap();
        }
       
    }
}