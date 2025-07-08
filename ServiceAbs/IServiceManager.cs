using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbs
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;  }
        public IBasketService BasketService { get;  }
        public IOrderServices OrderServices { get;  }
        public IPaymentService PaymentService { get;  }
        public IAuthenticationService AuthenticationService { get;  }
    }
}
