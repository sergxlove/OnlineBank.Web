using OnlineBank.Core.Contracts;

namespace OnlineBank.Core.Models
{
    public class Cards
    {
        public const int SIZE_NUMBERCARD = 16;
        public const string DATEEND_FORMAT = "mm/yy";
        public const int SIZE_CVV = 3;
        public Guid Id { get; }

        public string NumberCard { get; } = string.Empty;

        public string DateEnd { get; } = string.Empty;

        public string Cvv {  get; } = string.Empty;

        public Guid UserId { get; } 

        public Users? User { get; }

        public Guid? BankScoreId { get; }

        public BankScore? BankScore { get; }


        private Cards(string numberCard, string dateEnd, string cvv, Guid userId)
        {
            Id = Guid.NewGuid();
            NumberCard = numberCard;
            DateEnd = dateEnd;
            Cvv = cvv;
            UserId = userId;
        }

        private Cards(CardsRequest cr) : this (cr.NumberCard, cr.DateEnd, cr.Cvv, cr.UserId) 
        {

        }

        public static (Cards? card, string error) Create(string numberCard, string dateEnd, string cvv, Guid userId)
        {
            Cards? newCard = null;
            string error = string.Empty;

            if (string.IsNullOrEmpty(numberCard))
            {
                error = $"Отсутствует значение number card";
                return (newCard, error);
            }
            if (numberCard.Length != SIZE_NUMBERCARD)
            {
                error = $"Размер значения number card должен быть равен {SIZE_NUMBERCARD} символов";
                return (newCard, error);
            }
            if (string.IsNullOrEmpty(dateEnd))
            {
                error = $"Отсутствует значение dateEnd";
                return (newCard, error);
            }
            if (dateEnd.Length != DATEEND_FORMAT.Length)
            {
                error = $"Значение dateEnd должно иметь формат : {DATEEND_FORMAT}";
                return (newCard, error);
            }
            if (string.IsNullOrEmpty(cvv))
            {
                error = $"Отсутствует значение cvv";
                return (newCard, error);
            }
            if (cvv.Length != SIZE_CVV)
            {
                error = $"Размер значения cvv должен быть равным {SIZE_CVV} символов";
                return (newCard, error);
            }

            newCard = new(new CardsRequest()
            {
                NumberCard = numberCard,
                DateEnd = dateEnd,
                Cvv = cvv,
                UserId = userId
            });
            return (newCard,  error);
        }

    }
}
