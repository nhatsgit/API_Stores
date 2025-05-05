namespace API_Stores.Midlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Console.WriteLine($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");

            await _next(httpContext);

            Console.WriteLine($"Response: {httpContext.Response.StatusCode}");
        }
    }

}
