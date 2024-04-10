using CryptLearn.Modules.AccessControl.Core.DTOs;
using CryptLearn.Modules.AccessControl.Core.Entities;
using CryptLearn.Modules.AccessControl.Core.Exceptions;
using CryptLearn.Modules.AccessControl.Core.Services;
using CryptLearn.Modules.AccessControl.Unit.Moqs;
using CryptLearn.Shared.Abstractions.Auth;
using CryptLearn.Shared.Abstractions.Time;
using CryptLearn.Shared.Infrastructure.Auth;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CryptLearn.Modules.AccessControl.Unit.Services
{
    public class AccessControlService_Test
    {
        private readonly JwtOptions _jwtOptions;
        private readonly InMemoryRefreshTokensRepository _inMemoryRefreshTokensRepository;
        private readonly InMemoryUsersRepository _inMemoryUsersRepository;
        private readonly AccessControlService _accessControlService;
        private readonly TestTokenHandler _testTokenHandler;
        private readonly TestTokenFactory _testTokenFactory;

        public AccessControlService_Test()
        {
            _jwtOptions = new() { AccessTokenCookieName = "access", RefreshTokenCookieName = "refresh", AccessTokenExpiry = TimeSpan.FromSeconds(10), RefreshTokenExpiry = TimeSpan.FromSeconds(100) };
            _inMemoryRefreshTokensRepository = new(CreateClock());
            _inMemoryUsersRepository = new();
            _testTokenHandler = new();
            _testTokenFactory = new();
            _accessControlService = new AccessControlService(_inMemoryUsersRepository, new PasswordHasher<User>(), CreatePasswordValidator(IdentityResult.Success), CreateAuthManager(), _inMemoryRefreshTokensRepository, CreateClock(), _jwtOptions, _testTokenHandler, _testTokenFactory);
        }

        private IPasswordValidator CreatePasswordValidator(IdentityResult expectedResult)
        {
            var passwordValidator = new Mock<IPasswordValidator>();
            passwordValidator.Setup(x => x.Validate(It.IsAny<string>()))
                .Returns(expectedResult);
            return passwordValidator.Object;
        }

        private IClock CreateClock()
        {
            var clock = new Mock<IClock>();
            clock.Setup(x => x.CurrentDate())
                .Returns(() => DateTime.UtcNow);
            return clock.Object;
        }

        private IAuthManager CreateAuthManager()
        {
            var authManager = new Mock<IAuthManager>();
            authManager.Setup(x => x.CreateToken(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IDictionary<string, IEnumerable<string>>>()))
                .Returns(string.Empty);
            return authManager.Object;
        }

        [SetUp]
        public async Task SetUp()
        {
            _inMemoryUsersRepository.users.Clear();
            var category = TestContext.CurrentContext.Test.Properties.Get("Category");

            if (category is "Sign in" or "Profile")
            {
                var signUpDto = new SignUpDTO
                {
                    Email = "Email",
                    Password = "123",
                    UserName = "User"
                };
                await _accessControlService.SignUpAsync(signUpDto);
            }

            if (category is "Token rotation")
            {
                _testTokenHandler.SetPendingResults(true);
                _inMemoryRefreshTokensRepository.UserRefreshTokens.Clear();
            }
        }

        [TestCase(TestName = "Plain registering user", Category = "Sign up")]
        public async Task SignUpUser()
        {
            // arrange
            var signUpDto = new SignUpDTO()
            {
                Email = "some",
                Password = "123",
                UserName = "123"
            };

            // act
            await _accessControlService.SignUpAsync(signUpDto);

            // assert

            _inMemoryUsersRepository.users.Should().HaveCount(1);
        }

        [TestCase(TestName = "Register user with email in use", Category = "Sign up")]
        public async Task SignUpUserWithSameEmail()
        {
            // arrange
            _inMemoryUsersRepository.users.Add(new()
            {
                Id = Guid.NewGuid(),
                Email = "Email1",
                NormalizedEmail = "email1",
                UserName = "User1",
                NormalizedUserName = "user1",
                PasswordHash = "",
            });
            var signUpDto = new SignUpDTO()
            {
                Email = "Email1",
                UserName = "User2",
                Password = "",
            };

            // act
            var a = async () => await _accessControlService.SignUpAsync(signUpDto);

            // assert
            await a.Should().ThrowAsync<EmailInUseException>();
        }

        [TestCase(TestName = "Register user with user name in use", Category = "Sign up")]
        public async Task SignUpUserWithSameUserName()
        {
            // arrange
            _inMemoryUsersRepository.users.Add(new()
            {
                Id = Guid.NewGuid(),
                Email = "Email1",
                NormalizedEmail = "email1",
                UserName = "User1",
                NormalizedUserName = "user1",
                PasswordHash = "",
            });
            var signUpDto = new SignUpDTO()
            {
                Email = "Email2",
                UserName = "User1",
                Password = "",
            };

            // act
            var a = async () => await _accessControlService.SignUpAsync(signUpDto);

            // assert
            await a.Should().ThrowAsync<UserNameInUseException>();
        }

        [TestCase(TestName = "Register user with weak password", Category = "Sign up")]
        public async Task SignUpUserWithWeakPassword()
        {
            // arrane
            var accessControlService = new AccessControlService(_inMemoryUsersRepository, new PasswordHasher<User>(), CreatePasswordValidator(IdentityResult.Failed()), CreateAuthManager(), null, CreateClock(), _jwtOptions, _testTokenHandler, _testTokenFactory);

            var signUpDto = new SignUpDTO()
            {
                Email = "Email2",
                UserName = "User1",
                Password = "",
            };

            // act

            var a = async () => await accessControlService.SignUpAsync(signUpDto);

            // assert
            await a.Should().ThrowAsync<WeakPasswordException>();
        }

        [TestCase(TestName = "Login registered user", Category = "Sign in")]
        public async Task SignInUser()
        {
            // arrange
            var signInDto = new SignInDTO()
            {
                Email = "Email",
                Password = "123"
            };
            var cookies = new Mock<IResponseCookies>();
            cookies.Setup(x => x.Append(_jwtOptions.AccessTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()));
            cookies.Setup(x => x.Append(_jwtOptions.RefreshTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()));

            // act
            await _accessControlService.SignInAsync(signInDto, cookies.Object);

            // assert
            using (var scope = new AssertionScope())
            {
                cookies.Verify(x => x.Append(_jwtOptions.AccessTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once());
                cookies.Verify(x => x.Append(_jwtOptions.RefreshTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once());
            }
        }

        [TestCase(TestName = "Login with wrong password", Category = "Sign in")]
        public async Task SignInUserWrongPassword()
        {
            // arrange
            var signInDto = new SignInDTO()
            {
                Email = "Email",
                Password = "1234"
            };
            var cookies = new Mock<IResponseCookies>();

            // act
            var action = async () => await _accessControlService.SignInAsync(signInDto, cookies.Object);

            // assert
            await action.Should().ThrowAsync<WrongCredentialsException>();
        }

        [TestCase(TestName = "Login with wrong email", Category = "Sign in")]
        public async Task SignInUserWrongEmail()
        {
            // arrange
            var signInDto = new SignInDTO()
            {
                Email = "Email__",
                Password = "123"
            };
            var cookies = new Mock<IResponseCookies>();

            // act
            var action = async () => await _accessControlService.SignInAsync(signInDto, cookies.Object);

            // assert
            await action.Should().ThrowAsync<WrongCredentialsException>();
        }

        [TestCase(TestName = "Sign out user", Category = "Sign out")]
        public async Task SignOutUserWithCookies()
        {
            // arrange
            var cookies = new Mock<IResponseCookies>();
            cookies.Setup(x => x.Delete(_jwtOptions.AccessTokenCookieName));
            cookies.Setup(x => x.Delete(_jwtOptions.RefreshTokenCookieName));

            // act
            await _accessControlService.SignOutAsync(cookies.Object);

            // assert
            using (var scope = new AssertionScope())
            {
                cookies.Verify(x => x.Delete(_jwtOptions.AccessTokenCookieName), Times.Once());
                cookies.Verify(x => x.Delete(_jwtOptions.RefreshTokenCookieName), Times.Once());
            }
        }

        [TestCase(TestName = "Rotate token with valid access token", Category = "Token rotation")]
        public async Task RotateTokenWithValidAccessToken()
        {
            // arrange
            var token = "some token";
            var requestCookies = new Mock<IRequestCookieCollection>();
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.AccessTokenCookieName, out token));

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, null, new());

            // assert
            result.Should().Be(token);
        }

        [TestCase(TestName = "Rotate token with refresh token equaling null", Category = "Token rotation")]
        public async Task RotateTokenWithNullRefreshToken()
        {
            // arrange
            var requestCookies = new Mock<IRequestCookieCollection>();
            var responseCookies = new Mock<IResponseCookies>();

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, responseCookies.Object, new());

            // assert
            result.Should().BeNull();
        }


        [TestCase(TestName = "Rotate token with invalid refresh token", Category = "Token rotation")]
        public async Task RotateTokenWithInvalidRefreshToken()
        {
            // arrange
            var refreshToken = "refresh token";
            _testTokenHandler.SetPendingResults(false, false);
            var requestCookies = new Mock<IRequestCookieCollection>();
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.RefreshTokenCookieName, out refreshToken));
            var responseCookies = new Mock<IResponseCookies>();

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, responseCookies.Object, new());

            // assert
            result.Should().BeNull();
        }

        [TestCase(TestName = "Rotate token with valid refresh token that is not present in database", Category = "Token rotation")]
        public async Task RotateTokenWithNonexistingUserToken()
        {
            // arrange
            var accessToken = "access token";
            var refreshToken = "refresh token";
            _testTokenHandler.SetPendingResults(false, true);
            var requestCookies = new Mock<IRequestCookieCollection>();
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.RefreshTokenCookieName, out refreshToken));
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.AccessTokenCookieName, out accessToken));
            var responseCookies = new Mock<IResponseCookies>();

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, responseCookies.Object, new());

            // assert
            result.Should().BeNull();
        }

        [TestCase(TestName = "Rotate token with inactive user token", Category = "Token rotation")]
        public async Task RotateTokenWithInactiveUserToken()
        {
            // arrange
            var accessToken = "access token";
            var refreshToken = "refresh token";
            _testTokenHandler.SetPendingResults(false, true);
            var requestCookies = new Mock<IRequestCookieCollection>();
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.RefreshTokenCookieName, out refreshToken));
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.AccessTokenCookieName, out accessToken));
            var responseCookies = new Mock<IResponseCookies>();

            var userId = Guid.NewGuid();
            _testTokenFactory.ExpectedUserId = userId;

            await _inMemoryRefreshTokensRepository.AddAsync(new() { UserId = userId, Value = refreshToken, IsActive = false });
            await _inMemoryRefreshTokensRepository.AddAsync(new() { UserId = userId });
            await _inMemoryRefreshTokensRepository.AddAsync(new() { UserId = userId });
            await _inMemoryRefreshTokensRepository.AddAsync(new() { UserId = userId });

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, responseCookies.Object, new());

            // assert
            using (var scope = new AssertionScope())
            {
                result.Should().BeNull();
                _inMemoryRefreshTokensRepository.UserRefreshTokens.Should().HaveCount(0);
            }
        }

        [TestCase(TestName = "Rotate token", Category = "Token rotation")]
        public async Task RotateToken()
        {
            // arrange
            var accessToken = "access token";
            var refreshToken = "refresh token";
            _testTokenHandler.SetPendingResults(false, true);
            var requestCookies = new Mock<IRequestCookieCollection>();
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.RefreshTokenCookieName, out refreshToken));
            requestCookies.Setup(x => x.TryGetValue(_jwtOptions.AccessTokenCookieName, out accessToken));
            var responseCookies = new Mock<IResponseCookies>();

            var userId = Guid.NewGuid();
            _testTokenFactory.ExpectedUserId = userId;

            await _inMemoryRefreshTokensRepository.AddAsync(new() { UserId = userId, Value = refreshToken });

            // act
            var result = await _accessControlService.RotateTokenAsync(requestCookies.Object, responseCookies.Object, new());

            // assert
            using (var scope = new AssertionScope())
            {
                result.Should().NotBeNull();
                _inMemoryRefreshTokensRepository.UserRefreshTokens.First().IsActive.Should().BeFalse();
                responseCookies.Verify(x => x.Append(_jwtOptions.AccessTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once());
                responseCookies.Verify(x => x.Append(_jwtOptions.RefreshTokenCookieName, It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once());
            }
        }

        [TestCase(TestName = "Change password with valid password", Category = "Password change")]
        public async Task ChangePassword()
        {
            // arrange
            var changePasswordDto = new ChangePasswordDTO()
            {
                NewPassword = "111",
            };
            var userId = Guid.NewGuid();
            _inMemoryRefreshTokensRepository.UserRefreshTokens.Add(new() { UserId = userId });
            _inMemoryRefreshTokensRepository.UserRefreshTokens.Add(new() { UserId = userId });
            _inMemoryRefreshTokensRepository.UserRefreshTokens.Add(new() { UserId = userId });
            var user = new User() { Id = userId, Email = "123", NormalizedEmail = "123", NormalizedUserName = "123", PasswordHash = "123", UserName = "123" };
            _inMemoryUsersRepository.users.Add(user);

            var cookies = new Mock<IResponseCookies>();
            cookies.Setup(x => x.Delete(_jwtOptions.AccessTokenCookieName));
            cookies.Setup(x => x.Delete(_jwtOptions.RefreshTokenCookieName));

            var passwordHasher = new Mock<IPasswordHasher<User>>();
            passwordHasher.Setup(x => x.HashPassword(user, changePasswordDto.NewPassword)).Returns("1234");
            var service = new AccessControlService(_inMemoryUsersRepository, passwordHasher.Object, CreatePasswordValidator(IdentityResult.Success), CreateAuthManager(), _inMemoryRefreshTokensRepository, CreateClock(), _jwtOptions, _testTokenHandler, _testTokenFactory);

            // act
            var action = async () => await service.ChangePassword(userId, changePasswordDto, cookies.Object);

            // assert
            using var scope = new AssertionScope();
            await action.Should().NotThrowAsync();
            _inMemoryRefreshTokensRepository.UserRefreshTokens.Should().BeEmpty();
            cookies.Verify(x => x.Delete(_jwtOptions.AccessTokenCookieName), Times.Once());
            cookies.Verify(x => x.Delete(_jwtOptions.RefreshTokenCookieName), Times.Once());
        }

        [TestCase(TestName = "Change password with invalid password", Category = "Password change")]
        public async Task ChangePasswordWithInvalidPassword()
        {
            // arrange
            var changePasswordDto = new ChangePasswordDTO()
            {
                NewPassword = "111",
            };
            var userId = Guid.NewGuid();
            var user = new User() { Id = userId, Email = "123", NormalizedEmail = "123", NormalizedUserName = "123", PasswordHash = "123", UserName = "123" };
            _inMemoryUsersRepository.users.Add(user);

            var cookies = new Mock<IResponseCookies>();
            cookies.Setup(x => x.Delete(_jwtOptions.AccessTokenCookieName));
            cookies.Setup(x => x.Delete(_jwtOptions.RefreshTokenCookieName));

            var passwordHasher = new Mock<IPasswordHasher<User>>();
            passwordHasher.Setup(x => x.HashPassword(user, changePasswordDto.NewPassword)).Returns("1234");
            var service = new AccessControlService(_inMemoryUsersRepository, passwordHasher.Object, CreatePasswordValidator(IdentityResult.Failed()), CreateAuthManager(), _inMemoryRefreshTokensRepository, CreateClock(), _jwtOptions, _testTokenHandler, _testTokenFactory);

            // act
            var action = async () => await service.ChangePassword(userId, changePasswordDto, cookies.Object);

            // assert
            await action.Should().ThrowAsync<WeakPasswordException>();
        }

        [TestCase(TestName = "Get user profile information", Category = "Profile")]
        public async Task GetUserProfileInformation()
        {
            // arrange
            var userId = _inMemoryUsersRepository.users.First().Id;

            // act
            var dto = await _accessControlService.GetProfileInformationAsync(userId);

            // assert
            dto.Should().NotBeNull();
        }
    }
}
