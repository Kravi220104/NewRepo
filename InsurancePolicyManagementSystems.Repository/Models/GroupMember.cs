using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsurancePolicyManagementSystems.Repository.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsurancePolicyManagementSystems.Repository.Models
{
    public class GroupMember
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PolicyRegistration")]
        public int PolicyRegistrationId { get; set; }

        [ForeignKey(nameof(PolicyRegistrationId))]
        public RegisterForPolicies? RegisterForPolicies { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Aadhar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string? AnnualIncome { get; set; }
        public string? Relation { get; set; }

        // Bank Details
        public string? AccountHolderName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
    }
}

