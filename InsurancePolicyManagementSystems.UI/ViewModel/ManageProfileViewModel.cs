using System.ComponentModel.DataAnnotations;
using InsurancePolicyManagementSystems.Service.DTO;

namespace InsurancePolicyManagementSystems.UI.ViewModel
{
    public class ManageProfileViewModel
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
