using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using lindaniDS.Models;
using Microsoft.AspNet.Identity;

namespace lindaniDS.Controllers
{
    public class PaymentsController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: Payments
        public ActionResult Index(int? id)
        {
            var payments = db.Payments.Include(p => p.BookingPackages).Include(p => p.VehicleHire);
            var vehicleHires = db.VehicleHires.Find(id);
            ViewBag.Vehicle = db.VehicleHires.Where(s => s.vehicleID == id);
          //  ViewBag.test = vehicleHires.Email;
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        public ActionResult AddPay(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicleHires = db.VehicleHires.Find(id);
            Session["Vehicle"] = vehicleHires;
            //  Session["Vehicle"].Clear();
            if (vehicleHires == null)
            {
                return HttpNotFound();
            }
            //return View(vehicleHires);

             return RedirectToAction("Create", "Payments");

        }
        //////////////////////////////////////////
        public ActionResult VehiclePay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire vehicleHire =  db.VehicleHires.Find(id);
            if (vehicleHire == null)
            {
                return HttpNotFound();
            }
            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName", vehicleHire.modelID);
            return View(vehicleHire);
        }
        ////////        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VehiclePay([Bind(Include = "vehicleID,modelID,Email,color,regNo,noPlate,cost,condition,availability")] VehicleHire vehicleHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleHire).State = EntityState.Modified;
                 db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName", vehicleHire.modelID);
            return View(vehicleHire);
        }
        /// <summary>
        /// /////////////////////////////////     learner Pay
        /// 
        public ActionResult LearnerPay(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Learners payment = db.Learners.Find(id);
            ViewBag.Learner = payment;
            ViewBag.User = User.Identity.Name;
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.LearnerID = new SelectList(db.Learners, "LearnerID", "Name", payment.LearnerID);

            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //    [Bind(Include = "PaymentID,PayType,UserID,vehicleID,PackageID,Date,BankName,BranchCode,CardNumber,Expire,CVV")]
        public ActionResult LearnerPay( Payment payment, Enrollments enrollments)
        {
           
            if (ModelState.IsValid)
            {
                payment.UserID = User.Identity.Name;
                payment.LearnerID = (int)payment.LearnerID;
                enrollments.UserID = User.Identity.Name;
                enrollments.LearnerID = (int)payment.LearnerID;

                db.Enrollments.Add(enrollments);
                db.Payments.Add(payment);
                db.SaveChanges();

                return RedirectToAction("ThankHire", "Home");
            }

            return RedirectToAction("LearnerPay", "Payments");

        }




        /// <summary>
        /// ///////////////////////////////////////////////// learners
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(int? id)
        {
            Session.Timeout = 60;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire payment = db.VehicleHires.Find(id);
            ViewBag.Vehicle = payment;
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.vehicleID = new SelectList(db.VehicleHires, "vehicleID", "Email", payment.vehicleID);
           
            return View(); 
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,PayType,UserID,vehicleID,PackageID,Date,BankName,BranchCode,CardNumber,Expire,CVV")] Payment payment, VehicleHire VehicleHire, int id)
        {
            Session.Timeout = 60;
            var amo = db.VehicleHires.Find(id);
            if (ModelState.IsValid)
            {
                // amo.Photo
                payment.UserID = User.Identity.Name;
                payment.vehicleID = amo.vehicleID;
                VehicleHire.vehicleID = amo.vehicleID;
                VehicleHire.modelID = amo.modelID;
                amo.availability = true;
                db.Payments.Add(payment);
                db.SaveChanges();
                /////////////////////////////////////////

                //string subject = "Hello " + payment.UserID + " Thank you for Hiring" + amo.VehicleModel.vehicleName + "" + amo.VehicleModel.vehicleMake + "" + amo.VehicleModel.modelName + ". ";

                //string message = "This message was sent to " + payment.PayType + " \n " +
                //    "If it is not yours just ignore or contact us on amocodes@gmail.com \n \n " +
                //    "Hello you've hired " + amo.VehicleModel.vehicleName + "" + amo.VehicleModel.vehicleMake + "" + amo.VehicleModel.modelName + "" +
                //    " This vehicle cost is " + amo.cost + " \n You can come and coolect your Vehicle anytime. For more info please contact us on amocodes@gmail.com \n \n " +
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

                //NetworkCredential nc = new NetworkCredential("amocodes@gmail.com", "@Dut123456");
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = nc;
                //smtp.Send(mm);

            }

            return RedirectToAction("ThankHire", "Home");
                
            }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", payment.PackageID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", payment.UserID);
            ViewBag.vehicleID = new SelectList(db.VehicleHires, "vehicleID", "Email", payment.vehicleID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,PayType,UserID,vehicleID,PackageID,Date,BankName,BranchCode,CardNumber,Expire,CVV")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.BookingPackages, "PackageID", "Name", payment.PackageID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FirstName", payment.UserID);
            ViewBag.vehicleID = new SelectList(db.VehicleHires, "vehicleID", "Email", payment.vehicleID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

/*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
