using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class ManageProfileDTO
    {
        [Required]
        public string? Fullname { get; set; }

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
        public string? Phone { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}

