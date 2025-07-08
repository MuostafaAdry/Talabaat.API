
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using ReviewRouteApi.CustomValidationMethods;
using ReviewRouteApi.Middelwares;
using Scalar.AspNetCore;
using ServiceAbs;
using ServiceImm;
using ServiceImm.MappingProfilel;
using ServiceImm.ServiceRegistrations;
using Shared.ErrorModels;

namespace ReviewRouteApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // extension services to inject service IDataSeeding & IMainUnitOfWork & Connection String
            builder.Services.AddInfrasturctureRegistration(builder.Configuration);
            // extension services to inject service manager & product profile
            builder.Services.AddRegisterServices();
             
            // extension services to build service for jwt options& validations
            builder.Services.AddJwtServices(builder.Configuration);

            // handle validation error =>make a method to handle it in app
            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactury.GenirateApiValidationErrors;
            });

            var app = builder.Build();

            // Apply DataSeeding
            using var Scope = app.Services.CreateScope();
            var ObOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObOfDataSeeding.DataSeeding();
            ObOfDataSeeding.IdentityDataSeed();

            // custom   exception handler middleware
            app.UseMiddleware<CustomExeptionHandelerMiddelware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll"); 

            app.UseAuthentication();   
            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
