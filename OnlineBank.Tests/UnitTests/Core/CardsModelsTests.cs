using OnlineBank.Core.Models;

namespace OnlineBank.Tests.UnitTests.Core
{
    public class CardsModelsTests
    {
        private string _numberCard = "1111222233334444";

        private string _dateEnd = "11/11";

        private string _cvv = "123";

        private Guid _userId = Guid.NewGuid();

        [Fact]
        public void CheckErrorCreateCards()
        {
            var result = Cards.Create(_numberCard, _dateEnd, _cvv, _userId);

            Assert.Empty(result.error);
        }

        [Fact]
        public void CheckObjectsCreateCards()
        {
            var result = Cards.Create(_numberCard, _dateEnd, _cvv, _userId);

            Assert.True(result.card is not  null);
        }

        [Fact]
        public void CreateUserWhenNumberCardIsNull()
        {
            var result = Cards.Create( "", _dateEnd, _cvv, _userId);

            Assert.Equal("Отсутствует значение number card", result.error);
        }

        [Fact]
        public void CreateUserWhenDateEndIsNull()
        {
            var result = Cards.Create( _numberCard, "", _cvv, _userId);

            Assert.Equal("Отсутствует значение dateEnd", result.error);
        }

        [Fact]
        public void CreateUserWhenCvvIsNull()
        {
            var result = Cards.Create( _numberCard, _dateEnd, "", _userId);

            Assert.Equal("Отсутствует значение cvv", result.error);
        }

        [Theory]
        [InlineData("111122223333")]
        [InlineData("11112222333344445555")]
        public void CreateUserWhenNumberCardIncorrect(string numberCard)
        {
            var result = Cards.Create( numberCard, _dateEnd, _cvv, _userId);

            Assert.Equal($"Размер значения number card должен быть равен {Cards.SIZE_NUMBERCARD} символов",
                result.error);
        }

        [Theory]
        [InlineData("11/1")]
        [InlineData("11/11/11")]
        public void CreateUserWhenDateEndIncorrect(string dateEnd)
        {
            var result = Cards.Create( _numberCard, dateEnd, _cvv, _userId);

            Assert.Equal($"Значение dateEnd должно иметь формат : {Cards.DATEEND_FORMAT}",
                result.error);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("1234")]
        public void CreateUserWhenCvvIncorrect(string cvv)
        {
            var result = Cards.Create( _numberCard, _dateEnd, cvv, _userId);

            Assert.Equal($"Размер значения cvv должен быть равным {Cards.SIZE_CVV} символов",
                result.error);
        }
    }
}
