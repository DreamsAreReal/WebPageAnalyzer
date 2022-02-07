using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebPageAnalyzer.Core.Options;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Storage;

public class Repository<T> : IDisposable where T : BaseDto
{
    private readonly IMongoClient _client;
    private readonly IMongoCollection<T> _collection;
    private readonly IClientSessionHandle _sessionHandle;

    public Repository(IMongoClient client, IOptions<MongoOptions> options)
    {
        _client = client;
        _sessionHandle = _client.StartSession();
        _collection = _client.GetDatabase(options.Value.Name).GetCollection<T>(typeof(T).ToString());
    }

    public void Dispose()
    {
        _sessionHandle.Dispose();
    }

    public async Task Add(T model)
    {
        await _collection.InsertOneAsync(model);
    }

    public async Task Remove(string url)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq(x => x.Url, url));
    }

    public async Task<T> Get(string url)
    {
        return await (await _collection.FindAsync(Builders<T>.Filter.Eq(x => x.Url, url))).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> Get()
    {
        return (await _collection.FindAsync(Builders<T>.Filter.Empty)).ToEnumerable();
    }
}