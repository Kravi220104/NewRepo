using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyManagementSystems.UI.ViewModel
{
    public class UpdateProfileViewModel
    {
        [Required]
        public string? Fullname { get; set; }

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
