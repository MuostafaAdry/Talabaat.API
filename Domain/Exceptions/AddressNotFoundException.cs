using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AddressNotFoundException(string message):BaseNotFountException($"user {message} has No Address")
    {
    }
}
