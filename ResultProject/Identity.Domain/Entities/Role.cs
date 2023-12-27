using Identity.Domain.Common;
using Identity.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities;
public class Role:IEntity
{
    public Guid Id { get; set; }

    public RoleType Type { get; set; }

    public bool IsDisabled { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime ModifiedTime { get; set; }
}
