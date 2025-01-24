using Gestion_Devis_Facture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gestion_Devis_Facture.Pages
{[Authorize(Roles = "admin")]
    public class Update_UserModel : PageModel
    {
        
       
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Update_UserModel(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                Input = new EditUserViewModel();
            }

            [BindProperty]
            public EditUserViewModel Input { get; set; }

            public async Task<IActionResult> OnGetAsync(string id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                var roles = await _userManager.GetRolesAsync(user);
                Input = new EditUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Role = roles.FirstOrDefault() ?? string.Empty
                };

                return Page();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(Input.Id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.FirstName = Input.FirstName;
                    user.LastName = Input.LastName;
                    user.Email = Input.Email;
                    user.UserName = Input.Email;
                    user.PhoneNumber = Input.PhoneNumber;
                    user.Address = Input.Address;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                        if (!string.IsNullOrEmpty(Input.Role))
                        {
                            await _userManager.AddToRoleAsync(user, Input.Role);
                        }

                        return RedirectToPage("./Admin");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return Page();
            }
        }
    }

