using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientCostEstimate.Models;

namespace PatientCostEstimate.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientCostEstimate.Data.PatientCostEstimateContext _context;

        public CreateModel(PatientCostEstimate.Data.PatientCostEstimateContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Patient Patient { get; set; }
        [BindProperty]
        public Insurance Insurance { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Patient.Insurances = new List<Insurance>();
            Patient.Insurances.Add(Insurance);
            //_context.Patients.Add(Patient);
            //await _context.SaveChangesAsync();
            //return RedirectToPage("./Index");
            var emptyPatient = new Patient();
            var emptyInsurance = new Insurance();
            if (await TryUpdateModelAsync<Patient>(emptyPatient, "patient", p => p.PatientName, p => p.DateOfBirth))
            {
                _context.Patients.Add(emptyPatient);
                await _context.SaveChangesAsync();

                emptyInsurance.PatientID = emptyPatient.PatientID;
                if (await TryUpdateModelAsync<Insurance>(emptyInsurance, "insurance", i => i.InsuranceProvider, i => i.InsurancePlan, i => i.InsuranceID, i => i.PatientID))
                {
                    _context.Insurances.Add(emptyInsurance);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
