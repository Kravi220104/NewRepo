using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Repository.Models
{
    public class CreatePolicies
    {
        
        public int Id { get; set; }

    
        public required string PolicyName { get; set; }
        public required string PlanType { get; set; } // "Basic", "Standard", "Premium"
        public required int PolicyDuration { get; set; } // In years
        public required string PolicyDescription { get; set; } // In years
        //public required decimal SumAssurance { get; set; }
        //public required decimal AmountToBePaid { get; set; }

    }
}
