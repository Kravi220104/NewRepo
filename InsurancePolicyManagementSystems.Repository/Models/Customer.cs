using Microsoft.AspNetCore.Identity;

namespace InsurancePolicyManagementSystems.Repository.Models
{
    public class Customer : IdentityUser
    {
        public required string Fullname { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
