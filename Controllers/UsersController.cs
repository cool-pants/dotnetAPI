using dotnetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace dotnetWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private UserDBContext dbContext = new UserDBContext();
        public HttpResponseMessage GetUsers()
        {
            var users = dbContext.users.ToList();
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, users);
        }
        public HttpResponseMessage GetUser(int id)
        {
            var user = dbContext.users.Find(id);
            if(user != null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, user);
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.NotFound); 
        }

        public HttpResponseMessage GetUsersWithinK(int user_id, int k)
        {
            user user = dbContext.users.Find(user_id);

            double distMax = (double)(user.location + k);
            double distMin = (double)(user.location - k);

            var users = dbContext.users.Where(u => (u.location <= distMax && u.location >= distMin && u.id != user_id)); 

            if (users != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No user found within range");
        }
        public HttpResponseMessage GetUsersQueryName(string name)
        {
            var users = dbContext.users.Where(u => u.name.Contains(name));

            if (users != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No user found within range");
        }
    }
}