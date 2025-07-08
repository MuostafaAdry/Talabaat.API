using Domain.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceAbs;
using ServiceImm.MappingProfilel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImm.ServiceRegistrations
{
    public static class RegisterServices
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFacturyDeleget>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());

            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<Func<IOrderServices>>(provider =>
            () => provider.GetRequiredService<IOrderServices>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>());

            Services.AddScoped<ICacheService, CacheService>();


            return Services;
        }

        // add service for jwt

        public static IServiceCollection AddJwtServices(this IServiceCollection Services,IConfiguration _configration)
        {
            Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer=true,
                    ValidIssuer= _configration["JWTOptions:Issuer"],

                    ValidateAudience=true,
                    ValidAudience = _configration["JWTOptions:Audience"],

                    ValidateLifetime=true,

                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration.GetSection("JWTOptions")["SecretKey"])),


                };

            });

            return Services;
        }

    }
}
