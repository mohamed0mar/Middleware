

using Microsoft.Extensions.Logging;

namespace Middleware.Classes
{
    public class HandleMap
    {
        public static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("HandleMap Test 1");
            });
        }


        public static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("HandleMap Test 2");
            });
        }

        internal static void HandelMapWhen(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("HandleMap When Test");
            });
        }

        internal static void HandelUseWhen(IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
            app.Use(async (context, next) =>
            {
                var branchVer = context.Request.Query["branch"];
                logger.LogInformation("Branch used = {branchVer}", branchVer);

                await next();
            });
        }
    }
}
