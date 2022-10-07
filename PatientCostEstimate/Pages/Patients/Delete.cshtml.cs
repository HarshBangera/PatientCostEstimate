using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Models;

namespace PatientCostEstimate.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly PatientCostEstimate.Data.PatientCostEstimateContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(PatientCostEstimate.Data.PatientCostEstimateContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Patient Patient { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.AsNoTracking().FirstOrDefaultAsync(m => m.PatientID == id);

            if (patient == null)
            {
                return NotFound();
            }
            else 
            {
                Patient = patient;
            }

            if(saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = string.Format($"Delete {id} failed. Try again.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            try
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete", new {id, saveChangesError = true});
            }

        }
    }
}
