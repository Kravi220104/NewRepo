using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class ClaimDTO
    {
        // Data passed from the policy list
        public int Id { get; set; }
        public int PolicyRegistrationId { get; set; }
        public string CustomerId { get; set; } // Will be filled by controller

        // Data collected from the form
        [Required]
        public string IncidentTitle { get; set; }

        [Required]
        public DateTime IncidentDate { get; set; } = DateTime.Now;

        [Required]
        public decimal RequestedAmount { get; set; }

        [Required]
        public string Description { get; set; }

        // Claim status and details (set by system)
        public string Status { get; set; } = "Pending";
        public DateTime DateFiled { get; set; } = DateTime.Now;



        // 2. ADMIN DECISION FIELDS: CRITICAL for processing and customer display
        public decimal ApprovedAmount { get; set; } // <--- ADD THIS (Amount approved by Admin)
        public string? AdminComments { get; set; } // <--- ADD THIS (Reason for rejection or approval note)
        public DateTime? DecisionDate { get; set; } // Optional, but good practice

        public string CustomerName { get; set; } // <--- ADD THIS
        public string PolicyName { get; set; }
        public object AmountRequested { get; internal set; }
        public int ClaimId { get; internal set; }
        public int PolicyId { get; internal set; }
    }
}

