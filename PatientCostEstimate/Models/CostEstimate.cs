using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientCostEstimate.Models
{
    public class CostEstimate
    {
        [Required(ErrorMessage = "Please Select a Patient")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select a Patient")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please Select a Service")]
        public string ServiceId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostForPatient { get; set; }
    }
}
