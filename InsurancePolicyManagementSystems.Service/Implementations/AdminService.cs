//using InsurancePolicyManagementSystems.Repository.Data;
//using InsurancePolicyManagementSystems.Repository.Models;
//using InsurancePolicyManagementSystems.Service.DTO;
//using InsurancePolicyManagementSystems.Service.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace InsurancePolicyManagementSystems.Service.Implementations
//{
//    public class AdminService : IAdminService
//    {
//        private readonly UserManager<Customer> _userManager;

//        public AdminService(UserManager<Customer> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<(List<CustomerListDTO> Users, CustomerListDTO SearchedUser, bool UserNotFound)> GetCustomerListAsync(string searchEmail)
//        {
//            var allUsers = _userManager.Users.ToList();
//            var userList = new List<CustomerListDTO>();
//            CustomerListDTO searchedUser = null;
//            bool userNotFound = false;

//            foreach (var user in allUsers)
//            {
//                var dto = new CustomerListDTO
//                {
//                    Fullname = user.Fullname,
//                    Email = user.Email,
//                    Phone = user.Phone,
//                    Address = user.Address,
//                    IsActive = user.IsActive
//                };

//                if (!string.IsNullOrEmpty(searchEmail) &&
//                    user.Email.Equals(searchEmail, StringComparison.OrdinalIgnoreCase))
//                {
//                    searchedUser = dto;
//                }
//                else
//                {
//                    userList.Add(dto);
//                }
//            }

//            if (!string.IsNullOrEmpty(searchEmail) && searchedUser == null)
//            {
//                userNotFound = true;
//            }

//            return (userList, searchedUser, userNotFound);
//        }

//        public async Task UpdateCustomerStatusAsync(string email, bool isActive)
//        {
//            var customer = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
//            if (customer != null)
//            {
//                customer.IsActive = isActive;
//                await _userManager.UpdateAsync(customer);
//            }
//        }
//    }

//    public class PolicyService : IPolicyService
//    {
//        private readonly AppDbContext _context;

//        public PolicyService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<CreatePoliciesDTO>> GetAllPoliciesAsync()
//        {
//            return await _context.CreatePolicies
//                .Select(p => new CreatePoliciesDTO
//                {
//                    Id = p.Id,
//                    PolicyName = p.PolicyName,
//                    PlanType = p.PlanType,
//                    PolicyDuration = p.PolicyDuration,
//                    SumAssurance = p.SumAssurance,
//                    AmountToBePaid = p.AmountToBePaid
//                }).ToListAsync();
//        }

//        public async Task<CreatePoliciesDTO?> GetPolicyByIdAsync(int id)
//        {
//            var policy = await _context.CreatePolicies.FindAsync(id);
//            if (policy == null) return null;

//            return new CreatePoliciesDTO
//            {
//                Id = policy.Id,
//                PolicyName = policy.PolicyName,
//                PlanType = policy.PlanType,
//                PolicyDuration = policy.PolicyDuration,
//                SumAssurance = policy.SumAssurance,
//                AmountToBePaid = policy.AmountToBePaid
//            };
//        }

//        public async Task CreatePolicyAsync(CreatePoliciesDTO dto)
//        {
//            var policy = new CreatePolicies
//            {
//                PolicyName = dto.PolicyName,
//                PlanType = dto.PlanType,
//                PolicyDuration = dto.PolicyDuration,
//                SumAssurance = dto.SumAssurance,
//                AmountToBePaid = dto.AmountToBePaid
//            };

//            _context.CreatePolicies.Add(policy);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdatePolicyAsync(CreatePoliciesDTO dto)
//        {
//            var policy = await _context.CreatePolicies.FindAsync(dto.Id);
//            if (policy == null) return;

//            policy.PolicyName = dto.PolicyName;
//            policy.PlanType = dto.PlanType;
//            policy.PolicyDuration = dto.PolicyDuration;
//            policy.SumAssurance = dto.SumAssurance;
//            policy.AmountToBePaid = dto.AmountToBePaid;

//            await _context.SaveChangesAsync();
//        }

//        public async Task DeletePolicyAsync(int id)
//        {
//            var policy = await _context.CreatePolicies.FindAsync(id);
//            if (policy == null) return;

