using dotnetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnetWebAPI.Controllers
{
    public class LikesController : ApiController
    {
        private LikeDBContext likeContext = new LikeDBContext();
        private UserDBContext userContext = new UserDBContext();
        
        public HttpResponseMessage GetLikes()
        {
            var likes = likeContext.likes.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, likes);
        }

        public HttpResponseMessage GetLike(int id)
        {
            var entity = likeContext.likes.Find(id);
            if(entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No like entry with ID = "+id.ToString());
        }

        public HttpResponseMessage GetLikesByUser(int user_id)
        {
            var users = (from a in likeContext.likes
                        from b in likeContext.likes
                        from u in userContext.users
                        where a.liker == b.likee
                        where a.likee == b.likee
                        where a.likee == user_id
                        where u.id == b.likee
                        select u);
            if(users != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User has not liked any user");
        }
    }
}
