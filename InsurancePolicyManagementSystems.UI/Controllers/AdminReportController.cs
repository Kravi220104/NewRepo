
using InsurancePolicyManagementSystems.Service.Interfaces; // Required for IPolicyService
using Microsoft.AspNetCore.Authorization; // Highly recommended for Admin Controllers
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyManagementSystems.UI.Controllers
{
    [Authorize(Roles = "Admin")] // Recommended: Restrict access to Admins
    public class AdminReportController : Controller
    {
        // 1. DECLARE the private field
        private readonly IPolicyService _policyService;
        //private object _claimService;
        private readonly IClaimService _claimService;

        // Note: You might also need IAdminService here if you implement other actions later
        // private readonly IAdminService _adminService; 

        // 2. CREATE the constructor for Dependency Injection (DI)
        public AdminReportController(IPolicyService policyService /*, IAdminService adminService */)
        {
            // 3. ASSIGN the injected service to the private field
            _policyService = policyService;
            // _adminService = adminService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllCustomerClaims()
        {
            return View();
        }

        public async Task<IActionResult> AllCustomerPolicies()
        {
            // This will now work because _policyService is initialized
            var allPolicies = await _policyService.GetAllPolicyRegistrationsForAdminAsync();

            return View(allPolicies);
        }
        [HttpGet]
        public async Task<IActionResult> PendingPolicies()
        {
            // Get all policies with "Pending" status
            var policiesToReview = await _policyService.GetPoliciesByStatusAsync("Pending");
            return View(policiesToReview);
        }

        // 3B. Submission Action (Handles Approve/Reject POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePolicyStatus(int id, string status, string? rejectionReason)
        {
            if (id == 0) return BadRequest("Policy ID required.");

            var result = await _policyService.UpdatePolicyStatusAsync(id, status, rejectionReason);

            if (result)
            {
                TempData["SuccessMessage"] = $"Policy {id} successfully set to {status}.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to update policy {id}. Please try again.";
            }

            // Redirect Admin back to the list of policies needing review
            return RedirectToAction("PendingPolicies");
        }








        [HttpGet]
        public async Task<IActionResult> ClaimsToProcess()
        {
            // Fetch claims where Status == "Submitted"
            var pendingClaims = await _claimService.GetClaimsByStatusAsync("Submitted");
            return View(pendingClaims);
        }

        // 2B. Submission Action (Handles Approve/Reject Claim POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessClaimDecision(
            int claimId,
            string status,
            decimal approvedAmount, // Passed via form input if Approved
            string? adminComments)   // Passed via form input for both Approved/Rejected
        {
            if (claimId == 0) return BadRequest("Claim ID required.");

            // Call the service logic to update the database
            var result = await _claimService.UpdateClaimDecisionAsync(claimId, status, approvedAmount, adminComments);

            if (result)
            {
                TempData["SuccessMessage"] = $"Claim {claimId} decision recorded as {status}.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to process claim {claimId}.";
            }

            // Redirect Admin back to the list
            return RedirectToAction("ClaimsToProcess");
        }

        // Add other actions here...

    }
}
