using dotnetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace dotnetWebAPI.Controllers
{
    public class UsersController : ApiController
    {   
        public HttpResponseMessage GetUsers()
        {
            using (UserDBContext db = new UserDBContext())
            {
                var users = db.users.ToList();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, users);
            }
        }
        public HttpResponseMessage GetUser(int id)
        {
            using (UserDBContext dBContext = new UserDBContext())
            {
                var user = dBContext.users.Find(id);
                if(user != null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, 
                    "User with ID "+id.ToString()+" not found");
            }
        }

        /*public user GetMatches(int id)
        {
            


        }*/
    }
}