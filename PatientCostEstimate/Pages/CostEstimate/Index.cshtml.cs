using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Data;
using PatientCostEstimate.Models;
using PatientCostEstimate.Services;
using System.ComponentModel.DataAnnotations;

namespace PatientCostEstimate.Pages.CostEstimate
{
    public class IndexModel : PageModel
    {
        private readonly PatientCostEstimateContext _context;
        private readonly ICostEstimateService _costEstimateService;

        public IndexModel(PatientCostEstimateContext context, ICostEstimateService costEstimateService)
        {
            _context = context;
            _costEstimateService = costEstimateService;
        }

        [BindProperty]
        public Models.CostEstimate CostEstimate { get; set; }
        public IList<Patient> PatientList { get; set; }
        public IList<Service> ServiceList { get; set; }

        public async Task OnGetAsync()
        {
            if(_context.Patients != null)
            {
                PatientList = await _context.Patients.AsNoTracking().ToListAsync() ?? new List<Patient>();
                ServiceList = await _context.Services.AsNoTracking().ToListAsync() ?? new List<Service>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (string.IsNullOrEmpty(CostEstimate.ServiceId) || CostEstimate.PatientId == 0)
            //{
            //    return Page();
            //}

            ModelState.ClearValidationState(nameof(CostEstimate));
            if (!TryValidateModel(CostEstimate, nameof(CostEstimate)))
            {
                PatientList = await _context.Patients.AsNoTracking().ToListAsync() ?? new List<Patient>();
                ServiceList = await _context.Services.AsNoTracking().ToListAsync() ?? new List<Service>();
                return Page();
            }

            IQueryable<Service> serviceIQ = from s in _context.Services
                                              select s;
            var serviceCost = serviceIQ.Where(s => s.ServiceID.Equals(CostEstimate.ServiceId)).FirstOrDefault().Cost;
            IQueryable<Patient> patientsIQ = from p in _context.Patients
                                             select p;
            var insurancePlan = patientsIQ.Where(p => p.PatientID == CostEstimate.PatientId).Include(p => p.Insurances).FirstOrDefault().Insurances.FirstOrDefault().InsurancePlan;
            CostEstimate.CostForPatient = await _costEstimateService.GetCostEstimateAsync(serviceCost, insurancePlan.ToString());
            TempData.Set("SessionData", CostEstimate);
            return RedirectToPage("./CostEstimateOutput");
        }
    }
}
