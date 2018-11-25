using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace carInsuranceQuote.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult Admin()
        {
            StreamReader sr = new StreamReader("C:\\Users\\Ryan\\source\\repos\\carInsuranceQuote\\carInsuranceQuote\\App_Data\\Quote.txt");
            string info = sr.ReadToEnd();
            sr.Close();

            ViewBag.AdminData = info;
            return View("Index");
        }

        [HttpPost]
        public ActionResult SignUp(DateTime DateOfBirth, string FirstName = "FirstName", string LastName = "LastName", string EmailAddress = "email@email.com", string CarMake="Make", string CarModel="Model", int CarYear=0, int Tickets=0, bool DUI = false, bool FullCoverage = false)
        {

            double price = 50;
            int age = 0;
            string holder;
            StreamWriter sw = new StreamWriter("C:\\Users\\Ryan\\source\\repos\\carInsuranceQuote\\carInsuranceQuote\\App_Data\\Quote.txt", true);
            // C:\Users\Ryan\source\repos\carInsuranceQuote\carInsuranceQuote\App_Data

            if (DateOfBirth.Month < DateTime.Now.Month)
            {
                age = (DateTime.Now.Year - DateOfBirth.Year) - 1;
            }
            else if (DateOfBirth.Month > DateTime.Now.Month)
            {
                age = (DateTime.Now.Year - DateOfBirth.Year);
            }
            else if (DateOfBirth.Month == DateTime.Now.Month)
            {
                if (DateOfBirth.Day >= DateTime.Now.Day)
                {
                    age = (DateTime.Now.Year - DateOfBirth.Year);
                }
                else if (DateOfBirth.Day < DateTime.Now.Day)
                {
                    age = (DateTime.Now.Year - DateOfBirth.Year) - 1;
                }
            }

            if( age < 25)
            {
                price += 25;
                if (age < 18)
                {
                    price += 100;
                }
            }
            if(age > 100)
            {
                price += 25;
            }

            if (CarYear < 2000 || CarYear > 2015)
            {
                price += 25;
            }

            if(CarMake == "Porche" || CarMake == "proche")
            {
                price += 25;
                if (CarModel == "911 Carrera" || CarModel == "911 carrera")
                {
                    price += 25;
                }
            }

            if (Tickets > 0)
            {
                price += (Tickets * 10);
            }

            if (DUI == true)
            {
                price *= 1.25;
            }
            if(FullCoverage == true)
            {
                price *= 1.50;
            }

            holder = Convert.ToString(price);


            sw.WriteLine("{0} / {1} / {2} / {3} ||",price, FirstName, LastName, EmailAddress);
            sw.Close();
            ViewBag.Quote = holder + " " + FirstName + " " + LastName +" "+ EmailAddress; 

            return View("Index");
        }
    }
}