//            _context.CreatePolicies.Remove(policy);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<string> RegisterSelfPolicyAsync(RegisterForPoliciesDTO model)
//        {
//            var registration = new RegisterForPolicies
//            {
//                PolicyId = model.PolicyId,
//                PolicyName = model.PolicyName,
//                PlanType = model.PlanType,
//                PolicyType = model.PolicyType,
//                Fullname = model.Fullname,
//                DateOfBirth = model.DateOfBirth,
//                Gender = model.Gender,
//                AnnualIncome = model.AnnualIncome,
//                Occupation = model.Occupation,
//                TobaccoUse=model.TobaccoUse,
//                PhoneNumber = model.PhoneNumber,
//                AaadhaarNumber = model.AaadhaarNumber,
//                AccountHolderName = model.AccountHolderName,
//                AccountNumber = model.AccountNumber,
//                IFSCCode = model.IFSCCode,
//                BankName=model.BankName
//            };

//            _context.RegisterForPolicies.Add(registration);
//            await _context.SaveChangesAsync();

//            return $"Successfully registered for {model.PolicyName} (Self)";
//        }

//        public async Task<string> RegisterGroupPolicyAsync(RegisterForPoliciesDTO model)
//        {
//            var registration = new RegisterForPolicies
//            {
//                PolicyId = model.PolicyId,
//                PolicyName = model.PolicyName,
//                PlanType = model.PlanType,
//                PolicyType = model.PolicyType,
//                NumberOfMembers = model.NumberOfMembers,
//                Members = new List<GroupMember>()
//            };

//            foreach (var member in model.Members)
//            {
//                registration.Members.Add(new GroupMember
//                {
//                    Name = member.Name,
//                    Address = member.Address,
//                    Aadhar = member.Aadhar,
//                    DateOfBirth = member.DateOfBirth,
//                    Occupation = member.Occupation,
//                    AnnualIncome = member.AnnualIncome,
//                    Relation = member.Relation,
//                    AccountHolderName = member.AccountHolderName,
//                    AccountNumber = member.AccountNumber,
//                    IFSCCode = member.IFSCCode,
//                });
//            }

//            _context.RegisterForPolicies.Add(registration);
//            await _context.SaveChangesAsync();

//            return $"Successfully registered for {model.PolicyName} (Group)";
//        }
//    }
//}


