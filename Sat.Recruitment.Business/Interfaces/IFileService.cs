using Sat.Recruitment.Domain;
namespace Sat.Recruitment.Business.Interfaces
{
    public interface IFileService
    {
        List<User> ReadUsersFromFile();
        bool IsUserDuplicated(User user);
    }
}
