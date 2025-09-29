using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace InsurancePolicyManagementSystems.UI.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {
        // File: InsurancePolicyManagementSystems.UI.Controllers/ClaimController.cs (UPDATED)
    
        private readonly IPolicyService _policyService;
        // NOTE: You must create and inject IClaimService here for saving claims
        private readonly IClaimService _claimService;

        public ClaimController(IPolicyService policyService, IClaimService claimService)
        {
            _policyService = policyService;
            _claimService = claimService;
        }

        // =================================================================
        // 1. FILE CLAIM (GET) - Shows the form
        // =================================================================
        [HttpGet]
        public async Task<IActionResult> FileClaim(int policyRegistrationId)
        {
            // 1. Fetch Policy Details (optional, but good for form display)
            var policyDetails = await _policyService.GetPolicyRegistrationByIdAsync(policyRegistrationId);
            if (policyDetails == null || policyDetails.Status != "Approved")
            {
                TempData["ErrorMessage"] = "Policy not found or is not approved for claiming.";
                return RedirectToAction("AppliedPolicies", "Policies");
            }

            // 2. Initialize the DTO for the form
            var model = new ClaimDTO
            {
                PolicyRegistrationId = policyRegistrationId,
                IncidentTitle = $"Claim for {policyDetails.PolicyName}"
            };

            ViewBag.PolicyName = policyDetails.PolicyName;
            return View(model);
        }

        // =================================================================
        // 2. FILE CLAIM (POST) - Saves the claim
        // =================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FileClaim(ClaimDTO model)
        {
            // 1. Fill in system-controlled data
            model.CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                ViewBag.PolicyName = (await _policyService.GetPolicyRegistrationByIdAsync(model.PolicyRegistrationId))?.PolicyName ?? "Policy";
                return View(model);
            }

            // 2. Call Service to Save Claim
            // NOTE: You need to implement IClaimService.FileNewClaimAsync(ClaimDTO model)
            bool success = await _claimService.FileNewClaimAsync(model);

            if (success)
            {
                TempData["SuccessMessage"] = "Claim submitted successfully. Status is Pending review.";
                return RedirectToAction("MyClaims"); // Redirect to the claims list
            }

            TempData["ErrorMessage"] = "Failed to submit claim. Please try again.";
            return View(model);
        }

        // =================================================================
        // 3. MY CLAIMS (GET) - Shows the list of filed claims
        // =================================================================
        [HttpGet]
        public async Task<IActionResult> MyClaims()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // NOTE: You need to implement IClaimService.GetClaimsByCustomerAsync(string customerId)
            var filedClaims = await _claimService.GetClaimsByCustomerAsync(customerId);

            return View(filedClaims);
        }
    }
}

