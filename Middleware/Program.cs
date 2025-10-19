using Microsoft.AspNetCore.Builder;
using Middleware.Classes;

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

            #region Pipeline && Middleware 
            ////Pipeline And Middleware
            /////Pipeline is a sequence of Middleware that process HTTP requests and responses [RequestDelegate]
            /////Middleware is a software component that is used to handle requests and responses
            /////Take RequestDelegate and process it and return new RequestDelegate

            ////First Middleware this the master middleware form 
            ////Take RequestDelegate and return RequestDelegate
            ////the next parameter is a RequestDelegate
            //app.Use((RequestDelegate next )=>next);


            ////this middleware will write to the response and then call the next middleware
            //app.Use((RequestDelegate next) =>
            //{
            //    return (async (HttpContext context) =>
            //    {
            //        //write to the response
            //        await context.Response.WriteAsync("Hello from Middleware 1\n");
            //        await next(context); //call the next middleware
            //    });
            //});


            ////this Extension method version of the above middleware
            //app.Use(async (HttpContext context, RequestDelegate next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Middleware 2\n");
            //    await next(context); //call the next middleware
            //});


            ////this middlewar will not call the next middleware
            //app.Use(async (HttpContext context, RequestDelegate next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Middleware 3\n");
            //    //await next(context);  dont call the next middleware
            //});

            ////this will not be called because the previous middleware did not call next
            //app.Use(async (HttpContext context, RequestDelegate next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Middleware 4\n");
            //    await next(context); 
            //}); 
            #endregion



            #region Write Middleware Using app.Run

            //Adds a terminal middleware delegate to the application's request pipeline.
            //this Middleware will be the last one in the pipeline
            //This dont Call the next middleware
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("final middleware ");

            //});

            ////this Middleware will be not called because the final middleware is added at the end of the pipeline[app.Run()]
            //app.Use(async (HttpContext context, RequestDelegate next) =>
            //{
            //    await context.Response.WriteAsync("Hello from Middleware 4\n");
            //    await next(context);
            //});

            #endregion


            #region Never Call next() After Response


            //Case 01: Never Call next() After Response Has Started
            //app.Use(async (context, next) =>
            //{
            //    //is the response has not started yet you can set the status code and headers
            //    //context.Response.StatusCode=StatusCodes.Status200OK;
            //    //context.Response.Headers.Append("h1", "test");
            //    await context.Response.WriteAsync("the response has strated \n"); //responce has started
            //    //but if the response has started you cant set the status code and headers
            //    //context.Response.StatusCode = StatusCodes.Status200OK;
            //    //Expection will be thrown [StatusCode cannot be set because the response has already started]
            //    await next();

            //});

            //Case 02
            //app.Use(async (context, next) =>
            //{
            //    //the response has started here
            //    await context.Response.WriteAsync("the response has strated \n"); 
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    //this will throw an exception because the response has already started [above]
            //    //StatusCode cannot be set because the response has already started.
            //    context.Response.StatusCode = StatusCodes.Status200OK;
            //    await next();

            //});

            //Case 03: Solution
            //app.Use(async (context, next) =>
            //{
            //    //the response has started here
            //    await context.Response.WriteAsync("the response has strated \n");
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    //Solution: check if the response has started
            //    if (!context.Response.HasStarted)
            //    {
            //        context.Response.StatusCode = StatusCodes.Status200OK;
            //    }
            //    await next();
            //});
            #endregion


            #region Before & After Middleware

            //Before & After Middleware
            //When I call next() the control is passed to the next middleware in the pipeline
            //If there is code after next() it will be executed after the next middleware has completed

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("MW 01 Before\n"); //will be executed first
            //    await next(); //call the next middleware
            //    await context.Response.WriteAsync("MW 01 After\n"); //will be executed last
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("         MW 02 Before\n"); //will be executed second
            //    await next(); //call the next middleware
            //    await context.Response.WriteAsync("         MW 02 After\n");//will be executed fifth
            //});


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("             MW 03 Before\n"); //will be executed third
            //    await next(); //call the next middleware
            //    await context.Response.WriteAsync("             MW 03 After\n"); //will be executed fourth
            //});

            ///**
            // *  MW 01 Before
            //            MW 02 Before
            //                MW 03 Before
            //                MW 03 After
            //            MW 02 After
            //    MW 01 After
            // */

            #endregion

            #region  Middleware OrderingMyRegion

            ////Middleware Ordering : Order of middleware is important

            ////Built-in Middleware
            //app.UseExceptionHandler(); //For handling exceptions
            //app.UseHsts(); //For security for HTTPS
            //app.UseHttpsRedirection(); //Redirect HTTP to HTTPS
            //app.UseStaticFiles(); //For serving static files
            //app.UseRouting(); //For routing to choose endpoint
            //app.UseCors(); //For Cross-Origin Resource Sharing
            //app.UseAuthentication(); //For Authentication
            //app.UseAuthorization(); //For Authorization


            ////Custom Middleware
            //app.Use(async (context, next) => {await next();});

            ////Endpoint Middleware
            //app.MapGet("/", () => "Hlloe");

            #endregion


            #region Middleware Branching


            //app.Map("/branch1", Branch1.GetBranch1);


            //app.Map("/branch2", Branch2.GetBranch2);

            #endregion

            #region Middleware Branching


            #region app.UseWhen()
            //app.UseWhen() => Create Branching Middleware Based on Condition 
            //if the request path is /branch1 then the branch middleware will be executed
            //and it will write "You are in Branch 1" to the response
            //and then it will call the next middleware in the main pipeline => terminal middleware [important]

            //app.UseWhen(
            //    context => context.Request.Path.Equals("/branch1", StringComparison.OrdinalIgnoreCase),
            //    b =>
            //    {
            //        b.Use(async (context, next) =>

            //        {
            //            await context.Response.WriteAsync("You are in Branch 1");
            //            await next(context); //call the next middleware in the main pipeline
            //        });
            //    });

            //app.Run(context => context.Response.WriteAsync("tirminal Middleware"));

            #endregion

            #region app.MapWhen()
            //app.MapWhen() => Create Branching Middleware Based on Condition
            //if the request path is /branch1 then the branch middleware will be executed
            //and it will write "You are in Branch 1" to the response
            //MapWhen Dont call the next Middleware 

            //app.MapWhen(
            //    context => context.Request.Path.Equals("/branch1", StringComparison.OrdinalIgnoreCase),
            //    b =>
            //    {
            //        b.Run(async (context) =>

            //        {
            //            await context.Response.WriteAsync("You are in Branch 1");
            //            //await next(context); //dont call the next middleware in the main pipeline
            //            //its like tirminal middleware
            //        });
            //    });

            //app.Run(context => context.Response.WriteAsync("tirminal Middleware"));

            #endregion

            #endregion

            app.Run();
        }
    }
}
