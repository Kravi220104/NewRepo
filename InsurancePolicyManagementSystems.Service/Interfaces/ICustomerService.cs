using InsurancePolicyManagementSystems.Service.DTO;
using System.Security.Claims;

namespace InsurancePolicyManagementSystems.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> LoginAsync(LoginDTO dto);
        Task<RegisterResultDTO> RegisterAsync(RegisterDTO dto);
        Task<string> GetUserRoleAsync(string email);
        Task<CustomerListDTO> GetCustomerByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO dto);
        Task<CustomerListDTO> GetCurrentCustomerAsync(ClaimsPrincipal user);
        Task<bool> UpdateProfileAsync(UpdateProfileDTO dto, ClaimsPrincipal user);
        Task<bool> ManageProfileAsync(ManageProfileDTO dto, ClaimsPrincipal user);
        Task LogoutAsync();
    }
}
