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
    public class CommentsController : ApiController
    {
        private ArendiDBEntities db = new ArendiDBEntities();

        [Route("get/addcomment")]
        [HttpGet]
        public bool AddComment(string content, string date, int idea_id, int user_id)
        {
            Comment comment_to_add = new Comment
            {
                Content = content,
                Date = date,
                IdeaID = idea_id,
                UserID = user_id
            };
    
            try
            {
                db.Comments.Add(comment_to_add);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// AddComment

        [Route("get/deletecommentbyid")]
        [HttpGet]
        public bool DeleteCommentById(int id)
        {
            Comment comment;

            try
            {
                comment = db.Comments.Where(c => c.ID == id).First();
                db.Entry(comment).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Route("get/updatecomment")]
        [HttpGet]
        public bool UpdateComment(int old_id, string content, string date, int idea_id, int user_id)
        {
            Comment old_comment;
            Comment comment = new Comment
            {
                Content = content,
                Date = date,
                IdeaID = idea_id,
                UserID = user_id
            };

            try
            {
                old_comment = db.Comments.Where(c => c.ID == old_id).First();
                db.Entry(old_comment).Entity.Content = content;
                db.Entry(old_comment).Entity.Date = date;
                db.Entry(old_comment).Entity.IdeaID = idea_id;
                db.Entry(old_comment).Entity.UserID = user_id;
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