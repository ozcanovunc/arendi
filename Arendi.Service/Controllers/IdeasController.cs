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
    public class IdeasController : ApiController
    {
        private ArendiDBEntities db = new ArendiDBEntities();

        [Route("get/addidea")]
        [HttpGet]
        public bool AddIdea(int user_id, string content, string date)
        {
            Idea idea_to_add = new Idea
            {
                UserID = user_id,
                Content = content,
                Date = date
            };

            try
            {
                db.Ideas.Add(idea_to_add);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// AddIdea

        [Route("get/getideaid")]
        [HttpGet]
        public int GetIdeaId(string content, string date, int user_id)
        {
            Idea idea;

            try
            {
                idea = db.Ideas.Where
                    (i => i.Content == content && 
                    i.Date == date && 
                    i.UserID == user_id).First();
                return idea.ID;
            }
            catch
            {
                return -1;
            }
        }// GetIdeaId


        [Route("get/deleteideabyid")]
        [HttpGet]
        public bool DeleteIdeaById(int id)
        {
            Idea idea;

            try
            {
                idea = db.Ideas.Where(i => i.ID == id).First();
                db.Entry(idea).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// DeleteIdeaById

        [Route("get/updateidea")]
        [HttpGet]
        public bool UpdateIdea(int old_id, string content, string date)
        {
            Idea old_idea;

            try
            {
                old_idea = db.Ideas.Where(i => i.ID == old_id).First();
                db.Entry(old_idea).Entity.Content = content;
                db.Entry(old_idea).Entity.Date = date;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// UpdateIdea
    }
}