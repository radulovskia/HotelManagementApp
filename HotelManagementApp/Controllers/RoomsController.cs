using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagementApp.Models;

namespace HotelManagementApp.Controllers
{
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rooms
        [AllowAnonymous]
        public ActionResult Index()
        {
            var rooms = db.Rooms.Include(r => r.Unit);
            return View(rooms.ToList());
        }

        //GET: AddToRoom
        [Authorize(Roles = "Staff")]
        public ActionResult AddToRoom(int id)
        {
            var model = new AddToRoomModel();
            model.RoomId = id;
            model.Guests = db.Guests.ToList();
            var room = db.Rooms.Find(id);
            ViewBag.RoomNumber = room.Number;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult AddToRoom(AddToRoomModel model)
        {
            var room = db.Rooms.Find(model.RoomId);
            var guest = db.Guests.Find(model.GuestId);
            room.Guest = guest;
            db.SaveChanges();
            return View("Index", db.Rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return View("CustomNotFound");
            }
            return View(room);
        }

        // GET: Rooms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Number,Price,RoomArtUrl,UnitId")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", room.UnitId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return View("CustomNotFound");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", room.UnitId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Number,Price,RoomArtUrl,UnitId")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnitId = new SelectList(db.Units, "UnitId", "Name", room.UnitId);
            return View(room);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
