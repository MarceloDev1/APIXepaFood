using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class LogMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Level { get; set; } = null!; // Ex: Info, Warning, Error

        public string Message { get; set; } = null!;

        public string? Source { get; set; } // Método ou função onde ocorreu

        public string? StackTrace { get; set; }
    }
}
