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
    public class UnitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Units
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Units.Include(r => r.City).ToList());
        }

        //GET: AddToUnit
        [Authorize(Roles = "Admin")]
        public ActionResult AddToUnit(int id)
        {
            var model = new AddToUnitModel();
            model.UnitId = id;
            model.Rooms = db.Rooms.ToList();
            var unit = db.Units.Find(id);
            ViewBag.UnitName = unit.Name;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddToUnit(AddToUnitModel model)
        {
            var unit = db.Units.Find(model.UnitId);
            var room = db.Rooms.Find(model.RoomId);
            var city = db.Cities.Find(unit.CityId);
            unit.Rooms.Add(room);
            city.Rooms.Add(room);
            db.SaveChanges();
            return View("Index", db.Units.ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return View("CustomNotFound");
            }
            return View(unit);
        }

        // GET: Units/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name");
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "UnitId,Name,Address,CityId")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", unit.CityId);
            return View(unit);
        }

        // GET: Units/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return View("CustomNotFound");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", unit.CityId);
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "UnitId,CityId,Name,Address")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", unit.CityId);
            return View(unit);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
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
