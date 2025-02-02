using OnlineBank.DataAccess.Contracts.Requests;

namespace OnlineBank.DataAccess.Abstractions
{
    public interface ICardsRepository
    {
        Task<Guid> Add(string numberCard, string dateEnd, string cvv, Guid userId);
        Task<Guid> Add(RequestCards r);
        Task<int> Delete(string numberCard);
        Task<Guid?> Verify(string numberCard, string dateEnd, string cvv);
    }
}
