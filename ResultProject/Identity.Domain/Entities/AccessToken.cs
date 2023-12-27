using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities;
public class AccessToken:IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Value { get; set; } = default!;

    public bool IsRevoked { get; set; }

    public DateTime CreatedTime { get; set; }
}
