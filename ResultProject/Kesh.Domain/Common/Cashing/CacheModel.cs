using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Common.Cashing;
public abstract class CacheModel
{
    public abstract string CacheKey { get; }
}
