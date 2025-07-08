using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public class BasketNotFound(string id):BaseNotFountException($"Basket with id {id} Not Found !")
    {
    }
}
