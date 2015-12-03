using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Arendi.Service.Models;

namespace Arendi.Service.Controllers
{
    public class UsersController : ApiController
    {
        private ArendiDBEntities db = new ArendiDBEntities();

        // TODO: NotFound stuff
        [Route("get/getuserbyemail")]
        [HttpGet]
        public IHttpActionResult GetUserByEmail(string email)
        {
            User user;

            try
            {
                user = db.Users.Where(u => u.Email == email).First();
                return Ok(user);
            }
            catch
            {
                return Ok();
                //return NotFound();
            }
        }// GetUserByEmail

        // TODO: NotFound stuff
        [Route("get/getuserbyid")]
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            User user;

            try
            {
                user = db.Users.Where(u => u.ID == id).First();
                return Ok(user);
            }
            catch
            {
                return Ok();
                //return NotFound();
            }
        }// GetUserById

        // TODO: async, performance issues
        [Route("get/updateuserbyemail")]
        [HttpGet]
        public bool UpdateUserByEmail(string old_email, string new_username, 
            string new_password, string new_email, string new_type)
        {
            User old_user;
            User new_user = new User
            {
                Username = new_username,
                Password = new_password,
                Email = new_email,
                Type = new_type
            };

            try
            {
                old_user = db.Users.Where(u => u.Email == old_email).First();
                db.Entry(old_user).Entity.Username = new_username;
                db.Entry(old_user).Entity.Password = new_password;
                db.Entry(old_user).Entity.Email = new_email;
                db.Entry(old_user).Entity.Type = new_type;
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }// UpdateUser

        [Route("get/deleteuserbyemail")]
        [HttpGet]
        public bool DeleteUserByEmail(string email)
        {
            User user;

            try
            {
                user = db.Users.Where(u => u.Email == email).First();
                db.Entry(user).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// DeleteUser

        [Route("get/adduser")]
        [HttpGet]
        public bool AddUser(string username, string password, string email, string type)
        {
            User user_to_add = new User
            {
                Username = username,
                Password = password,
                Email = email,
                Type = type
            };

            try
            {
                // If user_to_add exists, do not add again
                if (db.Users.Where(u => u.Email == email).Count() != 0)
                    return false;

                db.Users.Add(user_to_add);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            /*
            catch(System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                string error = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        error += validationError.PropertyName.ToString() 
                        + " " + validationError.ErrorMessage.ToString() + "\n";
                    }
                }
                return error;
            }
            */

        }// AddUser
    }
}