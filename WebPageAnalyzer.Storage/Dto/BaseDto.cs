using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebPageAnalyzer.Storage.Dto;

public abstract class BaseDto
{
    [BsonId]
    public ObjectId Id { get; }
    public string Url { get; set; }
    
}