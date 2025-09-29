using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class RegisterForPoliciesDTO
    {
        public int PolicyId { get; set; }
        public string? PolicyName { get; set; }
        public string? PlanType { get; set; }

        // Must be required for all submissions
        [Required(ErrorMessage = "Policy Type is required.")]
        public string? PolicyType { get; set; } // Self or Group

        // Self fields - Made DateOfBirth nullable
        public string? Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; } // <== CHANGE: Made nullable
        public string? Gender { get; set; }
        public string? AnnualIncome { get; set; }
        public string? TobaccoUse { get; set; }
        public string? Occupation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AaadhaarNumber { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }

        // Group fields - Made NumberOfMembers nullable
        public int? NumberOfMembers { get; set; } // <== CHANGE: Made nullable
        public List<GroupMemberDTO> Members { get; set; } = new();

        // Add a validation method for self-consistency (Optional but recommended)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PolicyType == "Self")
            {
                if (string.IsNullOrEmpty(Fullname)) yield return new ValidationResult("Full Name is required for Self policy.", new[] { nameof(Fullname) });
                if (DateOfBirth == null) yield return new ValidationResult("Date of Birth is required for Self policy.", new[] { nameof(DateOfBirth) });
                // Add more [Required] checks for other Self fields here...
            }
            // Add similar logic for Group policy.
        }
    }
}



