using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClubDeportivoAPI.Models
{
    public class ClubDeportivo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string? NombreMiembro { get; set; }
        public string? Nacionalidad { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? AreaReservar { get; set; }
        public string? FechaPrestamo { get; set; }
    }
}

