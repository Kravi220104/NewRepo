using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
   
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Repository.Models
{
  
        public class RegisterForPolicies
        {
            [Key]
            public int Id { get; set; }

            public int PolicyId { get; set; }
            public string? PolicyName { get; set; }
            public string? PlanType { get; set; }
            public string? PolicyType { get; set; } // Self or Group

            // Self Policy Fields
            public string? Fullname { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public string? Gender { get; set; }
            public string? Occupation { get; set; }  
            public string? AnnualIncome { get; set; }
            public string? TobaccoUse { get; set; }
            
            public string? PhoneNumber { get; set; }
            public string? AaadhaarNumber { get; set; }

            // Bank Details
            public string? AccountHolderName { get; set; }
            public string? AccountNumber { get; set; }
            public string? IFSCCode { get; set; }
            public string? BankName { get; set; }

            // Group Policy Fields
            public int? NumberOfMembers { get; set; }
            public ICollection<GroupMember> Members { get; set; }

             //tHI IS USED TO SHOW THE LIST OF REGITERED CUSTOMER FOR SPECIFIC POLICY LINKED TO THE CUSTOMER
             public string CustomerId { get; set; } // The string type used by IdentityUser.Id
             [ForeignKey("CustomerId")]
             public Customer Customer { get; set; }


        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // Default is 'Pending'

        // 2. Application Date (When the customer applied)
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        // 3. Rejection Reason (Used by Admin)
        [MaxLength(500)]
        public string? RejectionReason { get; set; }



    }
    }


