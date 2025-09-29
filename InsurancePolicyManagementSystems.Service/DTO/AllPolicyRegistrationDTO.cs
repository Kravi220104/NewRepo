using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    
        public class AllPolicyRegistrationDTO
        {
            // Policy Registration Details
            public int RegistrationId { get; set; }
            public string PolicyName { get; set; }
            public string PolicyType { get; set; } // Self or Group
            public string PlanType { get; set; }
            public string Status { get; set; } = "Pending"; // Needs to be mapped from the model
            public DateTime ApplicationDate { get; set; } // Assuming you have a date field

            // Customer Details (to link the policy)
            public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmail { get; set; }
        }
    }

