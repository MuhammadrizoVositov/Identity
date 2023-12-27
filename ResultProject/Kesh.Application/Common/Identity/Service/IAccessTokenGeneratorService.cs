﻿using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Identity.Service;
public interface IAccessTokenGeneratorService
{
    AccessToken GetToken(User user);
}
