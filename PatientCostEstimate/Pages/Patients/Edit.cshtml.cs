using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Data;
using PatientCostEstimate.Models;

namespace PatientCostEstimate.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientCostEstimate.Data.PatientCostEstimateContext _context;

        public EditModel(PatientCostEstimate.Data.PatientCostEstimateContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;
        [BindProperty]
        public Insurance Insurance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient =  await _context.Patients.Include(p => p.Insurances).FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            Insurance = patient.Insurances.FirstOrDefault();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var patientToUpdate = await _context.Patients.Include(p => p.Insurances).FirstOrDefaultAsync(m => m.PatientID == id);
            var insuranceToUpdate = patientToUpdate.Insurances.FirstOrDefault();
            if(patientToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Patient>(patientToUpdate, "patient",
                p => p.PatientName, p => p.DateOfBirth) && await TryUpdateModelAsync<Insurance>(insuranceToUpdate, "insurance",
                i => i.InsuranceProvider, i => i.InsurancePlan, i => i.InsuranceID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool PatientExists(int id)
        {
          return _context.Patients.Any(e => e.PatientID == id);
        }
    }
}
