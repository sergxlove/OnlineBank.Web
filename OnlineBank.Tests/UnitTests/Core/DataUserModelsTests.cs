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

    }
}
