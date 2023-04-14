namespace web_api_labs.middlewares
{
    public class requestcountmiddelware :IMiddleware
    {

        private int requestcount = 0;


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            requestcount++;
            await next(context);
        }


        public int getcountofrequest()
        {
            return requestcount;
        }

       
    }
}
