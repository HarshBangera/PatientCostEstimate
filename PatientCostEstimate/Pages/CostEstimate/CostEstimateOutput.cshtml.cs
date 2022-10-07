using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PatientCostEstimate.Data;
using PatientCostEstimate.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientCostEstimate.Pages.CostEstimate
{
    public class CostEstimateOutputModel : PageModel
    {
        private readonly PatientCostEstimateContext _context;

        public CostEstimateOutputModel(PatientCostEstimateContext context)
        {
            _context = context;
        }

        public Patient Patient { get; set; }
        public Service Service { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostForPatient { get; set; }

        public async Task OnGetAsync()
        {
            var sessionData = TempData.Get<Models.CostEstimate>("SessionData");
            Patient = await _context.Patients.Include(p => p.Insurances).AsNoTracking().FirstOrDefaultAsync(m => m.PatientID == sessionData.PatientId);
            Service = _context.Services.Where(s => s.ServiceID.Equals(sessionData.ServiceId)).FirstOrDefault();
            CostForPatient = sessionData.CostForPatient;
        }
    }
}
