using Microsoft.AspNetCore.Identity;

namespace API_Identity.Data
{
    public class Student:IdentityUser
    {
        public string ClassLevel  { get; set; } = string.Empty;
    }
}
