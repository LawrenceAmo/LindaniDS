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
    public class BookingPackagesController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: BookingPackages
        public async Task<ActionResult> Index()
        {
            return View(await db.BookingPackages.ToListAsync());
        }

        // GET: BookingPackages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingPackages bookingPackages = await db.BookingPackages.FindAsync(id);
            if (bookingPackages == null)
            {
                return HttpNotFound();
            }
            return View(bookingPackages);
        }

        // GET: BookingPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "packageID,packageName,cost,availabilityDate")] BookingPackages bookingPackages)
        {
            if (ModelState.IsValid)
            {
                db.BookingPackages.Add(bookingPackages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bookingPackages);
        }

        // GET: BookingPackages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingPackages bookingPackages = await db.BookingPackages.FindAsync(id);
            if (bookingPackages == null)
            {
                return HttpNotFound();
            }
            return View(bookingPackages);
        }

        // POST: BookingPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "packageID,packageName,cost,availabilityDate")] BookingPackages bookingPackages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingPackages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookingPackages);
        }

        // GET: BookingPackages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingPackages bookingPackages = await db.BookingPackages.FindAsync(id);
            if (bookingPackages == null)
            {
                return HttpNotFound();
            }
            return View(bookingPackages);
        }

        // POST: BookingPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BookingPackages bookingPackages = await db.BookingPackages.FindAsync(id);
            db.BookingPackages.Remove(bookingPackages);
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
