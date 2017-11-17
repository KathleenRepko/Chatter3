using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chatter3.Models;
using Newtonsoft.Json;

namespace Chatter3.Controllers
{
    public class ChatsController : Controller
    {
        private Chatter3Entities db = new Chatter3Entities();
        private object select;

        // GET: Chats
        public ActionResult Index()
        {
            var chats = db.Chats.Include(c => c.AspNetUser);
            return View(chats.ToList());
        }

        public JsonResult TestJson()
        {
            //SELECT * FROM Chat

            var chats = from Chats in db.Chats
                        select new
                        {
                            ChatId = Chats.ChatId,
                            UserId = Chats.UserId,
                            ChatMessage = Chats.ChatMessage,
                            DateTimeStamp = Chats.DateTimeStamp
                        };

            //ChatsController.OrderByDescending(Chats.DateTimeStamp);

            //select new
            //{
            //    Chats.Message,
            //    Chats.AspNetUser.UserName
            //                OrderByDescending(DateTimeStamp)
            //};
            //var output = JsonConvert.SerializeObject(ChatsController.ToList());

            //return Json(output, JsonRequestBehavior.AllowGet);
            //var chats = from Chats in db.Chats
            //            orderby
            //              Chats.DateTimeStamp descending
            //            select new
            //            {
            //                Chats.AspNetUser.UserName,
            //                Chats.Message
            //            };

            //var output = JsonConvert.SerializeObject(chats.ToList());
            //return Json(output, JsonRequestBehavior.AllowGet);

            string jsonTest = "{\"firstName\":\"Bob\",\"lastName\":\"Sauce\",\"children\":[{\"firstName\":\"Barbie\",\"age\":19},{\"firstName\":\"Ron\",\"age\":null}]}";

                return Json(jsonTest, JsonRequestBehavior.AllowGet);
        }


        // GET: Chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // GET: Chats/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatId,UserId,ChatMessage,DateTimeStamp")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", chat.UserId);
            return View(chat);
        }

        // GET: Chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", chat.UserId);
            return View(chat);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatId,UserId,ChatMessage,DateTimeStamp")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", chat.UserId);
            return View(chat);
        }

        // GET: Chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chat chat = db.Chats.Find(id);
            db.Chats.Remove(chat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
