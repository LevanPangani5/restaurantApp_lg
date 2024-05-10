using Microsoft.AspNetCore.Identity;

namespace RazorPagesTestAppTF.Data.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
