using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.DTO
{
    public class AppliedPolicyDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string? PolicyName { get; set; }
        public string? PlanType { get; set; }
        public string? PolicyType { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now; // Assuming you add this to model later
        public string Status { get; set; } = "Pending";
        public string RejectionReason { get; set; }

    }
}
