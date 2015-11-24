using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Arendi.Service.Models;

namespace Arendi.Service.Controllers
{
    public class UsersController : ApiController
    {
        private ArendiDBEntities db = new ArendiDBEntities();

        [Route("get/userbymail")]
        [HttpGet]
        public IHttpActionResult GetUserByEmail(string email)
        {
            IQueryable<User> user;
            try
            {
                user = db.Users.Where(u => u.Email == email);
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("get/userbyid")]
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            IQueryable<User> user;
            try
            {
                user = db.Users.Where(u => u.ID == id);
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }

        // TODO: async?
        // TODO: Instead of getting old_user as parameter, get id or email and search for it
        [Route("get/updateuser")]
        [HttpGet]
        public bool UpdateUser(User old_user, User new_user)
        {
            User user;
            try
            {
                user = db.Users.Where(u => u == old_user).First();
                user = new_user;
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // TODO: Instead of getting user_to_delete as parameter, get id or email
        [Route("get/deleteuser")]
        [HttpGet]
        public bool DeleteUser(User user_to_delete)
        {
            User user;
            try
            {
                user = db.Users.Where(u => u == user_to_delete).First();
                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Route("get/adduser")]
        [HttpGet]
        public bool AddUser(User user_to_add)
        {
            User user;
            try
            {
                user = db.Users.Where(u => u == user_to_add).First();

                // If user_to_add exists, do not add again
                if (user != null)
                    return false;

                db.Users.Add(user_to_add);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}