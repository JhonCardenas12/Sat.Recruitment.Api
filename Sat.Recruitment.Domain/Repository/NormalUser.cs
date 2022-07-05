using Sat.Recruitment.Domain.Interfaces;


namespace Sat.Recruitment.Domain.Repository
{
    public class NormalUser : IPercentage
    {
        public decimal CalculatePercentage(decimal money)
        => money > 100 ? 2M * money : 0.0M;
    }
}
