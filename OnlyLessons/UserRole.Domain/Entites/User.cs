using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Common.Enums;

namespace UserRole.Domain.Entites;
public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
   public string EmailAdress { get; set; }
    public Role Role { get; set; } 
}
