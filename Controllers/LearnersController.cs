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
using System.Net.Mail;

namespace lindaniDS.Controllers
{
    public class LearnersController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: Learners
        public async Task<ActionResult> Index()
        {
            var learners = db.Learners;
            return View(await learners.ToListAsync());
        }
        public async Task<ActionResult> myBookings(string email)
        {
            // ViewBag.userId = User.Identity.GetUserId();
            var learners = db.Licences.Where(a => a.Email == email);
            return View(await learners.ToListAsync());
        }
        public async Task<ActionResult> AdminView()
        {
            var learners = db.Learners;
            return View(await learners.ToListAsync());
        }

        // GET: Learners/Details/5
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

        // GET: Learners/Create
        public ActionResult Create()
        {
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name");
          //  ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Learners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearnerID,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,Session,Names,Email,")] Learners learners)
        {
            if (ModelState.IsValid)
            {
                //string subject = "Your booking for Learners was successfull";

                //string message = "This message was sent to " + User.Identity.Name + " ' " + learners.Phone + " ' \n " +
                //    "If it is not yours just ignore or contact us on amocodes@gmail.com \n \n " +
                //    "Hello you booked for Learners. Your Sessions and Lessons will take place from " + learners.BookingDate + " " +
                //    "\n \n We will be happy to meet you in " + learners.Location + " at " + learners.BookingDate + "\n \n " +
                //    "We hank you \n Lindani DS \n";

                ///////./Mail ///
       
                ///// mail
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //MailAddress to = new MailAddress(User.Identity.Name);
                //MailAddress from = new MailAddress("amocodes@gmail.com");

                //MailMessage mm = new MailMessage(from, to);
                //mm.Subject = subject;
                //mm.Body = message;
                //mm.IsBodyHtml = false;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;

                //NetworkCredential nc = new NetworkCredential("amocodes@gmail.com", "@Dut991110");
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = nc;
                //smtp.Send(mm);
              //  learners.Email = User.Identity.Name;
                db.Learners.Add(learners);
                await db.SaveChangesAsync();
                return RedirectToAction("ThankBooking","Home");
            }

           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", learners.PackageID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", learners.UserID);
            return View(learners);
        }

        // GET: Learners/Edit/5
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
          //  ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", learners.PackageID);
          //  ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", learners.UserID);
            return View(learners);
        }

        // POST: Learners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearnerID,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,Session")] Learners learners)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learners).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
          //  ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", learners.PackageID);
          //  ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", learners.UserID);
            return View(learners);
        }

        // GET: Learners/Delete/5
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

        // POST: Learners/Delete/5
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
