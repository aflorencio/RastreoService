using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

using DBModel = RastreoService.Core.DB.Models.RastreoDBModel;


namespace RastreoService.Controllers
{
    public class RastreoController : ApiController
    {

        private Core.MainCore _ = new Core.MainCore();

        #region GET

        // GET: api/Rastreo
        [HttpGet]
        [Route("api/rastreo")]
        public List<DBModel> Get()
        {
            var data = _.ReadAll();
            return data;
        } 

        // GET: api/Rastreo/5
        public DBModel Get(string id)
        {
            var data = _.ReadById(id);

            return data;

        }

        #endregion

        #region POST

        // POST: api/Rastreo
        [HttpPost]
        [Route("api/rastreo")]
        public string Post(FormDataCollection value)
        {
            string[] words = value.Get("idContactoService").ToString().Split(',');

            DBModel data = new DBModel();

            List<ObjectId> contactoServiceList = new List<ObjectId>();
            foreach (string word in words)
            {
                contactoServiceList.Add(ObjectId.Parse(word));
            }
            data.idContactoService = contactoServiceList;

            data.finalizado = value.Get("finalizado") == "true" ? true : false;
            data.idTicketService = value.Get("idTicketService");
            data.keyWord = value.Get("keyWord");

            _.Create(data);

            return "OK!";
        }

        // POST: api/Rastreo/ID
        [HttpPost]
        [Route("api/Rastreo/{id}")]
        public string Post(FormDataCollection value, string id)
        {
            Core.DB.Models.Link linkes = new Core.DB.Models.Link();

            linkes.url = value.Get("url");
            linkes.comentario = value.Get("comentario");
            linkes.categoria = value.Get("categoria");
            linkes.status = value.Get("status");
            linkes.originalPDF = value.Get("originalPDF");
            linkes.finalPDF = value.Get("finalPDF");
            linkes._id = ObjectId.GenerateNewId();
            linkes.impacto = "50";
            linkes.idioma = value.Get("idioma");

            _.CreateToArray(linkes, id);
            return "OK";
        }


        #endregion

        #region PUT

        // PUT: api/Rastreo/5
        [HttpPut]
        [Route("api/Rastreo/{id}")]
        public string Put(string id, FormDataCollection value)
        {
            var name = value.FirstOrDefault().Key.ToString();
            var valor = value.FirstOrDefault().Value.ToString();

            DBModel obj = new DBModel();
            var existeMetodo = obj.GetType().GetProperty(name) == null ? false : true;
            if (existeMetodo == true)
            {
                _.Update(id, name, valor);
                return "OK!";
            }

            return "Error";

        }

        #endregion

        #region DELETE

        // DELETE: api/Rastreo/5
        [HttpDelete]
        [Route("api/Rastreo/{id}")]
        public void Delete(string id)
        {
            _.DeleteById(id);
        }

        #endregion
    }
}
