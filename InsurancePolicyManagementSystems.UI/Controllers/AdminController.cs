using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyManagementSystems.UI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IPolicyService _policyService;

        public AdminController(IAdminService adminService, IPolicyService policyService)
        {
            _adminService = adminService;
            _policyService = policyService;
        }

        public async Task<IActionResult> CustomerList(string searchEmail)
        {
            var (dtoList, searchedDto, userNotFound) = await _adminService.GetCustomerListAsync(searchEmail);

            var viewModelList = dtoList.Select(dto => new CustomerListDTO
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                IsActive=dto.IsActive
            }).ToList();

            CustomerListDTO searchedUser = null;
            if (searchedDto != null)
            {
                searchedUser = new CustomerListDTO
                {
                    Fullname = searchedDto.Fullname,
                    Email = searchedDto.Email,
                    Phone = searchedDto.Phone,
                    Address = searchedDto.Address,
                    IsActive =searchedDto.IsActive
                };
            }

            ViewBag.SearchedUser = searchedUser;
            ViewBag.UserNotFound = userNotFound;

            return View("~/Views/Admin/Customer/CustomerList.cshtml", viewModelList);
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManagePolicies()
        {
            var policies = await _policyService.GetAllPoliciesAsync();
            return View("~/Views/Admin/Policies/ManagePolicies.cshtml", policies);
        }

        [HttpGet]
        public IActionResult CreatePolicies()
        {
            return View("~/Views/Admin/Policies/CreatePolicies.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePolicies(CreatePoliciesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Policies/CreatePolicies.cshtml", model);
            }

            await _policyService.CreatePolicyAsync(model);
            return RedirectToAction("ManagePolicies");
        }

        [HttpGet]
        public async Task<IActionResult> EditPolicies(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Policies/EditPolicies.cshtml", policy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPolicies(CreatePoliciesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Policies/EditPolicies.cshtml", model);
            }

            await _policyService.UpdatePolicyAsync(model);
            return RedirectToAction("ManagePolicies");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePolicies(int id)
        {
            await _policyService.DeletePolicyAsync(id);
            return RedirectToAction("ManagePolicies");
        }

        // GET: /Admin/Activate
        public async Task<IActionResult> Activate(string email)
        {
            await _adminService.UpdateCustomerStatusAsync(email, true);
            TempData["Message"] = "Customer activated successfully.";
            return RedirectToAction("CustomerList");
        }

        // GET: /Admin/Deactivate
        public async Task<IActionResult> Deactivate(string email)
        {
            await _adminService.UpdateCustomerStatusAsync(email, false);
            TempData["Message"] = "Customer deactivated successfully.";
            return RedirectToAction("CustomerList");
        }
        public IActionResult ViewPolicy()
        {
            return View();
        }
    }
}
