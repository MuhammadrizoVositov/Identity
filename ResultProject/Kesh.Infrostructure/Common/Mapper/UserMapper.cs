using AutoMapper;
using Kesh.Application.Common.Identity.Models;
using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Mapper;
public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<SignUpDetails, User>();
    }
}
