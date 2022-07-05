using Microsoft.Extensions.Logging;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Data;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Repository;

namespace Sat.Recruitment.Business.Services
{
    public class FileService : IFileService
    {
        private readonly List<User> _users;
        private readonly UsersData _readUsers;
        private readonly ILogger<User> _logger;
        private readonly IEmailService _emailService;
        public static readonly Dictionary<string, Func<IPercentage>> percentage = new Dictionary<string, Func<IPercentage>>() {
            { "Normal", () => new NormalUser() },
            { "Premium", () => new PremiumUser() },
            { "SuperUser", () => new SuperUser() }
        };
        public FileService(ILogger<User> logger, IEmailService emailService)
        {
            _logger = logger;
            _users = new List<User>();
            _readUsers = new UsersData(_logger);
            _emailService = emailService;
        }
        public List<User> ReadUsersFromFile()
        {
            StreamReader reader = _readUsers.ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                if (line != null)
                {
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = _emailService.NormalizeEmail(line.Split(',')[1].ToString()),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = string.IsNullOrWhiteSpace(line.Split(',')[5].ToString()) ? 0M : decimal.Parse(line.Split(',')[5].ToString())
                    };
                    IPercentage selectedStrategy = percentage[user.UserType]();
                    user.Money += selectedStrategy.CalculatePercentage(user.Money);
                    _users.Add(user);
                }
            }
            reader.Close();
            return _users;
        }
        public bool IsUserDuplicated(User user)
        {
            _logger.LogInformation("Check if User is Duplicated");
            return _users.Exists(x => x.Name == user.Name || x.Address == user.Address || x.Email == user.Email || x.Phone == user.Phone);
        }

    }
}
