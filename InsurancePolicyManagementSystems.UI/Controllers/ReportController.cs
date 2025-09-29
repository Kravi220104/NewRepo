using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicyManagementSystems.UI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AppliedClaims()
        {
            // This will return the view in the Report folder
            return View();
        }
    }
}
