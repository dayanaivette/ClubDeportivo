using ClubDeportivoAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClubDeportivoAPI.Services
{
    public class RegistrosClubServices
    {
        private readonly IMongoCollection<ClubDeportivo> _registrosCollection;
        public RegistrosClubServices(IOptions<ClubDeportivoDbSettings> registrosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                registrosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                registrosDatabaseSettings.Value.DatabaseName);
            _registrosCollection = mongoDatabase.GetCollection<ClubDeportivo>(
                registrosDatabaseSettings.Value.CollectionName);
        }
        public async Task<List<ClubDeportivo>> GetAsync() =>
            await _registrosCollection.Find(_ => true).ToListAsync();
        public async Task<ClubDeportivo?> GetAsync(string id) =>
            await _registrosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(ClubDeportivo registro) =>
            await _registrosCollection.InsertOneAsync(registro);
        public async Task UpdateAsync(string id, ClubDeportivo registro) =>
            await _registrosCollection.ReplaceOneAsync(x => x.Id == id, registro);
        public async Task RemoveAsync(string id) =>
            await _registrosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
