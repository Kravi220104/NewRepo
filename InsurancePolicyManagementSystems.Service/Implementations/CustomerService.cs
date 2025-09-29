using InsurancePolicyManagementSystems.Repository.Models;
using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InsurancePolicyManagementSystems.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerService(SignInManager<Customer> signInManager, UserManager<Customer> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> LoginAsync(LoginDTO dto)
        {

            var customer = await _userManager.FindByEmailAsync(dto.Email);

            // Check if customer exists and is active
            if (customer == null || !customer.IsActive)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, false);
            return result.Succeeded;
        }

        public async Task<string> GetUserRoleAsync(string email)
        {
            var customer = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(customer);
            return roles.FirstOrDefault();
        }

        public async Task<RegisterResultDTO> RegisterAsync(RegisterDTO dto)
        {
            var customer = new Customer
            {
                Fullname = dto.Fullname,
                UserName = dto.Email,
                NormalizedUserName = dto.Email.ToUpper(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                Phone = dto.Phone,
                Address = dto.Address
            };

            var result = await _userManager.CreateAsync(customer, dto.Password);
            var response = new RegisterResultDTO { Success = result.Succeeded };

            if (!result.Succeeded)
            {
                response.Errors.AddRange(result.Errors.Select(e => e.Description));
                return response;
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(customer, "User");
            await _signInManager.SignInAsync(customer, false);

            return response;
        }

        public async Task<CustomerListDTO> GetCustomerByEmailAsync(string email)
        {
            var customer = await _userManager.FindByEmailAsync(email);
            if (customer == null) return null;

            return new CustomerListDTO
            {
                Fullname = customer.Fullname,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var customer = await _userManager.FindByNameAsync(dto.Email);
            if (customer == null) return false;

            var removeResult = await _userManager.RemovePasswordAsync(customer);
            if (!removeResult.Succeeded) return false;

            var addResult = await _userManager.AddPasswordAsync(customer, dto.NewPassword);
            return addResult.Succeeded;
        }

        public async Task<CustomerListDTO> GetCurrentCustomerAsync(ClaimsPrincipal user)
        {
            var customer = await _userManager.GetUserAsync(user);
            if (customer == null) return null;

            return new CustomerListDTO
            {
                Fullname = customer.Fullname,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileDTO dto, ClaimsPrincipal user)
        {
            var customer = await _userManager.GetUserAsync(user);
            if (customer == null) return false;

            bool isChanged = false;

            if (customer.Fullname != dto.Fullname) { customer.Fullname = dto.Fullname; isChanged = true; }
            if (customer.Phone != dto.Phone) { customer.Phone = dto.Phone; isChanged = true; }
            if (customer.Address != dto.Address) { customer.Address = dto.Address; isChanged = true; }

            if (isChanged)
            {
                var result = await _userManager.UpdateAsync(customer);
                return result.Succeeded;
            }

            return true;
        }

        public async Task<bool> ManageProfileAsync(ManageProfileDTO dto, ClaimsPrincipal user)
        {
            var customer = await _userManager.GetUserAsync(user);
            if (customer == null) return false;

            customer.Fullname = dto.Fullname;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            var result = await _userManager.UpdateAsync(customer);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

