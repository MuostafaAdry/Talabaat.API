using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class UserNotFoundException(string Email):BaseNotFountException($"User With Email {Email} is Not Found")
    {
    }
}
