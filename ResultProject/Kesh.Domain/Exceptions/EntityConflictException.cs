using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Exceptions;
public class EntityConflictException : EntityException
{
    public EntityConflictException(Guid entityId, string message) : base(entityId, message)
    {
    }
}
