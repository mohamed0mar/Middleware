namespace Middleware.Classes
{
    public class Branch1
    {
        public static void GetBranch1(IApplicationBuilder app)
        {
            CommonBranch.GetCommonBranch(app);
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Branch 3 - Middleware 1\n");
                await next();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Branch 4 - Middleware 1\n");
            });
        }
    }

    public class Branch2
    {
        public static void GetBranch2(IApplicationBuilder app)
        {
            CommonBranch.GetCommonBranch(app);
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Branch 5 - Middleware 1\n");
                await next();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Branch 6 - Middleware 1\n");
            });
        }
    }

    public abstract class CommonBranch
    {
        public static void GetCommonBranch(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Branch 1 - Middleware 1\n");
                await next();
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Branch 2 - Middleware 1\n");
                await next();
            });
        }
    }
}
