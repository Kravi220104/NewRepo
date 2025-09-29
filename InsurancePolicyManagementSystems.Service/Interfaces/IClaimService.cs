using InsurancePolicyManagementSystems.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.Interfaces
{
    public interface IClaimService
    {
        // Saves the new claim to the database
        Task<bool> FileNewClaimAsync(ClaimDTO model);

        // Fetches claims for the customer
        Task<List<ClaimDTO>> GetClaimsByCustomerAsync(string customerId);


        Task<List<ClaimDTO>> GetClaimsByStatusAsync(string status);

        // For Admin to update the decision
        Task<bool> UpdateClaimDecisionAsync(int claimId, string status, decimal approvedAmount, string? adminComments);
    }
}
