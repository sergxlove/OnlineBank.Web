using OnlineBank.Core.Models;

namespace OnlineBank.Tests.UnitTests.Core
{
    public class DataUserModelsTests
    {
        private string _firstName = "sergxlove";
        private string _secondName = "sergxlove";
        private string _lastName = "sergxlove";
        private string _dateBirth = "02.02.2022";
        private string _passportData = "1111/111111";
        private string _numberPhone = "+7-000-000-00-00";
        private string _email = "exapmle@mail.com";
        [Fact]
        public void CheckErrorCreateDateUser()
        {
            var result = DataUsers.Create(new()
            {
                FirstName = _firstName, 
                SecondName = _secondName, 
                LastName = _lastName,
                DateBirth = _dateBirth, 
                PassportData = _passportData,
                NumberPhone = _numberPhone,
                Email = _email
            });

            Assert.Empty(result.error);
        }

        [Fact]
        public void CheckObjectCreateDateUser()
        {
            var result = DataUsers.Create(new()
            {
                FirstName = _firstName,
                SecondName = _secondName,
                LastName = _lastName,
                DateBirth = _dateBirth,
                PassportData = _passportData,
                NumberPhone = _numberPhone,
                Email = _email
            });

            Assert.True(result.dataUser is not null);
        }


    }
}
