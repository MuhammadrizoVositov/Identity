using Kesh.Application.RequestContexts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.RequestContexts.Broker;
public interface IRequestContextProvider
{
    RequestContext GetRequestContext();
}
