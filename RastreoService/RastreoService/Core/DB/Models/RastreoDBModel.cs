using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RastreoService.Core.DB.Models
{
    public class Keyword
    {
        [BsonIgnoreIfNull]
        public string impacto { get; set; }
        [BsonIgnoreIfNull]
        public string comentario { get; set; }
        [BsonIgnoreIfNull]
        public string categoria { get; set; }
        [BsonIgnoreIfNull]
        public string idioma { get; set; }
        [BsonIgnoreIfNull]
        public string url { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class RastreoDBModel
    {
        public ObjectId _id { get; set; }
        public string idContactoService { get; set; }
        [BsonIgnoreIfNull]
        public bool finalizado { get; set; }
        public string idTicketService { get; set; }
        [BsonIgnoreIfNull]
        public List<Keyword> keywords { get; set; }
    }

}