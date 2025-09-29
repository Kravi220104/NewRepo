using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Repository.Models
{
  
        public class Claim
        {
            [Key]
            public int Id { get; set; }

            // Foreign Key to the Policy Registration
            // This links the claim back to the specific policy application (RegisterForPolicies)
            public int PolicyRegistrationId { get; set; }
            [ForeignKey("PolicyRegistrationId")]
            public RegisterForPolicies PolicyRegistration { get; set; }

            // Foreign Key to the Customer (redundant but useful for queries)
            public string CustomerId { get; set; }
            [ForeignKey("CustomerId")]
            public Customer Customer { get; set; }

            // Claim Details
            [Required]
            public string IncidentTitle { get; set; }

            [Required]
            public DateTime IncidentDate { get; set; }

            [Required]
            [Column(TypeName = "decimal(18, 2)")] // Ensure precise currency storage
            public decimal RequestedAmount { get; set; }

            [Required]
            public string Description { get; set; }

            // Status and Tracking
            public string Status { get; set; } = "Pending"; // Initial status
            public DateTime DateFiled { get; set; } = DateTime.Now;

            // Admin Fields (for later use when admin approves/rejects)
            public DateTime? DecisionDate { get; set; }
            public string? AdminComments { get; set; }
            public decimal? ApprovedAmount { get; set; }
        public decimal? AmountRequested { get; set; }
    }
    }
