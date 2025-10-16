using Microsoft.AspNetCore.Builder;

namespace Middleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DI Container
            //IOC Container


            var app = builder.Build();

            //Pipeline And Middleware
            ///Pipeline is a sequence of Middleware that process HTTP requests and responses [RequestDelegate]
            ///Middleware is a software component that is used to handle requests and responses
            ///Take RequestDelegate and process it and return new RequestDelegate

            //First Middleware this the master middleware form 
            //Take RequestDelegate and return RequestDelegate
            //the next parameter is a RequestDelegate
            app.Use((RequestDelegate next )=>next);

            
            //this middleware will write to the response and then call the next middleware
            app.Use((RequestDelegate next) =>
            {
                return (async (HttpContext context) =>
                {
                    //write to the response
                    await context.Response.WriteAsync("Hello from Middleware 1\n");
                    await next(context); //call the next middleware
                });
            });


            //this Extension method version of the above middleware
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello from Middleware 2\n");
                await next(context); //call the next middleware
            });


            //this middlewar will not call the next middleware
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello from Middleware 3\n");
                //await next(context);  dont call the next middleware
            });

            //this will not be called because the previous middleware did not call next
            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await context.Response.WriteAsync("Hello from Middleware 4\n");
                await next(context); 
            });



            app.Run();
        }
    }
}
