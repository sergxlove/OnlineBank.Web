using OnlineBank.Core.Models;

namespace OnlineBank.Tests.UnitTests.Core
{
    public class UsersModelsTests
    {
        private string _login = "loginsergxlove";

        private string _password = "12345678";

        [Fact]
        public void CheckErrorCreateUser()
        {
            var result = Users.Create(_login, _password);

            Assert.Empty(result.error);
        }

        [Fact]
        public void CheckObjectCreateUser()
        {
            var result = Users.Create(_login, _password);

            Assert.True(result.user is not null);
        }

        [Fact]
        public void CreateUserWhenLoginIsNull()
        {
            var result = Users.Create("", _password);

            Assert.Equal("Отсутствует значение login", result.error);
        }

        [Fact]
        public void CreateUserWhenPasswordIsNull()
        {
            var result = Users.Create(_login, "");

            Assert.Equal("Отсутствует значение password", result.error);
        }

        [Theory]
        [InlineData("no")]
        [InlineData("nononononononononononono")]
        public void CreateUserWhenLoginIncorrect(string login)
        {
            var result = Users.Create(login, _password);

            Assert.Equal($"Значение login должно иметь от {Users.MIN_LENGTH_LOGIN} до {Users.MAX_LENGTH_LOGIN} символов",
                result.error);
        }

        [Theory]
        [InlineData("no")]
        [InlineData("nononononononononononononononononononononononononononononononononononono")]
        public void CreateUserWhenPasswordIncorrect(string password)
        {
            var result = Users.Create(_login, password);

            Assert.Equal($"Значение password должно иметь от {Users.MIN_LENGTH_PASSWORD} " +
                $"до {Users.MAX_LENGTH_PASSWORD} символов", result.error);
        }
    }
}
