using Kesh.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Entities;
public class UserSignInDetails:AuditableEntity
{
    public string IpAddress { get; set; } = default!;

    public string UserAgent { get; set; } = default!;

    public Guid UserId { get; set; }
}
