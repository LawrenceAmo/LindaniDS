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
using System.IO;


namespace lindaniDS.Controllers
{
    public class VehicleHiresController : Controller
    {
        private LindaniContext db = new LindaniContext();

        // GET: VehicleHires
        public async Task<ActionResult> Index()
        {

            //if (Session["Email"] == null)
            //{
            //    return RedirectToAction("Home", "Home");
            //}
            //else
            //{
                var vehicleHires = db.VehicleHires.Include(v => v.VehicleModel);
                ViewBag.hired = db.VehicleHires.Where(s => s.availability).Count();
                ViewBag.dCar = db.VehicleHires.Where(a => a.condition).Count();
                ViewBag.totCar = db.VehicleHires.Count();

                return View(await vehicleHires.ToListAsync());
          //  }
            
        }
        ////////
        public async Task<ActionResult> Hire(int? id)        {

                var vehicleHires = db.VehicleHires.Include(v => v.VehicleModel).Include(a => a.Address);


            //VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            //ViewBag.Vehicle = vehicleHire;

            return View(await vehicleHires.ToListAsync());
            

        }

        // GET: VehicleHires/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            if (vehicleHire == null)
            {
                return HttpNotFound();
            }
            return View(vehicleHire);
        }

        public async Task<ActionResult> ViewCar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            if (vehicleHire == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vehicle = vehicleHire;
            return View(vehicleHire);
        }

        // GET: VehicleHires/Create
        public ActionResult Create()
        {
            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName");
            return View();
        }

        // POST: VehicleHires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "vehicleID,modelID,Email,color,regNo,noPlate,cost,condition,availability,Picture,Photo")] VehicleHire vehicleHire, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //if (upload != null && upload.ContentLength > 0)
                //{
                //    int fileLength = upload.ContentLength;
                //    Byte[] array = new Byte[fileLength];
                //    upload.InputStream.Read(array, 0, fileLength);
                //    vehicleHire.Picture = array;
                //    vehicleHire.availability = vehicleHire.car();        
                //    upload.SaveAs(HttpContext.Server.MapPath("~/Images/")
                //                                              + upload.FileName);
                //            string name = upload.FileName;
                //            vehicleHire.Photo = name;  
                //}

                if (upload != null && upload.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(upload.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    upload.SaveAs(_path);

                    vehicleHire.Photo = _FileName;
                    
                }

                db.VehicleHires.Add(vehicleHire);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName", vehicleHire.modelID);
            return View(vehicleHire);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            if (vehicleHire == null)
            {
                return HttpNotFound();
            }
            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName", vehicleHire.modelID);
            return View(vehicleHire);
        }

        // POST: VehicleHires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "vehicleID,modelID,Email,color,regNo,noPlate,cost,condition,availability")] VehicleHire vehicleHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleHire).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.modelID = new SelectList(db.VehicleModels, "modelID", "vehicleName", vehicleHire.modelID);
            return View(vehicleHire);
        }

        // GET: VehicleHires/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            if (vehicleHire == null)
            {
                return HttpNotFound();
            }
            return View(vehicleHire);
        }

        // POST: VehicleHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleHire vehicleHire = await db.VehicleHires.FindAsync(id);
            db.VehicleHires.Remove(vehicleHire);
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
