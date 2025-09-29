using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Added for [Authorize]
using System.Security.Claims;          // Added for User.FindFirstValue

namespace InsurancePolicyManagementSystems.UI.Controllers
{
    // Ensure all customer-facing actions that require login are authorized
    [Authorize]
    public class PoliciesController : Controller
    {
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // =================================================================
        // NEW ACTION: SHOW POLICIES APPLIED BY CURRENT CUSTOMER
        // =================================================================
        [HttpGet]
        public async Task<IActionResult> AppliedPolicies()
        {
            // Get the ID of the currently logged-in user
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(customerId))
            {
                // This should not happen if [Authorize] is used, but it's a safe check
                return RedirectToAction("Login", "Account");
            }

            var policies = await _policyService.GetAppliedPoliciesByCustomerAsync(customerId);
            return View(policies);
        }

        // =================================================================
        // AVAILABLE POLICIES (Often accessible to all, but restricted by controller [Authorize])
        // =================================================================
        [HttpGet]
        public async Task<IActionResult> AvailablePolicies()
        {
            var policies = await _policyService.GetAllPoliciesAsync();
            return View(policies);
        }

        // =================================================================
        // REGISTER FOR POLICIES - GET
        // =================================================================
        [HttpGet]
        public async Task<IActionResult> RegisterForPolicies(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            var model = new RegisterForPoliciesDTO
            {
                PolicyId = policy.Id,
                PolicyName = policy.PolicyName,
                PlanType = policy.PlanType,
                Members = new List<GroupMemberDTO>()
            };

            return View(model);
        }

        // =================================================================
        // REGISTER FOR POLICIES - POST (MODIFIED TO SAVE CustomerId)
        // =================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterForPolicies(RegisterForPoliciesDTO model)
        {
            // CRITICAL: Get the Customer ID of the user submitting the form
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(customerId))
            {
                // If the user somehow loses authentication during post, redirect to login
                TempData["ErrorMessage"] = "You must be logged in to apply for a policy.";
                return RedirectToAction("Login", "Account");
            }

            // Ensure Members is not null for consistency checks if ModelState fails
            if (model.Members == null)
            {
                model.Members = new List<GroupMemberDTO>();
            }

            // 1. Check for basic validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string message;

            if (model.PolicyType == "Self")
            {
                // CHANGE: Pass customerId to the service
                message = await _policyService.RegisterSelfPolicyAsync(model, customerId);
            }
            else if (model.PolicyType == "Group")
            {
                // 2. Group specific validation checks
                if (model.NumberOfMembers == null || model.NumberOfMembers < 1 || model.NumberOfMembers > 3)
                {
                    ModelState.AddModelError("NumberOfMembers", "Group must have between 1 and 3 members.");
                    return View(model);
                }

                if (model.Members.Count != model.NumberOfMembers.Value)
                {
                    ModelState.AddModelError("Members", $"Consistency Error: Expected {model.NumberOfMembers.Value} members but received {model.Members.Count}. Please ensure all details are correctly entered.");
                    return View(model);
                }

                // CHANGE: Pass customerId to the service
                message = await _policyService.RegisterGroupPolicyAsync(model, customerId);
            }
            else
            {
                ModelState.AddModelError("PolicyType", "Please select a valid policy type.");
                return View(model);
            }

            // If successful, redirect to the new AppliedPolicies list
            TempData["SuccessMessage"] = message;
            return RedirectToAction("AppliedPolicies");
        }
    }
}




