using Arendi.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arendi.Controllers
{
    public static class IdeaController
    {
        private static readonly HttpClient IdeaControllerClient = new HttpClient
        {
            BaseAddress = new Uri(App.BaseAddress)
        };

        public static async Task<List<Idea>> GetIdeas()
        {
            string requestString = "getideas";
            string requestResult = await IdeaControllerClient.GetStringAsync(requestString);
            List<Idea> ideas = JsonConvert.DeserializeObject<List<Idea>>(requestResult);
            return ideas;
        }

        public static async Task<bool> AddIdea(int user_id, string content, string date)
        {
            string requestString = "addidea?user_id=" + user_id.ToString() +  "&content=" 
                + content + "&date=" + date;
            string requestResult = await IdeaControllerClient.GetStringAsync(requestString);
            bool isAdded = JsonConvert.DeserializeObject<bool>(requestResult);
            return isAdded;
        }

        public static async Task<int> GetIdeaId(string content, string date, int user_id)
        {
            string requestString = "getideaid?content=" + content + "&date=" + date + 
                "&user_id=" + user_id;
            string requestResult = await IdeaControllerClient.GetStringAsync(requestString);
            int id = JsonConvert.DeserializeObject<int>(requestResult);
            return id;
        }

        public static async Task<bool> DeleteIdeaById(int id)
        {
            string requestString = "deleteideabyid?id=" + id;
            string requestResult = await IdeaControllerClient.GetStringAsync(requestString);
            bool isDeleted = JsonConvert.DeserializeObject<bool>(requestResult);
            return isDeleted;
        }

        public static async Task<bool> UpdateIdea(int old_id, string content, string date)
        {
            string requestString = "updateidea?old_id=" + old_id + "&content=" + content 
                + "&date=" + date;
            string requestResult = await IdeaControllerClient.GetStringAsync(requestString);
            bool isUpdated = JsonConvert.DeserializeObject<bool>(requestResult);
            return isUpdated;
        }
    }
}
