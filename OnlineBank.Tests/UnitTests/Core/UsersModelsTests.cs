using OnlineBank.Core.Models;

namespace OnlineBank.Tests.UnitTests.Core
{
    public class UsersModelsTests
    {
        private string _login = "loginsergxlove";

        private string _password = "12345678";

        private string _numberCard = "1111222233334444";

        private string _dateEnd = "11/11";

        private string _cvv = "123";

        [Fact]
        public void CheckErrorCreateUser()
        {
            var result = Users.Create(_login, _password, _numberCard, _dateEnd, _cvv);

            Assert.Empty(result.error);
        }

        [Fact]
        public void CheckObjectCreateUser()
        {
            var result = Users.Create(_login, _password, _numberCard, _dateEnd, _cvv);

            Assert.True(result.user is not null);
        }

        [Fact]
        public void CreateUserWhenLoginIsNull()
        {
            var result = Users.Create("", _password, _numberCard, _dateEnd, _cvv);

            Assert.Equal("Отсутствует значение login", result.error);
        }

        [Fact]
        public void CreateUserWhenPasswordIsNull()
        {
            var result = Users.Create(_login, "", _numberCard, _dateEnd, _cvv);

            Assert.Equal("Отсутствует значение password", result.error);
        }

        [Fact]
        public void CreateUserWhenNumberCardIsNull()
        {
            var result = Users.Create(_login, _password, "", _dateEnd, _cvv);

            Assert.Equal("Отсутствует значение number card", result.error);
        }

        [Fact]
        public void CreateUserWhenDateEndIsNull()
        {
            var result = Users.Create(_login, _password, _numberCard, "", _cvv);

            Assert.Equal("Отсутствует значение dateEnd", result.error);
        }

        [Fact]
        public void CreateUserWhenCvvIsNull()
        {
            var result = Users.Create(_login, _password, _numberCard, _dateEnd, "");

            Assert.Equal("Отсутствует значение cvv", result.error);
        }

        [Theory]
        [InlineData("no")]
        [InlineData("nononononononononononono")]
        public void CreateUserWhenLoginIncorrect(string login)
        {
            var result = Users.Create(login, _password, _numberCard, _dateEnd, _cvv);

            Assert.Equal($"Значение login должно иметь от {Users.MIN_LENGTH_LOGIN} до {Users.MAX_LENGTH_LOGIN} символов",
                result.error);
        }

        [Theory]
        [InlineData("no")]
        [InlineData("nononononononononononononononononononononononononononononononononononono")]
        public void CreateUserWhenPasswordIncorrect(string password)
        {
            var result = Users.Create(_login, password, _numberCard, _dateEnd, _cvv);

            Assert.Equal($"Значение password должно иметь от {Users.MIN_LENGTH_PASSWORD} " +
                $"до {Users.MAX_LENGTH_PASSWORD} символов", result.error);
        }

        [Theory]
        [InlineData("111122223333")]
        [InlineData("11112222333344445555")]
        public void CreateUserWhenNumberCardIncorrect(string numberCard)
        {
            var result = Users.Create(_login, _password, numberCard, _dateEnd, _cvv);

            Assert.Equal($"Размер значения number card должен быть равен {Users.SIZE_NUMBERCARD} символов",
                result.error);
        }

        [Theory]
        [InlineData("11/1")]
        [InlineData("11/11/11")]
        public void CreateUserWhenDateEndIncorrect(string dateEnd)
        {
            var result = Users.Create(_login, _password, _numberCard, dateEnd, _cvv);

            Assert.Equal($"Значение dateEnd должно иметь формат : {Users.DATEEND_FORMAT}",
                result.error);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("1234")]
        public void CreateUserWhenCvvIncorrect(string cvv)
        {
            var result = Users.Create(_login, _password, _numberCard, _dateEnd, cvv);

            Assert.Equal($"Размер значения cvv должен быть равным {Users.SIZE_CVV} символов",
                result.error);
        }

    }
}
