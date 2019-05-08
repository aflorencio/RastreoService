using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using DBModel = RastreoService.Core.DB.Models.RastreoDBModel;

namespace RastreoService.Core.DB.Query
{
    public class RastreoQuery
    {

        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<DBModel> _Collection;

        public RastreoQuery(string connectionString) //COSNTRUCTOR 
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("RastreoService");
            _Collection = _database.GetCollection<DBModel>("rastreo");
        }


        #region CREATE

        public async Task Create(DBModel rastreo) //CREATE
        {
            await _Collection.InsertOneAsync(rastreo);
        }

        public async Task CreateToArray(Core.DB.Models.Link dataLink, string id)
        {

            var filter = Builders<DBModel>.Filter.Eq(e => e._id, ObjectId.Parse(id));

            var update = Builders<DBModel>.Update.Push<Core.DB.Models.Link>(e => e.links, dataLink);

            await _Collection.FindOneAndUpdateAsync(filter, update);

        }


        #endregion


        #region READ

        public List<DBModel> ReadAll()
        {
            return _Collection.Find(new BsonDocument()).ToList();
        }

        public List<DBModel> ReadByField(string fieldName, string fieldValue)
        {
            var filter = Builders<DBModel>.Filter.Eq(fieldName, fieldValue);
            var result = _Collection.Find(filter).ToList();

            return result;
        }

        public DBModel ReadById(string id)
        {
            try
            {
                var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var data = _Collection.Find(filter).FirstOrDefault();
                return data;
            }
            catch
            {
                return null;
            }
        }

        public List<DBModel> GetRastreo(int startingFrom, int count) //ESTE CREO QUE NO SE ESTA USANDO
        {
            var result = _Collection.Find(new BsonDocument())
            .Skip(startingFrom)
            .Limit(count)
            .ToList();

            return result;
        }

        #endregion

        #region UPDATE

        public bool Update(string id, string udateFieldName, string updateFieldValue)
        {
            var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<DBModel>.Update.Set(udateFieldName, updateFieldValue);

            var result = _Collection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        #endregion


        #region DELETE

        public bool DeleteById(string id)
        {
            var filter = Builders<DBModel>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = _Collection.DeleteOne(filter);
            return result.DeletedCount != 0;
        }

        #endregion
    }
}