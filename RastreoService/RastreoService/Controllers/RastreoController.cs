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
        public Core.DB.Models.RastreoDBModel Get(string id)
        {
            var data = _.ReadById(id);

            return data;

        }

        #endregion

        #region POST

        // POST: api/Rastreo
        [HttpPost]
        [Route("api/rastreo")]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            var jsonString = await request.Content.ReadAsStringAsync();

            Core.DB.Models.RastreoDBModel account = JsonConvert.DeserializeObject<Core.DB.Models.RastreoDBModel>(jsonString);
            _.Create(account);

            return new HttpResponseMessage(HttpStatusCode.Created);

            //string[] words = value.Get("idContactoService").ToString().Split(',');

            //Core.DB.Models.RastreoDBModel data = new Core.DB.Models.RastreoDBModel();
            //List<ObjectId> contactoServiceList = new List<ObjectId>();

            //foreach (string word in words)
            //{
            //    contactoServiceList.Add(ObjectId.Parse(word));
            //}

            //data.idContactoService = contactoServiceList;
            //data.finalizado = value.Get("finalizado") == "true" ? true : false;
            //data.idTicketService = value.Get("idTicketService");
            //data.keyWord = value.Get("keyWord");

            //_.CreateContacto(data);

            //return "OK!";

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

            _.CreateToArray(linkes, id);
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
        public void Delete(string id)
        {
            _.DeleteById(id);
        }

        #endregion
    }
}
