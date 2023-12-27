using Kesh.Domain.Common.Entities;
using Kesh.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Entities;
public class Role:AuditableEntity
{
    public RoleType Type { get; set; }

    public bool IsActive { get; set; }
}

