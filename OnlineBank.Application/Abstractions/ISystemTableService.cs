namespace OnlineBank.Application.Abstractions
{
    public interface ISystemTableService
    {
        Task<long> GetAndIncrementNumberCard();
        Task<long> GetNumberCard();
        Task<int> IncrementNumberCard();
    }
}
