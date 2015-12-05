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

        [Route("get/getcommentsbyideaid")]
        [HttpGet]
        public List<Comment> GetCommentsByIdeaId(int id)
        {
            var comments = db.Comments;
            var comments_by_idea_id = new List<Comment>();

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    if (comment.IdeaID == id)
                        comments_by_idea_id.Add(comment);
                }
                return comments_by_idea_id;
            }
            else
            {
                return null;
            }
        }// GetCommentsByIdeaId

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

        [Route("get/getcommentid")]
        [HttpGet]
        public int GetCommentId(string content, string date, int idea_id, int user_id)
        {
            Comment comment;

            try
            {
                comment = db.Comments.Where
                    (c => c.Content == content && 
                    c.Date == date && 
                    c.IdeaID == idea_id && 
                    c.UserID == user_id).First();
                return comment.ID;
            }
            catch
            {
                return -1;
            }
        }// GetCommentId

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
        }// DeleteCommentById

        [Route("get/updatecomment")]
        [HttpGet]
        public bool UpdateComment(int old_id, string content, string date, int idea_id, int user_id)
        {
            Comment old_comment;

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
        }// UpdateComment
    }
}