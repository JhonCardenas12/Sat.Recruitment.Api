using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Services;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Test
{
    public class Tests
    {
        private IUserService _userService;
        private IEmailService _emailService;
        private IFileService _readUsersService;

        //public UserService(ILogger<User> logger, IEmailService emailService, IFileService readUsersService)
        [SetUp]
        public void Setup()
        {
            _emailService = new EmailService();
            _readUsersService = new FileService(new NullLogger<User>(), _emailService);
            _userService = new UserService(new NullLogger<User>(), _emailService, _readUsersService);
        }

        [Test]
        public void TestCreateUserNormal()
        {
            var userRequest = new User
            {
                Name = "UserNormal",
                Email = "UserNormal@hotmail.com",
                Address = "UserNormal carrera test",
                Phone = "+57 3141246098",
                UserType = "Normal",
                Money = 435348
            };
            var result = _userService.CreateUser(userRequest);

            Assert.True(result.IsSuccess);
            Assert.AreEqual("User Created", result.Message[0]);
        }
        [Test]
        public void TestCreateUserPremium()
        {
            var userRequest = new User
            {
                Name = "UserPremium",
                Email = "UserPremium@hotmail.com",
                Address = "UserPremium carrera test",
                Phone = "+57 3141246789",
                UserType = "Premium",
                Money = 122344
            };
            var result = _userService.CreateUser(userRequest);

            Assert.True(result.IsSuccess);
            Assert.AreEqual("User Created", result.Message[0]);
        }
        [Test]
        public void TestCreateUserSuperUser()
        {
            var userRequest = new User
            {
                Name = "UserSuperUser",
                Email = "UserSuperUser@hotmail.com",
                Address = "SuperUser carrera test",
                Phone = "+57 3141246546",
                UserType = "SuperUser",
                Money = 122344
            };
            var result = _userService.CreateUser(userRequest);

            Assert.True(result.IsSuccess);
            Assert.AreEqual("User Created", result.Message[0]);
        }
        [Test]
        public void TestUserDuplicate()
        {
            var userRequest = new User
            {
                Name = "Juan",
                Email = "Juan@marmol.com",
                Address = "Peru 2464",
                Phone = "+5491154762312",
                UserType = "Normal",
                Money = 1234
            };
            var result = _userService.CreateUser(userRequest);

            Assert.False(result.IsSuccess);
            Assert.AreEqual("The user is duplicated", result.Message[0]);
        }
        [Test]
        public void TestAddressAndPhone()
        {
            var userRequest = new User
            {
                Name = "Test",
                Email = "Test@hotmail.com",
                UserType = "Normal",
                Money = 34534
            };
            var result = _userService.CreateUser(userRequest);

            Assert.False(result.IsSuccess);
            Assert.AreEqual("The address is required", result.Message[0]);
            Assert.AreEqual("The phone is required", result.Message[1]);
        }
    }
}