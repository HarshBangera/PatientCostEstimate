using PatientCostEstimate.Models;

namespace PatientCostEstimate.Data;

public class DbInitializer
{
    public static void Initialize(PatientCostEstimateContext context)
    {
        if (context.Patients.Any())
        {
            return;
        }

        var patients = new Patient[]
        {
            new Patient{ PatientName="Dummy Sharma", DateOfBirth=DateTime.Parse("1995-03-26")},
            new Patient{ PatientName="Dummy Verma", DateOfBirth=DateTime.Parse("1965-07-06")}
        };
        context.Patients.AddRange(patients);
        context.SaveChanges();

        var services = new Service[]
        {
            new Service{ServiceID="SID001", ServiceName="MRI", Cost=10000M},
            new Service{ServiceID="SID002", ServiceName="ECG", Cost=5000M},
            new Service{ServiceID="SID003", ServiceName="CBC", Cost=1000M}
        };
        context.Services.AddRange(services);
        context.SaveChanges();

        var insurances = new Insurance[]
        {
            new Insurance{InsuranceID="HDFC12345", InsuranceProvider="HDFC", InsurancePlan=InsurancePlanEnum.Gold, PatientID=1 },
            new Insurance{InsuranceID="SBI12345", InsuranceProvider="SBI", InsurancePlan=InsurancePlanEnum.Platinum, PatientID=2}
        };
        context.Insurances.AddRange(insurances);
        context.SaveChanges();
    }
}
