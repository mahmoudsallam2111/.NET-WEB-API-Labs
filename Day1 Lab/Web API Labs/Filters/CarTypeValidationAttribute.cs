using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;
using Web_API_Labs.Models;

namespace Web_API_Labs.Filters
{
    public class CarTypeValidationAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var car = context.ActionArguments["car"] as Car;
            var regex = new Regex("^(Electric|Gas| Disel| Hybird)$" , RegexOptions.IgnoreCase,
                TimeSpan.FromSeconds(2));
            if (car == null || !regex.IsMatch(car.Type))
            {
                context.ModelState.AddModelError("Type","the type of car is not match any one from existing");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
