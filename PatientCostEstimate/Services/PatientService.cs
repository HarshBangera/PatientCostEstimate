using PatientCostEstimate.Models;

namespace PatientCostEstimate.Services;

public class PatientService
{
    public int AddPatient(Patient patient)
    {
        return patient.PatientID;
    }
}
