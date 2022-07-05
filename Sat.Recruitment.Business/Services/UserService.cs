using Microsoft.Extensions.Logging;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Interfaces;

namespace Sat.Recruitment.Business.Services
{
    public class UserService : IUserService
    {
        private List<User> _users;
        private Result _response;
        private readonly ILogger<User> _logger;
        private readonly IEmailService _emailService;
        private readonly IFileService _readUsersService;

        public UserService(ILogger<User> logger, IEmailService emailService, IFileService readUsersService)
        {
            _logger = logger;
            _users = new List<User>();
            _emailService = emailService;
            _readUsersService = readUsersService;
            _response = new Result
            {
                IsSuccess = true,
                Message = new List<string>()
            };
        }
        public Result CreateUser(User user)
        {
            _logger.LogInformation("Run Creating User", user);
            try
            {
                if (Validate(user))
                {
                    User newUser = new User
                    {
                        Name = user.Name,
                        Email = _emailService.NormalizeEmail(user.Email),
                        Address = user.Address,
                        Phone = user.Phone,
                        UserType = user.UserType,
                        Money = user.Money
                    };

                    IPercentage selectedStrategy = FileService.percentage[user.UserType]();
                    newUser.Money += selectedStrategy.CalculatePercentage(user.Money);
                    _users = _readUsersService.ReadUsersFromFile();
                    if (_readUsersService.IsUserDuplicated(newUser))
                    {
                        SetResponse(false, "The user is duplicated");
                    }
                    else
                    {
                        SetResponse(true, "User Created");
                    }

                }
                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, user);
                SetResponse(false, ex.Message);
                return _response;
            }
        }

        private bool Validate(User user)
        {
            _logger.LogInformation("Check Validations");
            if (string.IsNullOrWhiteSpace(user.Name))
                SetResponse(false, "The name is required");
            if (string.IsNullOrWhiteSpace(user.Email))
                SetResponse(false, "The email is required");
            if (string.IsNullOrWhiteSpace(user.Address))
                SetResponse(false, "The address is required");
            if (string.IsNullOrWhiteSpace(user.Phone))
                SetResponse(false, "The phone is required");

            return _response.IsSuccess;
        }

        private void SetResponse(bool value, string message)
        {
            _response.IsSuccess = value;
            _response.Message.Add(message);
        }
    }
}
