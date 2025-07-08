using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class OrderEmailNotFoundException(string email):BaseNotFountException($"No Orders To Email = {email} !")
    {
    }
}
