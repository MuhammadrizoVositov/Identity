using AutoMapper;
using Kesh.Application.RequestContexts.Broker;
using Kesh.Application.RequestContexts.Models;
using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Mapper;
public class UserSignInDetailsMapper:Profile
{
    public UserSignInDetailsMapper()
    {
        IRequestContextProvider a;

        CreateMap<RequestContext, UserSignInDetails>();
    }
}
