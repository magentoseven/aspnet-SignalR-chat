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
using ChatHub.DAL;
using ChatHub.Models;
using Microsoft.AspNet.SignalR;
using ChatHub.signalr.hubs;

namespace ChatHub.Controllers
{
    public class ChatController : ApiController
    {
        private ChatContext db = new ChatContext();

        // GET: api/Chat
        public IQueryable<Chat> GetChats()
        {
            return db.Chats;
        }

        // GET: api/Chat/5
        [ResponseType(typeof(Chat))]
        public IHttpActionResult GetChat(int id)
        {
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        // PUT: api/Chat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChat(int id, Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chat.Id)
            {
                return BadRequest();
            }

            db.Entry(chat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Chat
        [ResponseType(typeof(Chat))]
        public IHttpActionResult PostChat(Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chats.Add(chat);
            db.SaveChanges();

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<signalr.hubs.ChatHub>();
            hubContext.Clients.All.getChatLog();

            return CreatedAtRoute("DefaultApi", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chat/5
        [ResponseType(typeof(Chat))]
        public IHttpActionResult DeleteChat(int id)
        {
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return NotFound();
            }

            db.Chats.Remove(chat);
            db.SaveChanges();

            return Ok(chat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatExists(int id)
        {
            return db.Chats.Count(e => e.Id == id) > 0;
        }
    }
}