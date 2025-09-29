using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class VerifyEmailDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}

