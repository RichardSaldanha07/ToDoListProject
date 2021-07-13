using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoListProject.Models;

namespace ToDoListProject.Controllers
{
    public class ToDoesController : Controller
    {
        // Reference to our Database
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        // Index() method -> returns a view of Db ToDoList, it list all of the ToDos and return it as a view
        
        public ActionResult Index()
        {
            return View();
        }

        // Creating a method for getting the List of ToDos for the user currently in the web application
        private IEnumerable<ToDo> GetMyToDoes()
        {
            // Viewing the data based on the current users Identity
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            IEnumerable<ToDo> myToDoes = db.ToDos.ToList().Where(x => x.User == applicationUser);

            int ToDoCompleteCount = 0;
            foreach (ToDo toDo in myToDoes)
            {
                if (toDo.IsCompleted)
                {
                    ToDoCompleteCount++;
                }
            }
            ViewBag.ProgressPercent = Math.Round(100f * ((float)ToDoCompleteCount / (float)myToDoes.Count()));

            return myToDoes;
        }

        // Action Method for the table
        public ActionResult BuildToDoTable()
        {
            return PartialView("_MyToDoTable", GetMyToDoes()); // returning view where the user object is equal to the current user only.
        }

        // GET: ToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }

            return View(toDo);
        }

        // GET: ToDoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,IsCompleted,ToDoDate")] ToDo toDo)
        {
            // If Model is valid then add data to the db context
            if (ModelState.IsValid)
            {
                // Adding the data based on the current users Identity
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentApplicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                toDo.User = currentApplicationUser;
                db.ToDos.Add(toDo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDo);
        }

        // Creating an Ajax method which will be used for adding the to do list based on the user logged in to the application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreatedToDo([Bind(Include = "Id,Description,ToDoDate")] ToDo toDo)
        {
            // If Model is valid then add data to the db context
            if (ModelState.IsValid)
            {
                // Adding the data based on the current users Identity
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentApplicationUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                toDo.User = currentApplicationUser;
                toDo.IsCompleted = false;
                db.ToDos.Add(toDo);
                db.SaveChanges();
            }

            return PartialView("_MyToDoTable", GetMyToDoes());
        }
        // GET: ToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (toDo.User != applicationUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,IsCompleted,ToDoDate")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }

 // Creating an Ajax method which will be used for edditing the to do list based on the user logged in to the application
        [HttpPost]
        public ActionResult AJAXEditedToDo(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            else
            {
                toDo.IsCompleted = value;
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_MyToDoTable", GetMyToDoes());
            }
        }


        // GET: ToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);
            if (toDo == null)
            {
                return HttpNotFound();
            }
            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo toDo = db.ToDos.Find(id);
            db.ToDos.Remove(toDo);
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
