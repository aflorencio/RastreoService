using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoServiceConsole.Core.DB.Models
{
    public class Link
    {
        public ObjectId _id { get; set; }
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
        [BsonIgnoreIfNull]
        public string status { get; set; }
        [BsonIgnoreIfNull]
        public string originalPDF { get; set; }
        [BsonIgnoreIfNull]
        public string finalPDF { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class RastreoDBModel
    {
        public ObjectId _id { get; set; }
        [BsonIgnoreIfNull]
        public List<ObjectId> idContactoService { get; set; }
        [BsonIgnoreIfNull]
        public bool finalizado { get; set; }
        [BsonIgnoreIfNull]
        public string idTicketService { get; set; }
        [BsonIgnoreIfNull]
        public string keyWord { get; set; }
        [BsonIgnoreIfNull]
        public List<Link> links { get; set; }
    }


}
