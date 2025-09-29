using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Fullname is required")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }



        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "The password password does not match.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}

