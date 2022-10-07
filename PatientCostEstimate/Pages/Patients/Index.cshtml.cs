using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PatientCostEstimate.Models;
using Microsoft.Extensions.Configuration;

namespace PatientCostEstimate.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly PatientCostEstimate.Data.PatientCostEstimateContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(PatientCostEstimate.Data.PatientCostEstimateContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Patient> Patients { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            if (_context.Patients != null)
            {
                CurrentSort = sortOrder;
                NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";
                if(searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                CurrentFilter = searchString;

                IQueryable<Patient> patientsIQ = from p in _context.Patients
                                                 select p;

                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    patientsIQ = patientsIQ.Where(x => x.PatientName.Contains(CurrentFilter));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        patientsIQ = patientsIQ.OrderByDescending(p => p.PatientName);
                        break;
                    case "Date":
                        patientsIQ = patientsIQ.OrderBy(p => p.DateOfBirth);
                        break;
                    case "date_desc":
                        patientsIQ = patientsIQ.OrderByDescending(p => p.DateOfBirth);
                        break;
                    default:
                        patientsIQ = patientsIQ.OrderBy(p => p.PatientName);
                        break;
                }

                var pageSize = _configuration.GetValue("PageSize", 4);
                Patients = await PaginatedList<Patient>.CreateAsync(patientsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }
    }
}
