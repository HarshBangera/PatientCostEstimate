namespace PatientCostEstimate.Services
{
    public interface ICostEstimateService
    {
        Task<decimal> GetCostEstimateAsync(decimal serviceCost, string insurancePlan);
    }
}