using InsurancePolicyManagementSystems.Service.DTO;


namespace InsurancePolicyManagementSystems.Service.Interfaces
{
    public interface IAdminService
    {
        Task<(List<CustomerListDTO> Users, CustomerListDTO SearchedUser, bool UserNotFound)> GetCustomerListAsync(string searchEmail);
        Task UpdateCustomerStatusAsync(string email, bool isActive);
    }

    public interface IPolicyService
    {
        Task<List<CreatePoliciesDTO>> GetAllPoliciesAsync();
        Task<CreatePoliciesDTO?> GetPolicyByIdAsync(int id);
        Task CreatePolicyAsync(CreatePoliciesDTO policyDto);
        Task UpdatePolicyAsync(CreatePoliciesDTO policyDto);
        Task DeletePolicyAsync(int id);
        Task<string> RegisterSelfPolicyAsync(RegisterForPoliciesDTO model,string customerId);
        Task<string> RegisterGroupPolicyAsync(RegisterForPoliciesDTO model,string CustomerId);
        Task<List<AppliedPolicyDTO>> GetAppliedPoliciesByCustomerAsync(string customerId);
        Task<List<AllPolicyRegistrationDTO>> GetAllPolicyRegistrationsForAdminAsync();

        Task<AppliedPolicyDTO> GetPolicyRegistrationByIdAsync(int id);
        // NEW METHOD: For Admin to approve or reject a policy
        Task<bool> UpdatePolicyStatusAsync(int policyId, string status, string? rejectionReason);

        // NEW METHOD: For Admin to get the list of pending policies
        Task<List<AllPolicyRegistrationDTO>> GetPoliciesByStatusAsync(string status); // Added
    }

}

