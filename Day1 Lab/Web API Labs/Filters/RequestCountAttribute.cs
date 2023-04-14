using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_API_Labs.Filters
{
    public class RequestCountAttribute:ActionFilterAttribute
    {
        private static int counter = 0;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
           Interlocked.Increment(ref counter);  
        }
        public static int getRequestCount()
        {
            return counter;
        }
    }
}
