namespace InsurancePolicyManagementSystems.UI.ViewModel
{
    public class CreatePoliciesViewModel
    {
        // Unique identifier for the policy, used for CRUD operations
        public int Id { get; set; }

        // Admin-managed Policy Details
        public required string PolicyName { get; set; }
        public required string PolicyType { get; set; } // "Self" or "Group"
        public required string PlanType { get; set; } // "Basic", "Standard", "Premium"
        public required int PolicyDuration { get; set; } // In years
        public required decimal SumAssurance { get; set; }
        public required decimal AmountToBePaid { get; set; }
    }
}
