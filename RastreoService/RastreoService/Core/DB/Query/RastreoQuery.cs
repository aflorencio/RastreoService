using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RastreoService.Core.DB.Query
{
    public class RastreoQuery
    {

        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Core.DB.Models.RastreoDBModel> _rastreoCollection;

        public RastreoQuery(string connectionString) //COSNTRUCTOR 
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("RastreoService");
            _rastreoCollection = _database.GetCollection<Core.DB.Models.RastreoDBModel>("rastreo");
        }


        #region CREATE

        public async Task InserRastreo(Core.DB.Models.RastreoDBModel rastreo) //CREATE
        {
            await _rastreoCollection.InsertOneAsync(rastreo);
        }

        public async Task AddLink(Core.DB.Models.Link dataLink, string id)
        {

            var filter = Builders<Core.DB.Models.RastreoDBModel>.Filter.Eq(e => e._id, ObjectId.Parse(id));

            var update = Builders<Core.DB.Models.RastreoDBModel>.Update.Push<Core.DB.Models.Link>(e => e.links, dataLink);

            await _rastreoCollection.FindOneAndUpdateAsync(filter, update);

        }


        #endregion


        #region READ

        public List<Core.DB.Models.RastreoDBModel> GetAllRastreo()
        {
            return _rastreoCollection.Find(new BsonDocument()).ToList();
        }

        public List<Core.DB.Models.RastreoDBModel> GetRastreoByField(string fieldName, string fieldValue)
        {
            var filter = Builders<Core.DB.Models.RastreoDBModel>.Filter.Eq(fieldName, fieldValue);
            var result = _rastreoCollection.Find(filter).ToList();

            return result;
        }

        public Core.DB.Models.RastreoDBModel GetRastreoById(string id)
        {
            try
            {
                var filter = Builders<Core.DB.Models.RastreoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var data = _rastreoCollection.Find(filter).FirstOrDefault();
                return data;
            }
            catch
            {
                return null;
            }
        }

        public List<Core.DB.Models.RastreoDBModel> GetRastreo(int startingFrom, int count) //ESTE CREO QUE NO SE ESTA USANDO
        {
            var result = _rastreoCollection.Find(new BsonDocument())
            .Skip(startingFrom)
            .Limit(count)
            .ToList();

            return result;
        }

        #endregion

        #region UPDATE

        public bool UpdateRastreo(string id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<Core.DB.Models.RastreoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Core.DB.Models.RastreoDBModel>.Update.Set(udateFieldName, updateFieldValue);

            var result = _rastreoCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        #endregion


        #region DELETE

        public bool DeleteRastreoById(string id)
        {
            var filter = Builders<Core.DB.Models.RastreoDBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = _rastreoCollection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }

        #endregion
    }
}