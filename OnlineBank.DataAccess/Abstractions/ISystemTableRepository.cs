namespace OnlineBank.DataAccess.Abstractions
{
    public interface ISystemTableRepository
    {
        Task<long> Get();
        Task<long> GetAndIncrement();
        Task<int> Increment();
    }
}
