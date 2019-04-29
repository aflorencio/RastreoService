﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RastreoService.Core
{
    public class MainCore
    {
        #region CREATE

        public string CreateContacto(Core.DB.Models.RastreoDBModel data)
        {

            Core.DB.Query.RastreoQuery qCreate = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");

            //qCreate.Create(data); //El metodo antiguo que funciona
            qCreate.InsertUser(data);//Nuevo metodo
            return "OK!";
        }

        #endregion

        #region READ
        public List<Core.DB.Models.RastreoDBModel> ReadAll()
        {
            Core.DB.Query.RastreoQuery qReadAll = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");

            return qReadAll.GetAllUsers();//Nuevo metodo
        }

        public Core.DB.Models.RastreoDBModel ReadId(string id)
        {

            Core.DB.Query.RastreoQuery qReadId = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");

            var data = qReadId.GetUsersById(id);
            return data;
        }

        public List<Core.DB.Models.RastreoDBModel> ReadValue(string fieldName, string fieldValue)
        {
            Core.DB.Query.RastreoQuery qReadId = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");

            var data = qReadId.GetUsersByField(fieldName, fieldValue);
            return data;

        }
        #endregion

        #region UPDATE
        public void Update(string id, string updateFieldName, string updateFieldValue)
        {

            Core.DB.Query.RastreoQuery qUpdate = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");
            qUpdate.UpdateUser(id, updateFieldName, updateFieldValue);

        }

        #endregion

        #region DELETE

        public void Delete(string id)
        {
            Core.DB.Query.RastreoQuery qDelete = new Core.DB.Query.RastreoQuery("mongodb://51.83.73.69:27017");
            qDelete.DeleteUserById(id);
        }

        #endregion
    }
}