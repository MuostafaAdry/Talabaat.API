using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductNotFoundEx(int id): BaseNotFountException($"Product with id {id} Not Found")
    {
    }
}
