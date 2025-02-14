using OnlineBank.Core.Contracts;

namespace OnlineBank.Core.Models
{
    public class BankScore
    {
        public Guid Id { get; }

        public string Number { get; } = string.Empty;

        public string CurrencyCode { get; } = string.Empty;

        public decimal Balance { get; }

        public string AccountType { get; } = string.Empty;

        public string Status { get; } = string.Empty;

        public Guid UsersId { get; } 

        public Users? Users { get; }

        public Guid? CardId { get; set; }

        public Cards? Cards { get; }

        private BankScore(Guid id, string number, string currencyCode, decimal balance,
            string accountType, string status, Guid userId)
        {
            Id = id;
            Number = number;
            CurrencyCode = currencyCode;
            Balance = balance;
            AccountType = accountType;
            Status = status;
            UsersId = userId;
        }

        public static (BankScore? score, string error) Create (Guid id, string number, string currencyCode,
            decimal balance, string accountType, string status, Guid userId)
        {
            BankScore? score = null;
            string error = string.Empty;

            score = new BankScore(id, number, currencyCode, balance, accountType, status, userId);
            return(score, error);
        }

        public static (BankScore? score, string error) Create (BankScoreRequest r)
        {
            return Create(r.Id, r.Number, r.CurrencyCode, r.Balance, r.AccountType, r.Status, r.UsersId);
        }
    }
}
