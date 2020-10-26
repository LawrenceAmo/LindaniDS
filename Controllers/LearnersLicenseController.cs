using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lindaniDS.Models;

namespace lindaniDS.Controllers
{
    public class LearnersLicenseController : Controller
    {
        private LindaniContext db = new LindaniContext();  //      ViewBag.dCar = db.VehicleHires.Where(a => a.condition).Count();

        // GET: LearnersLicense
        public async Task<ActionResult> Index()
        {
            return View(await db.Learners.ToListAsync());
        }
        public async Task<ActionResult> LearnersCodes()
        {
            return View(await db.Learners.ToListAsync());
        }


        public async Task<ActionResult> LearnerPortal()
        {
            var user = User.Identity.Name;
            var enrol = await db.Enrollments.Where(a => a.UserID == user).ToListAsync();
            ViewBag.enrol = enrol;
            return View();
        }



        // GET: LearnersLicense/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Learners learners = await db.Learners.FindAsync(id);
            if (learners == null)
            {
                return HttpNotFound();
            }
            return View(learners);
        }

        // GET: LearnersLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LearnersLicense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearnerID,Name,LearnerDisc,Picture,Photo,Code")] Learners learners)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learners);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(learners);
        }

        // GET: LearnersLicense/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Learners learners = await db.Learners.FindAsync(id);
            if (learners == null)
            {
                return HttpNotFound();
            }
            return View(learners);
        }

        // POST: LearnersLicense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearnerID,Name,LearnerDisc,Picture,Photo,Code")] Learners learners)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learners).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(learners);
        }

        // GET: LearnersLicense/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Learners learners = await db.Learners.FindAsync(id);
            if (learners == null)
            {
                return HttpNotFound();
            }
            return View(learners);
        }

        // POST: LearnersLicense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Learners learners = await db.Learners.FindAsync(id);
            db.Learners.Remove(learners);
            await db.SaveChangesAsync();
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
