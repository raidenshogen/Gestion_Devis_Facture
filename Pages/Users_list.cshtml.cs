using Gestion_Devis_Facture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Pages
{  [Authorize(Roles = "admin")]
    public class Users_listModel : PageModel
    {
        
       
            private readonly UserManager<ApplicationUser> _userManager;

            public Users_listModel(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
                Users = new List<UserViewModel>();
            }

            public List<UserViewModel> Users { get; set; }

            public async Task<IActionResult> OnGetAsync()
            {
                var users = await _userManager.Users.ToListAsync();
                Users = new List<UserViewModel>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    Users.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        CreatedAt = user.CreatedAt,
                        EmailConfirmed = user.EmailConfirmed,
                        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        Roles = roles.ToList()
                    });
                }

                return Page();
            }

            public async Task<IActionResult> OnPostDeleteAsync(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return RedirectToPage();
            }
        public class UserViewModel
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public bool EmailConfirmed { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public List<string> Roles { get; set; }=new List<string>(); 
        }
    }
    }

