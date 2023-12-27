using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.RequestContexts.Models;
public class RequestContext
{
    public Guid? UserId { get; set; }

    public string IpAddress { get; set; } = default!;

    public string UserAgent { get; set; } = default!;
}
