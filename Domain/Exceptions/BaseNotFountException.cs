using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public abstract class BaseNotFountException(string message):Exception(message)
    {
    }
}
