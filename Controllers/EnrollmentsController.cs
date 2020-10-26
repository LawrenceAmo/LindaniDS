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
    public class EnrollmentsController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: Enrollments
        public async Task<ActionResult> Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Learners);
            return View(await enrollments.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollments enrollments = await db.Enrollments.FindAsync(id);
            if (enrollments == null)
            {
                return HttpNotFound();
            }
            return View(enrollments);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.LearnerID = new SelectList(db.Learners, "LearnerID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollID,UserID,LearnerID")] Enrollments enrollments)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LearnerID = new SelectList(db.Learners, "LearnerID", "Name", enrollments.LearnerID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", enrollments.UserID);
            return View(enrollments);
        }

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollments enrollments = await db.Enrollments.FindAsync(id);
            if (enrollments == null)
            {
                return HttpNotFound();
            }
            ViewBag.LearnerID = new SelectList(db.Learners, "LearnerID", "Name", enrollments.LearnerID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", enrollments.UserID);
            return View(enrollments);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollID,UserID,LearnerID")] Enrollments enrollments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LearnerID = new SelectList(db.Learners, "LearnerID", "Name", enrollments.LearnerID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", enrollments.UserID);
            return View(enrollments);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollments enrollments = await db.Enrollments.FindAsync(id);
            if (enrollments == null)
            {
                return HttpNotFound();
            }
            return View(enrollments);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollments enrollments = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollments);
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
