using AutoMapper;
using EmployeeAPI.Model;
using EmployeeAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Signup, Employee>().ForMember(s=>s.Home_Address,opt=>opt.MapFrom(s=>s.HomeAddress));
            CreateMap<Login, Employee>();
             
        }
    }
}
