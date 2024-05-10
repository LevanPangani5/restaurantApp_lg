using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using RazorPagesTestAppTF.Data.DbModels;
using RazorPagesTestAppTF.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;

namespace RazorPagesTestAppTF.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _userEmailStore;
        private readonly IEmailSender _emailSender;
        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserStore<IdentityUser> userStore, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _userEmailStore = GetEmailStore();
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (!roles.Any())
            {
                var adminRole = new IdentityRole("Admin");
                var userRole = new IdentityRole("User");

                await _roleManager.CreateAsync(adminRole);
                await _roleManager.CreateAsync(userRole);
            }

            ReturnUrl = returnUrl;

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new InputModel
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r
                })
            };
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = Activator.CreateInstance<ApplicationUser>();
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _userEmailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (Input.Role is null)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { userId = user.Id, code = code }, protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email,
                    "Confirm your account - Identity Manager",
                        $"Please confirm your account by clicking on the following link: <a href=\"{HtmlEncoder.Default.Encode(callBackUrl)}\">Click here</a>");

                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return Page();
        }

        public class InputModel
        {

            [System.ComponentModel.DataAnnotations.Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
            public string ConfirmPassword { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            public string FirstName { get; set; }

            [System.ComponentModel.DataAnnotations.Required]
            public string LastName { get; set; }

            public string? Role { get; set; }

            public IEnumerable<SelectListItem>? RoleList { get; set; }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotImplementedException("The default UI requires a user store with email support");
            }

            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
