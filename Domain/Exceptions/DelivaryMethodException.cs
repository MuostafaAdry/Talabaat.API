using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class DelivaryMethodException(string id):BaseNotFountException($"Delivery Method with id= {id} NOT FOUND")
    {
    }
}
