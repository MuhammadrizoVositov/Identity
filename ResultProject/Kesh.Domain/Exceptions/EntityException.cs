using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Exceptions;
public class EntityException:Exception
{
    public EntityException(Guid entityId, string message)
    {
    }
}
