using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMModel = RastreoService.MRastreo;

namespace RastreoService
{
    [RestResource]
    class RastreoController
    {
        MainCore _ = new MainCore("mongodb://51.83.73.69:27017", "QueueNotificationService");

        #region GET
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/queuenotification/all")]
        public IHttpContext ReadAllContacto(IHttpContext context)
        {

            var data = _.ReadAll();

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/queuenotification/one")]
        public IHttpContext ReadOneContacto(IHttpContext context)
        {

            var id = context.Request.QueryString["id"] ?? "what?"; //Si no id dara error
            var data = _.ReadId(id);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }
        #endregion

        #region POST

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/queuenotification/add")]
        public IHttpContext AddContacto(IHttpContext context)
        {

            string jsonRAW = context.Request.Payload;
            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            DMModel data = new DMModel();

            data.fecha = DateTime.Now;
            data.leido = dataId?.leido;
            data.prioridad = dataId?.prioridad;
            data.from = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.from.ToString()).Success == true ? ObjectId.Parse(dataId?.from.ToString()) : null;
            data.to = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.to.ToString()).Success == true ? ObjectId.Parse(dataId?.to.ToString()) : null;
            data.mensaje = dataId?.mensaje;
            data.serviceID = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.serviceID.ToString()).Success == true ? ObjectId.Parse(dataId?.serviceID.ToString()) : null;
            data.tipoServicio = dataId?.tipoServicio;
            data.toType = dataId?.toType;


            _.Create(data);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        #endregion

        #region PUT

        [RestRoute(HttpMethod = HttpMethod.PUT, PathInfo = "/api/queuenotification/update")]
        public IHttpContext UpdateContacto(IHttpContext context)
        {

            string jsonRAW = context.Request.Payload;
            var id = context.Request.QueryString["id"] ?? "what?";

            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            DMModel data = new DMModel();

            data._id = ObjectId.Parse(id);
            //data.fecha = DateTime.Now;
            data.leido = dataId?.leido;
            data.prioridad = dataId?.prioridad;
            data.from = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.from.ToString()).Success == true ? ObjectId.Parse(dataId?.from.ToString()) : null;
            data.to = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.to.ToString()).Success == true ? ObjectId.Parse(dataId?.to.ToString()) : null;
            data.mensaje = dataId?.mensaje;
            data.serviceID = new Regex(@"^[0-9a-fA-F]{24}$").Match(dataId?.serviceID.ToString()).Success == true ? ObjectId.Parse(dataId?.serviceID.ToString()) : null;
            data.tipoServicio = dataId?.tipoServicio;
            data.toType = dataId?.toType;


            _.Update(id, data);

            context.Response.SendResponse("Updated!");
            return context;

        }


        #endregion
    }
}
