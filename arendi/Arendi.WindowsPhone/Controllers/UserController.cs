using System;
using System.Net.Http;
using System.Threading.Tasks;
using Arendi.DataModel;
using Newtonsoft.Json;

namespace Arendi.Controllers
{
    public static class UserController
    {
        private static readonly HttpClient UserControllerClient = new HttpClient
        {
            BaseAddress = new Uri(App.BaseAddress)
        };

        public static async Task<User> GetUserByEmail(string email)
        {
            string requestString = "getuserbyemail?email=" + email;
            string requestResult = await UserControllerClient.GetStringAsync(requestString);
            User user = JsonConvert.DeserializeObject<User>(requestResult);
            return user;     
        }

        public static async Task<User> GetUserById(int id)
        {
            string requestString = "getuserbyid?id=" + id;
            string requestResult = await UserControllerClient.GetStringAsync(requestString);
            User user = JsonConvert.DeserializeObject<User>(requestResult);
            return user;
        }
        
        public static async Task<bool> AddUser(string username, string password, 
            string email, string type)
        {
            string requestString = "adduser?username=" + username + 
                "&password=" + password + "&email=" + email + "&type=" + type;
            string requestResult = await UserControllerClient.GetStringAsync(requestString);
            bool isAdded = JsonConvert.DeserializeObject<bool>(requestResult);
            return isAdded;
        }

        public static async Task<bool> DeleteUserByEmail(string email)
        {
            string requestString = "deleteuserbyemail?email=" + email;
            string requestResult = await UserControllerClient.GetStringAsync(requestString);
            bool isDeleted = JsonConvert.DeserializeObject<bool>(requestResult);
            return isDeleted;
        }

        public static async Task<bool> UpdateUserByEmail(string old_email, 
            string new_username, string new_password, string new_email, string new_type)
        {
            string requestString = "updateuserbyemail?old_email=" + old_email + 
                "&new_username=" + new_username + "&new_password=" + new_password + 
                "&new_email=" + new_email + "&new_type=" + new_type;
            string requestResult = await UserControllerClient.GetStringAsync(requestString);
            bool isUpdated = JsonConvert.DeserializeObject<bool>(requestResult);
            return isUpdated;
        }
    }
}
