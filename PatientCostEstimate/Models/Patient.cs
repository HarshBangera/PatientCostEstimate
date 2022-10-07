using System.ComponentModel.DataAnnotations;

namespace PatientCostEstimate.Models;

public class Patient
{
    [Key]
    [Display(Name = "Patient ID")]
    public int PatientID { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    [Required]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public ICollection<Insurance> Insurances { get; set; }

}
