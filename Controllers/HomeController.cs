using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using lindaniDS.Models;

namespace lindaniDS.Controllers
{
    public class HomeController : Controller
        {
            private LindaniContext db = new LindaniContext();
            public ActionResult Index()
            {
                return View();
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }

            [HttpPost]
            public ActionResult Contact(string name, string email, string subject, string message)
            {

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                MailAddress to = new MailAddress("amocodes@gmail.com");
                MailAddress from = new MailAddress(email);

                MailMessage mm = new MailMessage(from, to);
                //MailMessage mm = new MailMessage(email, "amocodes@gmail.com");
                mm.Subject = subject;
                mm.Body = "Hello my Name is: ( " + name + " ) and my Email is: ( " + email + " ) \n \n MESSAGE: \n \n " + message;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("amocodes@gmail.com", "@Dut123456");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Send(mm);

                ViewBag.Error = "Your message was Sent successfully \n We will get in touch with you as soon as we get this email.\n We thank you.";
                return View();
            }
            /// <summary>
            /// ////////////////////////////////////
            /// </summary>
            /// <returns></returns>
            public ActionResult AllBookings()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }

        public ActionResult ThankHire()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ThankBooking()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
            {
                return View();
            }


            [HttpPost]
            public ActionResult Register(User User)
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(User);
                    db.SaveChanges();
                }
                return RedirectToAction("Login", "Home");
            }

            public ActionResult Login()
            {
                if (Session["Email"] != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }

            [HttpPost]
            public ActionResult Login(User User)
            {
                var UserLogedIn = db.Users.SingleOrDefault(x => x.Email == User.Email && x.Password == User.Password);
                if (UserLogedIn != null)
                {
                    string email = User.Email;
                    ViewBag.err = "The E-mail or Password are incorrect, Please try again";
                    ViewBag.success = "You are successfully Loged In.";
                    Session["Email"] = email;
                    Session["UserID"] = User.UserID;

                    if (email.Contains("lindani") || email.Contains("lindaniDS") || email.Contains("lindanids"))
                    {
                        Session["ad"] = email;
                        return RedirectToAction("Index", "VehicleHires");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }



                }
                else
                {
                    ViewBag.tryOnce = "yes";
                    return View();
                }
            }

            public ActionResult Logout()
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
        }
    }