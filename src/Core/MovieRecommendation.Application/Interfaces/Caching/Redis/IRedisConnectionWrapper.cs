using MovieRecommendation.Application.Enums.Caching.Redis;
using StackExchange.Redis;
using System.Net;

namespace MovieRecommendation.Application.Interfaces.Caching.Redis
{
    public interface IRedisConnectionWrapper : IDisposable
    {
        IDatabase GetDatabase(int db);

        IServer GetServer(EndPoint endPoint);

        EndPoint[] GetEndPoints();

        void FlushDatabase(RedisDatabaseNumber db);

    }
}
