using Mongo.CRUD;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMModel = RastreoService.MRastreo;

namespace RastreoService
{
    class MainCore
    {
        public IMongoCRUD<DMModel> db;
        public MainCore(string server, string database)
        {
            db = new MongoCRUD<DMModel>(server, database);
        }

        #region CREATE

        public void Create(DMModel data)
        {
            try
            {
                db.Create(data);
            }
            catch (Exception e)
            {
            }
        }

        #endregion

        #region READ
        public List<DMModel> ReadAll()
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("_id", new BsonDocument()
                    .Add("$exists", new BsonBoolean(true))
            );
            Mongo.CRUD.Models.SearchOptions so = new Mongo.CRUD.Models.SearchOptions();

            so.PageSize = 10;
            so.PageNumber = 1;
            var data = db.Search(filter, so).Documents.ToList();

            return data;

        }
        // FALTA ES BUSCAR POR CAMPO? ESTA EN QUERY       

        public DMModel ReadId(string id)
        {
            return db.Get(ObjectId.Parse(id));
        }

        //public List<DMModel> ReadValue(string fieldName, string fieldValue)
        //{


        //}
        #endregion

        #region UPDATE
        public void Update(string id, DMModel data)
        {
            DMModel document = new DMModel();

            document = db.Get(ObjectId.Parse(id));
            document = data;

            db.Update(document); //SEGURO QUE TIENES QUE ENVIAR EL DATA O EL DOCUMENT. SI ES ASI ME CAMBIA LA ID FUNCIONA ESTO BIEN?

        }

        #endregion

    }
}
