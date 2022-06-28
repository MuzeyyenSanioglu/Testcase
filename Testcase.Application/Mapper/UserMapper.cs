using AutoMapper;
using Testcase.Application.DTOs;
using Testcase.User.Domain;

namespace Testcase.Application
{
    public class UserMapper: Profile

    {
        public UserMapper()
        {
            CreateMap<Users, UserDto>().ReverseMap();
        }
       
    }
}