using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCostEstimate.Models;

public class Service
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string ServiceID { get; set; }

    [Required]
    [Display(Name = "Service Name")]
    [StringLength(30)]
    public string ServiceName { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total Cost")]
    public decimal Cost { get; set; }
}
