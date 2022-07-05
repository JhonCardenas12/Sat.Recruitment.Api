using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Data
{
    public class UsersData
    {
        private readonly ILogger<User> _logger;
        const string PahtFile = "/Files/UsersDataSource.txt";
        public UsersData(ILogger<User> logger)
        {
            _logger = logger;
        }
        public StreamReader ReadUsersFromFile()
        {
            _logger.LogInformation(string.Format("Load User from: {0}", PahtFile));
            var path = Directory.GetCurrentDirectory() + PahtFile;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
