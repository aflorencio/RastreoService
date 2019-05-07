using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace RastreoService.Controllers
{
    public class RastreoController : ApiController
    {

        private Core.MainCore _ = new Core.MainCore();

        #region GET

        // GET: api/Rastreo
        [HttpGet]
        [Route("api/rastreo")]
        public List<Core.DB.Models.RastreoDBModel> Get()
        {
            var data = _.ReadAll();
            return data;
        } //POR AHORA SOLO DE TODOS LOS CAMPOS BORRAR CUANDO ESTEN TODOS LOS DATOS DEL CONTROLADOR

        // GET: api/Rastreo/5
        public string Get(int id)
        {
            return "value";
        }

        #endregion

        #region POST

        // POST: api/Rastreo
        [HttpPost]
        [Route("api/rastreo")]
        public string Post(FormDataCollection value)
        {
            string[] words = value.Get("idContactoService").ToString().Split(',');

            Core.DB.Models.RastreoDBModel data = new Core.DB.Models.RastreoDBModel();
            List<ObjectId> contactoServiceList = new List<ObjectId>();

            foreach (string word in words)
            {
                contactoServiceList.Add(ObjectId.Parse(word));
            }
            
            data.idContactoService = contactoServiceList;
            data.finalizado = value.Get("finalizado") == "true" ? true : false;
            data.idTicketService = value.Get("idTicketService");
            data.keyWord = value.Get("keyWord");

            _.CreateContacto(data);

            return "OK!";
        }

        // POST: api/Rastreo/ID
        [HttpPost]
        [Route("api/Rastreo/{id}")]
        public string Post(string id, FormDataCollection value)
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

            _.AddLink(linkes, id);
            return "OK";
        }


        #endregion

        #region PUT

        // PUT: api/Rastreo/5
        public void Put(int id, [FromBody]string value)
        {
        }


        #endregion

        #region DELETE

        // DELETE: api/Rastreo/5
        public void Delete(int id)
        {
        }

        #endregion
    }
}
