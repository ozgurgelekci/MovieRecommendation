namespace MovieRecommendation.Application.Interfaces.Caching
{
    public interface ILocker
    {
        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);
    }
}
