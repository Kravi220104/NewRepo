using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceManagement.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _customerService.LoginAsync(dto);
            if (success)
            {
                var role = await _customerService.GetUserRoleAsync(dto.Email);
                return role switch
                {
                    "Admin" => RedirectToAction("AdminDashboard", "Admin"),
                    "User" => RedirectToAction("CustomerDashboard", "Customer"),
                    _ => RedirectToAction("Index", "Home")
                };
            }

            ModelState.AddModelError("", "Customer not found or Inactive");
            return View(dto);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _customerService.RegisterAsync(dto);
            if (result.Success)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View(dto);
        }

        [HttpGet]
        public IActionResult VerifyEmail() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var customer = await _customerService.GetCustomerByEmailAsync(dto.Email);
            if (customer == null)
            {
                ModelState.AddModelError("", "Customer not found");
                return View(dto);
            }

            return RedirectToAction("ChangePassword", new { username = customer.Email });
        }

        [HttpGet]
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("VerifyEmail");

            return View(new ChangePasswordDTO { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(dto);
            }

            var success = await _customerService.ChangePasswordAsync(dto);
            if (success)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Failed to change password");
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _customerService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CustomerDashboard()
        {
            var customer = await _customerService.GetCurrentCustomerAsync(User);
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            var customer = await _customerService.GetCurrentCustomerAsync(User);
            var dto = new ManageProfileDTO
            {
                Fullname = customer.Fullname,
                Phone = customer.Phone,
                Address = customer.Address,
                Email = customer.Email
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageProfile(ManageProfileDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _customerService.ManageProfileAsync(dto, User);
            if (success)
                return RedirectToAction("CustomerDashboard");

            ModelState.AddModelError("", "Failed to update profile");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var customer = await _customerService.GetCurrentCustomerAsync(User);
            if (customer == null) return RedirectToAction("Login");

            var dto = new UpdateProfileDTO
            {
                Fullname = customer.Fullname,
                Phone = customer.Phone,
                Address = customer.Address
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _customerService.UpdateProfileAsync(dto, User);
            if (success)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("CustomerDashboard");
            }

            ModelState.AddModelError("", "Failed to update profile");
            return View(dto);
        }

        public IActionResult Policies() => View();
    }
}


