using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCostEstimate.Models;

public enum InsurancePlanEnum
{
    Platinum, Gold, Silver
}
public class Insurance
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string InsuranceID { get; set; }

    [Required]
    [Display(Name = "Insurance Provider")]
    [StringLength(30)]
    public string InsuranceProvider { get; set; }

    [Required]
    [Display(Name = "Insurance Plan")]
    public InsurancePlanEnum InsurancePlan { get; set; }

    public int PatientID { get; set; }
    public Patient Patient { get; set; }
}
