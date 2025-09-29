using InsurancePolicyManagementSystems.Repository.Data;
using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using InsurancePolicyManagementSystems.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyManagementSystems.Service.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly AppDbContext _context;

        public ClaimService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FileNewClaimAsync(ClaimDTO model)
        {
            if (model == null) return false;

            // Map DTO to the database Model (Claim)
            var claimEntity = new Claim
            {
                PolicyRegistrationId = model.PolicyRegistrationId,
                CustomerId = model.CustomerId,
                IncidentTitle = model.IncidentTitle,
                IncidentDate = model.IncidentDate,
                RequestedAmount = model.RequestedAmount,
                Description = model.Description,
                Status = "Pending", // Always save as pending initially
                DateFiled = DateTime.Now
            };

            try
            {
                await _context.Claims.AddAsync(claimEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (recommended)
                return false;
            }
        }

        public async Task<List<ClaimDTO>> GetClaimsByCustomerAsync(string customerId)
        {
            return await _context.Claims
                .Where(c => c.CustomerId == customerId)
                .Select(c => new ClaimDTO
                {
                    PolicyRegistrationId = c.PolicyRegistrationId,
                    CustomerId = c.CustomerId,
                    IncidentTitle = c.IncidentTitle,
                    IncidentDate = c.IncidentDate,
                    RequestedAmount = c.RequestedAmount,
                    Description = c.Description,
                    Status = c.Status,
                    DateFiled = c.DateFiled
                })
                .ToListAsync();
        }



        public async Task<List<ClaimDTO>> GetClaimsByStatusAsync(string status)
        {
            return await _context.Claims
                .Include(c => c.PolicyRegistration) // Link to policy for context
                .Where(c => c.Status == status)
                .Select(c => new ClaimDTO // Assuming you have this DTO
                {
                    ClaimId = c.Id,
                    PolicyId = c.PolicyRegistrationId,
                    CustomerName = c.PolicyRegistration.Fullname, // Or map from Customer entity
                    PolicyName = c.PolicyRegistration.PolicyName,
                    Status = c.Status,
                    AmountRequested = c.AmountRequested,
                    // ... map other relevant details for Admin review
                })
                .ToListAsync();
        }

        // Implementation for 1A: Update Claim Decision (Core Logic)
        public async Task<bool> UpdateClaimDecisionAsync(int claimId, string status, decimal approvedAmount, string? adminComments)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim == null) return false;

            claim.Status = status; // "Approved" or "Rejected"
            claim.DecisionDate = DateTime.Now;

            // Only set approved amount if approved
            claim.ApprovedAmount = (status == "Approved") ? approvedAmount : 0;

            // Set the admin's comments for customer visibility
            claim.AdminComments = adminComments;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

    }
}