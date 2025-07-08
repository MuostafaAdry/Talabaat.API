using Domain.Contracts;
using ServiceAbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm
{
    public class ServiceManagerWithFacturyDeleget(Func<IProductService> ProductFactury,
        Func<IBasketService> BasketFactuey, Func<IOrderServices> OrderFactuey,
        Func<IAuthenticationService> AuthenticationFactuey,Func<IPaymentService> paymentService) : IServiceManager
    {
        public IProductService ProductService => ProductFactury.Invoke();

        public IBasketService BasketService => BasketFactuey.Invoke();

        public IOrderServices OrderServices => OrderFactuey.Invoke();
        public IPaymentService PaymentService => paymentService.Invoke();

        public IAuthenticationService AuthenticationService => AuthenticationFactuey.Invoke();
    }
}
