using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Arendi.DataModel;
namespace Arendi.Controllers
{
    public static class CommentController
    {
        private static readonly HttpClient CommentControllerClient = new HttpClient
        {
            BaseAddress = new Uri(App.BaseAddress)
        };

        public static async Task<List<Comment>> GetCommentsByUserId(int id)
        {
            string requestString = "getcommentsbyuserid?id=" + id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(requestResult);
            return comments;
        }

        public static async Task<List<Comment>> GetCommentsByIdeaId(int id)
        {
            string requestString = "getcommentsbyideaid?id=" + id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(requestResult);
            return comments;
        }

        public static async Task<bool> AddComment
            (string content, string date, int idea_id, int user_id)
        {
            string requestString = "addcomment?content=" + content + "&date=" + 
                date + "&idea_id=" + idea_id.ToString() + "&user_id=" + user_id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            bool isAdded = JsonConvert.DeserializeObject<bool>(requestResult);
            return isAdded;
        }

        public static async Task<int> GetCommentId
            (string content, string date, int idea_id, int user_id)
        {
            string requestString = "getcommentid?content=" + content + "&date=" + date + "&idea_id=" 
                + idea_id.ToString() + "&user_id=" + user_id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            int id = JsonConvert.DeserializeObject<int>(requestResult);
            return id;
        }

        public static async Task<bool> DeleteCommentById(int id)
        {
            string requestString = "deletecommentbyid?id=" + id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            bool isDeleted = JsonConvert.DeserializeObject<bool>(requestResult);
            return isDeleted;
        }

        public static async Task<bool> UpdateComment
            (int old_id, string content, string date, int idea_id, int user_id)
        {
            string requestString = "updatecomment?old_id=" + old_id.ToString() + 
                "&content=" + content + "&date=" + date + "&idea_id=" + idea_id.ToString() 
                + "&user_id=" + user_id.ToString();
            string requestResult = await CommentControllerClient.GetStringAsync(requestString);
            bool isUpdated = JsonConvert.DeserializeObject<bool>(requestResult);
            return isUpdated;
        }

    }
}
