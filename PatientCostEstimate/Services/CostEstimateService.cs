namespace PatientCostEstimate.Services
{
    public class CostEstimateService : ICostEstimateService
    {
        public async Task<decimal> GetCostEstimateAsync(decimal serviceCost, string insurancePlan)
        {
            decimal serviceCostForPatient;
            switch (insurancePlan.ToString())
            {
                case "Platinum":
                    serviceCostForPatient = serviceCost - (serviceCost * 90 / 100);
                    break;
                case "Gold":
                    serviceCostForPatient = serviceCost - (serviceCost * 75 / 100);
                    break;
                case "Silver":
                    serviceCostForPatient = serviceCost - (serviceCost * 65 / 100);
                    break;
                default:
                    serviceCostForPatient = serviceCost;
                    break;
            }
            return serviceCostForPatient;
        }
    }
}
