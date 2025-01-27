namespace OnlineBank.DataAccess.Abstractions
{
    public interface ICardsRepository
    {
        Task<Guid> Add(string numberCard, string dateEnd, string cvv, Guid userId);
        Task<int> Delete(string numberCard);
    }
}
