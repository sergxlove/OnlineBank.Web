using OnlineBank.DataAccess.Contracts.Requests;

namespace OnlineBank.Application.Abstractions
{
    public interface ICardsService
    {
        Task<Guid> AddNewCard(RequestCards request);
        Task<Guid> AddNewCard(string numberCard, string dateEnd, string cvv, Guid userId);
        Task<int> DeleteCard(string numberCard);
        Task<Guid?> VerifyCard(string numberCard, string dateEnd, string cvv);
        Task<string> GenerateNumberCard();
        string GenerateDateEnd();
        string GenerateCvv();
    }
}
