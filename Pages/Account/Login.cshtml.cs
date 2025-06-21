using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AgenTurismo.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError(string.Empty, "Usuário e senha são obrigatórios");
                return Page();
            }

            if (Username == "admin" && Password == "senha123")
            {
                var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "admin"),
            new(ClaimTypes.Role, "Administrator")
        };

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = "/Pacotes/Index"
                };

                var identity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(
                    "Cookies",
                    new ClaimsPrincipal(identity),
                    authProperties);

                return LocalRedirect(authProperties.RedirectUri);
            }

            ModelState.AddModelError(string.Empty, "Login inválido");
            return Page();
        }
    }
}