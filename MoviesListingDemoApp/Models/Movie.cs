using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoviesListingDemoApp.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public int Year { get; set; } 

    public List<string> Genres { get; set; } = null!;
    
    [BsonElement("rotten_tomatoes")]
    public float RottenTomatoes { get; set; }
}