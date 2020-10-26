using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public class LicencesController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: Licences
        public async Task<ActionResult> Index()
        {
           // ViewBag.userId = User.Identity.GetUserId();
            var licences = db.Licences;
            return View(await licences.ToListAsync());
        }

        public async Task<ActionResult> myBookings(string email)
        {
            // ViewBag.userId = User.Identity.GetUserId();
            var licences = db.Licences.Where(a => a.Email == email);
            return View(await licences.ToListAsync());
        }
        public async Task<ActionResult> AdminView()
        {
            // ViewBag.userId = User.Identity.GetUserId();
            var licences = db.Licences;
            return View(await licences.ToListAsync());
        }

        // GET: Licences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
            return View(licence);
        }

        // GET: Licences/Create
        public ActionResult Create()
        {
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name");
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Licences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LearnerID,UserID,Names,Email,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,BookingDate2,Photo,Picture,Code")] Licence licence, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    /// Mail ///                    
                    string subject = "Your booking for Code "+licence.Code+" Licence was successfull";

                    string message = "This message was sent to "+licence.Names+" ' " +licence.Email+ " ' \n " +
                        "If it is not yours just ignore or contact us on amocodes@gmail.com \n \n " +
                        "Hello you booked code "+licence.Code+" Licence. Your Sessions and Lessons will take place from "+licence.BookingDate+" untill "+licence.BookingDate2+"." +
                        "\n \n We will be happy to meet you in "+licence.Location+" at "+licence.BookingDate+"\n \n " +
                        "We hank you \n Lindani DS \n";

                    /////./Mail ///
                    string _FileName = Path.GetFileName(upload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    upload.SaveAs(_path);
                    licence.Picture = _FileName;
                    /// mail
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    MailAddress to = new MailAddress(licence.Email);
                    MailAddress from = new MailAddress("amocodes@gmail.com");

                    MailMessage mm = new MailMessage(from, to);
                    mm.Subject = subject;
                    mm.Body =  message;
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("amocodes@gmail.com", "@Dut991110");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = nc;
                    smtp.Send(mm);

                   // licence.BookingDate2 = licence.BookingDate;
                    db.Licences.Add(licence);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ThankBooking", "Home");
                }
                /// licence.LearnerID = Session[""]
                return View(licence);
            }

           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // GET: Licences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
           // ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // POST: Licences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LearnerID,Province,City,Surbub,Street,ZipCode,Phone,IDNum,Location,BookingDate,Photo,Picture,Code")] Licence licence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           // ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", licence.PackageID);
          //  ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", licence.UserID);
            return View(licence);
        }

        // GET: Licences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licence licence = await db.Licences.FindAsync(id);
            if (licence == null)
            {
                return HttpNotFound();
            }
            return View(licence);
        }

        // POST: Licences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Licence licence = await db.Licences.FindAsync(id);
            db.Licences.Remove(licence);
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