using InsurancePolicyManagementSystems.Repository.Data;
using InsurancePolicyManagementSystems.Repository.Models;
using InsurancePolicyManagementSystems.Service.DTO;
using InsurancePolicyManagementSystems.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyManagementSystems.Service.Implementations
{
    // The AdminService remains unchanged from your provided code, but included for completeness.
    public class AdminService : IAdminService
    {
        private readonly UserManager<Customer> _userManager;

        public AdminService(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(List<CustomerListDTO> Users, CustomerListDTO SearchedUser, bool UserNotFound)> GetCustomerListAsync(string searchEmail)
        {
            // Best practice: Use AsNoTracking() and avoid ToList() before filtering/projection
            var allUsers = await _userManager.Users.AsNoTracking().ToListAsync();
            var userList = new List<CustomerListDTO>();
            CustomerListDTO searchedUser = null;
            bool userNotFound = false;

            foreach (var user in allUsers)
            {
                var dto = new CustomerListDTO
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    IsActive = user.IsActive
                };

                if (!string.IsNullOrEmpty(searchEmail) &&
                    user.Email.Equals(searchEmail, StringComparison.OrdinalIgnoreCase))
                {
                    searchedUser = dto;
                }
                else
                {
                    userList.Add(dto);
                }
            }

            if (!string.IsNullOrEmpty(searchEmail) && searchedUser == null)
            {
                userNotFound = true;
            }

            return (userList, searchedUser, userNotFound);
        }

        public async Task UpdateCustomerStatusAsync(string email, bool isActive)
        {
            var customer = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (customer != null)
            {
                customer.IsActive = isActive;
                await _userManager.UpdateAsync(customer);
            }
        }
    }

    // =================================================================
    // POLICY SERVICE IMPLEMENTATION WITH REQUIRED CHANGES
    // =================================================================
    public class PolicyService : IPolicyService
    {
        private readonly AppDbContext _context;

        public PolicyService(AppDbContext context)
        {
            _context = context;
        }

        // --- Existing Policy CRUD Methods (No changes needed) ---

        public async Task<List<CreatePoliciesDTO>> GetAllPoliciesAsync()
        {
            return await _context.CreatePolicies
                .Select(p => new CreatePoliciesDTO
                {
                    Id = p.Id,
                    PolicyName = p.PolicyName,
                    PlanType = p.PlanType,
                    PolicyDuration = p.PolicyDuration,
                    PolicyDescription = p.PolicyDescription
                }).ToListAsync();
        }

        public async Task<CreatePoliciesDTO?> GetPolicyByIdAsync(int id)
        {
            var policy = await _context.CreatePolicies.FindAsync(id);
            if (policy == null) return null;

            return new CreatePoliciesDTO
            {
                Id = policy.Id,
                PolicyName = policy.PolicyName,
                PlanType = policy.PlanType,
                PolicyDuration = policy.PolicyDuration,
                PolicyDescription = policy.PolicyDescription
            };
        }

        public async Task CreatePolicyAsync(CreatePoliciesDTO dto)
        {
            var policy = new CreatePolicies
            {
                PolicyName = dto.PolicyName,
                PlanType = dto.PlanType,
                PolicyDuration = dto.PolicyDuration,
                PolicyDescription = dto.PolicyDescription
            };

            _context.CreatePolicies.Add(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePolicyAsync(CreatePoliciesDTO dto)
        {
            var policy = await _context.CreatePolicies.FindAsync(dto.Id);
            if (policy == null) return;

            policy.PolicyName = dto.PolicyName;
            policy.PlanType = dto.PlanType;
            policy.PolicyDuration = dto.PolicyDuration;
            policy.PolicyDescription = dto.PolicyDescription;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePolicyAsync(int id)
        {
            var policy = await _context.CreatePolicies.FindAsync(id);
            if (policy == null) return;

            _context.CreatePolicies.Remove(policy);
            await _context.SaveChangesAsync();
        }

        // --- Policy Registration Methods (MODIFIED) ---

        // CHANGE 1: Accept customerId parameter
        public async Task<string> RegisterSelfPolicyAsync(RegisterForPoliciesDTO model, string customerId)
        {
            var registration = new RegisterForPolicies
            {
                PolicyId = model.PolicyId,
                PolicyName = model.PolicyName,
                PlanType = model.PlanType,
                PolicyType = model.PolicyType,
                // CHANGE 2: Map the CustomerId
                CustomerId = customerId,

                Fullname = model.Fullname,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                AnnualIncome = model.AnnualIncome,
                Occupation = model.Occupation,
                // Assuming TobaccoUse, AaadhaarNumber, BankName are new properties on your model/DTO
                TobaccoUse = model.TobaccoUse,
                PhoneNumber = model.PhoneNumber,
                AaadhaarNumber = model.AaadhaarNumber,
                AccountHolderName = model.AccountHolderName,
                AccountNumber = model.AccountNumber,
                IFSCCode = model.IFSCCode,
                BankName = model.BankName
            };

            _context.RegisterForPolicies.Add(registration);
            await _context.SaveChangesAsync();

            return $"Successfully registered for {model.PolicyName} (Self)";
        }

        // CHANGE 1: Accept customerId parameter
        public async Task<string> RegisterGroupPolicyAsync(RegisterForPoliciesDTO model, string customerId)
        {
            var registration = new RegisterForPolicies
            {
                PolicyId = model.PolicyId,
                PolicyName = model.PolicyName,
                PlanType = model.PlanType,
                PolicyType = model.PolicyType,
                // CHANGE 2: Map the CustomerId
                CustomerId = customerId,

                NumberOfMembers = model.NumberOfMembers,
                Members = new List<GroupMember>()
            };

            // Note: If you have bank details for the primary policyholder (even for Group),
            // you should map those non-member properties here as well.

            // Populate the Group Members collection
            if (model.Members != null)
            {
                foreach (var member in model.Members)
                {
                    registration.Members.Add(new GroupMember
                    {
                        Name = member.Name,
                        Address = member.Address,
                        Aadhar = member.Aadhar,
                        DateOfBirth = member.DateOfBirth,
                        Occupation = member.Occupation,
                        AnnualIncome = member.AnnualIncome,
                        Relation = member.Relation,
                        AccountHolderName = member.AccountHolderName,
                        AccountNumber = member.AccountNumber,
                        IFSCCode = member.IFSCCode,
                        // Add ConsentGiven if it exists on GroupMember model
                    });
                }
            }

            _context.RegisterForPolicies.Add(registration);
            await _context.SaveChangesAsync();

            return $"Successfully registered for {model.PolicyName} (Group)";
        }

        // NEW METHOD: Get applied policies for a specific user
        public async Task<List<AppliedPolicyDTO>> GetAppliedPoliciesByCustomerAsync(string customerId)
        {
            return await _context.RegisterForPolicies
                // CRITICAL: Filter by the CustomerId
                .Where(p => p.CustomerId == customerId)
                .Select(p => new AppliedPolicyDTO
                {
                    Id = p.Id,
                    PolicyName = p.PolicyName,
                    PlanType = p.PlanType,
                    PolicyType = p.PolicyType,
                    // Assuming ApplicationDate and Status are added/defaulted in your DTO/model
                    Status = p.Status,

                    // FIX 2: Map the RejectionReason (Needed for Rejected status)
                    RejectionReason = p.RejectionReason,

                    // FIX 3: Map the actual ApplicationDate
                    ApplicationDate = p.ApplicationDate,
                })
                .ToListAsync();
        }
        // File: InsurancePolicyManagementSystems.Service.Implementations/PolicyService.cs (UPDATED)

        // ... inside PolicyService class ...

        public async Task<List<AllPolicyRegistrationDTO>> GetAllPolicyRegistrationsForAdminAsync()
        {
            // IMPORTANT: Include the Customer navigation property to get customer details
            const string AdminEmail = "admin@insurance.com";
            return await _context.RegisterForPolicies
                .Include(r => r.Customer)
                .Where(r => r.Customer.Email != AdminEmail)
                .Select(r => new AllPolicyRegistrationDTO
                {
                    RegistrationId = r.Id,
                    PolicyName = r.PolicyName,
                    PolicyType = r.PolicyType,
                    PlanType = r.PlanType,

                    // Map the Status (assuming you have a Status property on RegisterForPolicies)
                    Status = "Pending", // Replace with r.Status if implemented
                    ApplicationDate = DateTime.Now, // Replace with r.ApplicationDate if implemented

                    // Map Customer details
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Fullname,
                    CustomerEmail = r.Customer.Email
                })
                .ToListAsync();
        }
        // File: InsurancePolicyManagementSystems.Service.Implementations/PolicyService.cs

        // ... inside PolicyService class ...

        // Implement the required interface member
        public async Task<AppliedPolicyDTO> GetPolicyRegistrationByIdAsync(int id)
        {
            // Ensure you have using Microsoft.EntityFrameworkCore; at the top of the file
            var registration = await _context.RegisterForPolicies
                // Include any linked data needed, like Customer or Policy details, if applicable
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registration == null)
            {
                return null;
            }

            // Map the entity to the DTO
            return new AppliedPolicyDTO
            {
                Id = registration.Id,
                PolicyName = registration.PolicyName,
                PolicyType = registration.PolicyType,
                PlanType = registration.PlanType,
                Status = registration.Status,
                ApplicationDate = registration.ApplicationDate,
                RejectionReason = registration.RejectionReason, // Assumed to exist on the entity
                CustomerId = registration.CustomerId
                // Map other properties as needed
            };
        }

        // Inside PolicyService class

        // NEW METHOD 1: Get policies by status (e.g., "Pending") for the Admin list view
        public async Task<List<AllPolicyRegistrationDTO>> GetPoliciesByStatusAsync(string status)
        {
            // Note: You might want to filter out the admin user if they apply for policies
            return await _context.RegisterForPolicies
                .Include(r => r.Customer) // Include Customer to show customer name/email
                .Where(r => r.Status == status)
                .Select(r => new AllPolicyRegistrationDTO
                {
                    RegistrationId = r.Id,
                    PolicyName = r.PolicyName,
                    PolicyType = r.PolicyType,
                    PlanType = r.PlanType,
                    ApplicationDate = r.ApplicationDate,
                    Status = r.Status,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.Fullname,
                    CustomerEmail = r.Customer.Email
                })
                .ToListAsync();
        }


        // NEW METHOD 2: Update the policy status
        public async Task<bool> UpdatePolicyStatusAsync(int policyId, string status, string? rejectionReason)
        {
            var policy = await _context.RegisterForPolicies.FindAsync(policyId);
            if (policy == null) return false;

            // 1. Set the new status ("Approved" or "Rejected")
            policy.Status = status;

            // 2. Set the rejection reason only if the policy is rejected
            policy.RejectionReason = (status == "Rejected") ? rejectionReason : null;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // In a real application, log the exception here
                return false;
            }
        }

        // ... rest of the PolicyService class methods ...
    }
}