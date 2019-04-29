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

            Core.DB.Models.RastreoDBModel data = new Core.DB.Models.RastreoDBModel();

            data.idContactoService = value.Get("idContactoService");
            data.finalizado = value.Get("finalizado") == "true" ? true : false;
            data.idTicketService = value.Get("idTicketService");

            _.CreateContacto(data);

            return "OK!";
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
