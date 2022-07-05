using Sat.Recruitment.Domain.Interfaces;


namespace Sat.Recruitment.Domain.Repository
{
    public class SuperUser : IPercentage
    {
        public decimal CalculatePercentage(decimal money)
        => money > 100 ? money * 0.20M : 0.0M;
    }
}
