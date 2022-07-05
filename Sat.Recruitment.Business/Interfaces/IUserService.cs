using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Business.Interfaces
{
    public interface IUserService
    {
        Result CreateUser(User user);
    }
}
