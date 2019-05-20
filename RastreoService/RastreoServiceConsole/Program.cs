using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RastreoServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var v = "v0.1.0.0";
            Console.Title = "Rastreo Service " + v;
            Console.WriteLine("     R A S T R E O   S E R V I C E    " + v);
            var serverStandar = new RestServer();

            var input = "";
            while ((input = Console.ReadLine()) != "q")
            {
                switch (input)
                {
                    case "start":
                        Console.WriteLine("Starting service...");
                        serverStandar.Port = "5002";
                        serverStandar.Host = "*";
                        serverStandar.Start();
                        Console.Title = "[ON] RastreoService " + v;

                        break;


                    case "start --log":
                        Console.WriteLine("Starting service...");

                        using (var server = new RestServer())
                        {
                            server.Port = "5002";
                            server.Host = "*";
                            server.LogToConsole().Start();
                            Console.Title = "[ON] RastreoService " + v;
                            Console.ReadLine();
                            server.Stop();
                        }

                        break;
                    case "stop":
                        Console.WriteLine("Stopping service...");
                        serverStandar.Stop();
                        Console.Title = "RastreoService " + v;
                        break;
                    case "--version":
                        Console.WriteLine(v);

                        break;
                    default:
                        Console.WriteLine(String.Format("Unknown command: {0}", input));
                        break;
                }

            }
        }

    }

    [RestResource]
    public class TestResource
    {
        #region GET
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/rastreo/all")]
        public IHttpContext ReadAllContacto(IHttpContext context)
        {
            Core.MainCore _ = new Core.MainCore();

            var data = _.ReadAll();

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/rastreo/one")]
        public IHttpContext ReadOneContacto(IHttpContext context)
        {
            Core.MainCore _ = new Core.MainCore();

            var id = context.Request.QueryString["id"] ?? "what?"; //Si no id dara error
            var data = _.ReadById(id);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }
        #endregion

        #region POST

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/rastreo/add")]
        public IHttpContext AddRastreo(IHttpContext context)
        {
            Core.MainCore _ = new Core.MainCore();

            string jsonRAW = context.Request.Payload;
            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            string[] words = dataId?.idContactoService.ToString().Split(',');

            Core.DB.Models.RastreoDBModel data = new Core.DB.Models.RastreoDBModel();

            List<ObjectId> contactoServiceList = new List<ObjectId>();
            foreach (string word in words)
            {
                contactoServiceList.Add(ObjectId.Parse(word));
            }
            data.idContactoService = contactoServiceList;

            data.finalizado = dataId?.finalizado == "true" ? true : false;
            data.idTicketService = dataId?.idTicketService;
            data.keyWord = dataId?.keyWord;

            _.Create(data);


            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/rastreo/addLink")]
        public IHttpContext AddLink(IHttpContext context)
        {
            var id = context.Request.QueryString["id"] ?? "what?";
            Core.MainCore _ = new Core.MainCore();
            string jsonRAW = context.Request.Payload;
            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);
            Core.DB.Models.Link linkes = new Core.DB.Models.Link();

            linkes.url = dataId?.url; ;
            linkes.comentario = dataId?.comentario; ;
            linkes.categoria = dataId?.categoria; ;
            linkes.status = dataId?.status; ;
            linkes.originalPDF = dataId?.originalPDF; ;
            linkes.finalPDF = dataId?.finalPDF; ;
            linkes._id = ObjectId.GenerateNewId();
            linkes.impacto = dataId?.impacto; ;
            linkes.idioma = dataId?.idioma;

            _.CreateToArray(linkes, id);


            string json = JsonConvert.SerializeObject(linkes, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        #endregion

        #region PUT

        [RestRoute(HttpMethod = HttpMethod.PUT, PathInfo = "/api/rastreo/update")]
        public IHttpContext UpdateContacto(IHttpContext context)
        {

            Core.MainCore _ = new Core.MainCore();
            var id = context.Request.QueryString["id"] ?? "what?"; //Si no id dara error
                                                                   //var data = _.ReadId(id);

            var name = context.Request.QueryString["name"] ?? "what?";
            var valor = context.Request.QueryString["value"] ?? "what?";

            Core.DB.Models.RastreoDBModel obj = new Core.DB.Models.RastreoDBModel();
            var existeMetodo = obj.GetType().GetProperty(name) == null ? false : true;
            if (existeMetodo == true)
            {
                _.Update(id, name, valor);

                context.Response.SendResponse("Updated!");
                return context;
            }

            context.Response.SendResponse("Error!");
            return context;

        }


        #endregion

        #region DELETE

        [RestRoute(HttpMethod = HttpMethod.DELETE, PathInfo = "/api/rastreo/delete")]
        public IHttpContext DeleteContacto(IHttpContext context)
        {
            Core.MainCore _ = new Core.MainCore();

            var id = context.Request.QueryString["id"] ?? "what?"; //Si no id dara error

            _.DeleteById(id);

            context.Response.SendResponse("Deleted!");
            return context;
        }


        #endregion

        [RestRoute]
        public IHttpContext Default(IHttpContext context)
        {
            context.Response.SendResponse("RastreoService.");
            return context;
        }
    }

}
