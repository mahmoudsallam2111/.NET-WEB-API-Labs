using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Web_API_Labs.Validations
{
    public class DatevalidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date < DateTime.Now;  
        }

    }
}
