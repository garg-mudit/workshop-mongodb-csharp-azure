using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using MoviesListingDemoApp.Models;
using MoviesListingDemoApp.Settings;

using System.Linq;

namespace MoviesListingDemoApp.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Movie> _moviesCollection;

    /// <summary>
    /// Initialize MongoClient for the given connection string, database and collection name
    /// </summary>
    /// <param name="mongoDBSettings"></param>
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _moviesCollection = database.GetCollection<Movie>(mongoDBSettings.Value.CollectionName);
    }

    /// <summary>
    /// Find all movies document in the collection
    /// </summary>
    /// <returns></returns>
    public async Task<List<Movie>> GetAsync()
    {
        var filter = Builders<Movie>.Filter.Empty;

        return await _moviesCollection.Find(filter).ToListAsync();
    }


    /// <summary>
    /// Create a movie document in the collection
    /// </summary>
    /// <param name="movie"></param>
    public async Task CreateAsync(Movie movie)
    {
        await _moviesCollection.InsertOneAsync(movie);
    }

    /// <summary>
    /// Update an existing document in the collection
    /// </summary>
    /// <param name="id"></param>
    /// <param name="genre"></param>
    public async Task UpdateGenre(string id, string genre)
    {
        var filter = Builders<Movie>.Filter
            .Eq(nameof(Movie.Id), id);
        var update = Builders<Movie>.Update.AddToSet(nameof(Movie.Genres), genre);

        var result = await _moviesCollection.UpdateOneAsync(filter, update);
    }

    /// <summary>
    /// Delete an existing document in the collection
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Movie>.Filter
            .Eq(nameof(Movie.Id), id);

        await _moviesCollection.DeleteOneAsync(filter);
    }

}