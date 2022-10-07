using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Data;
using PatientCostEstimate.Models;

namespace PatientCostEstimate.Pages.Patients
{
    public class DetailsModel : PageModel
    {
        private readonly PatientCostEstimate.Data.PatientCostEstimateContext _context;

        public DetailsModel(PatientCostEstimate.Data.PatientCostEstimateContext context)
        {
            _context = context;
        }

      public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.Include(p => p.Insurances).AsNoTracking().FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }
            else 
            {
                Patient = patient;
            }
            return Page();
        }
    }
}
