using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class GroupMemberDTO
    {
        // Add [Required] attributes and make DateOfBirth nullable
        [Required(ErrorMessage = "Member Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Aadhar is required.")]
        public string? Aadhar { get; set; }
        public int Age { get; set; }

        public DateTime? DateOfBirth { get; set; } // <== CHANGE: Made nullable

        [Required(ErrorMessage = "Occupation is required.")]
        public string? Occupation { get; set; }
        [Required(ErrorMessage = "Annual Income is required.")]
        public string? AnnualIncome { get; set; }
        [Required(ErrorMessage = "Relation is required.")]
        public string? Relation { get; set; }
        [Required(ErrorMessage = "Account Holder Name is required.")]
        public string? AccountHolderName { get; set; }
        [Required(ErrorMessage = "Account Number is required.")]
        public string? AccountNumber { get; set; }
        [Required(ErrorMessage = "IFSC Code is required.")]
        public string? IFSCCode { get; set; }
    }
}
