using OnlineBank.Core.Contracts;
using OnlineBank.Core.Models;
using System.Security;

namespace OnlineBank.Tests.UnitTests.Core
{
    public class DataUserModelsTests
    {
        public (DataUsers? user, string error) CreateDataUserObject(string firstName = "sergxlove", 
            string secondName = "sergxlove", string lastName = "sergxlove", string dateBirth = "02.02.2022",
            string passportData = "1111/111111", string numberPhone = "+7-000-000-00-00", 
            string email = "example@mail.com")
        {
            var result = new DataUsersRequest()
            {
                FirstName = firstName, 
                SecondName = secondName, 
                LastName = lastName,
                DateBirth = dateBirth, 
                PassportData = passportData,
                NumberPhone = numberPhone,
                Email = email
            };
            return DataUsers.Create(result); 
        }

        [Fact]
        public void CheckErrorCreateDateUser()
        {
            var result = CreateDataUserObject();

            Assert.Empty(result.error);
        }

        [Fact]
        public void CheckObjectCreateDateUser()
        {
            var result = CreateDataUserObject();

            Assert.True(result.user is not null);
        }

        [Fact]
        public void CreateDataUserWhenFirstNameNull()
        {
            var result = CreateDataUserObject(firstName: string.Empty);

            Assert.Equal("Отсутствует ФИО", result.error);
        }

        [Fact]
        public void CreateDataUserWhenLastNameNull()
        {
            var result = CreateDataUserObject(lastName: string.Empty);

            Assert.Equal("Отсутствует ФИО", result.error);
        }

        [Fact]
        public void CreateDataUserWhenSecondNameNull()
        {
            var result = CreateDataUserObject(secondName: string.Empty);

            Assert.Equal("Отсутствует ФИО", result.error);
        }

        [Fact]
        public void CreateDataUserWhenDateBirthNull()
        {
            var result = CreateDataUserObject(dateBirth: string.Empty);

            Assert.Equal("Отсутствует дата рождения", result.error);
        }

        [Fact]
        public void CreateDataUserWhenPassportDataNull()
        {
            var result = CreateDataUserObject(passportData: string.Empty);

            Assert.Equal("Отсутствуют паспортные данные", result.error);
        }

        [Fact]
        public void CreateDataUserWhenNumberPhoneNull()
        {
            var result = CreateDataUserObject(numberPhone: string.Empty);

            Assert.Equal("Отсутствует номер телефона", result.error);
        }

        [Fact]
        public void CreateDataUserWhenEmailNull()
        {
            var result = CreateDataUserObject(email: string.Empty);

            Assert.Equal("Отсутствует электронная почта", result.error);
        }

        [Fact]
        public void CreateDataUserWhenFirstNameIncorrect()
        {
            var result = CreateDataUserObject(firstName: "sergxlovesergxlovesergxlovesergxlove");

            Assert.Equal($"Имя дллжно иметь до {DataUsers.MAX_LENGTH_FIRSTNAME} символов", result.error);
        }

        [Fact]
        public void CreateDataUsersWhenSecondNameIncorrect()
        {
            var result = CreateDataUserObject(secondName: "sergxlovesergxlovesergxlovesergxlove");

            Assert.Equal($"Отчество дллжно иметь до {DataUsers.MAX_LENGTH_SECONDNAME} символов", result.error);
        }

        [Fact]
        public void CreateDataUsersWhenLastNameIncorrect()
        {
            var result = CreateDataUserObject(lastName: "sergxlovesergxlovesergxlovesergxlove");

            Assert.Equal($"Фамилия дллжно иметь до {DataUsers.MAX_LENGTH_LASTNAME} символов", result.error);
        }

        [Theory]
        [InlineData("742652")]
        [InlineData("yjklhfch")]
        public void CreateDataUsersWhenDateBirthIncorrect(string dateBirth)
        {
            var result = CreateDataUserObject(dateBirth: dateBirth);

            Assert.Equal($"Некорректный формат даты рождения", result.error);
        }

        [Theory]
        [InlineData("jkhghfd")]
        [InlineData("234675556843")]
        public void CreateDataUserWhenPassportDataIncorrect(string passportData)
        {
            var result = CreateDataUserObject(passportData: passportData);

            Assert.Equal($"Некорректный формат паспортных данных", result.error);
        }

        [Theory]
        [InlineData("43335973535")]
        [InlineData("sksfesego39")]
        public void CreateDataUserWhenNumberPhoneIncorrect(string numberPhone)
        {
            var result = CreateDataUserObject(numberPhone: numberPhone);

            Assert.Equal("Некорректный формат номера телефона", result.error);
        }

        [Theory]
        [InlineData("ser")]
        [InlineData("sergxlovesergxlovesergxlovesergxlovesergxlovesergxlove")]
        public void CreateDataUserWhenEmailIncorrect(string email)
        {
            var result = CreateDataUserObject(email: email);

            Assert.Equal($"Электронная почта должна иметь от {DataUsers.MIN_LENGTH_EMAIL} " +
                    $"до {DataUsers.MAX_LENGTH_EMAIL} символов", result.error);
        }
    }
}
