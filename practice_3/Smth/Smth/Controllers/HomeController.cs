using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Smth.Models;

namespace Smth.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        HumanContext dbil = new HumanContext();
        public ActionResult Index()
        {
            //foreach (var k in dbil.Humans.AsEnumerable())
            //{
            //    dbil.Humans.Remove(k);
            //}
            //dbil.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Smt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Smt(Human hum)
        {
            dbil.Humans.Add(hum);
            dbil.SaveChanges();
            return View("table", dbil.Humans.AsEnumerable());
        }

        public ActionResult Deletic(string namee)
        {
            
            foreach (var k in dbil.Humans)
            {
                if (k.name == namee)
                {
                    dbil.Humans.Remove(k);
                }
            }
            ViewBag.Piu = namee;
            dbil.SaveChanges();
            return View();
        } 

        public ActionResult Print()
        {

            return View(dbil.Humans.AsEnumerable());
        }

        [HttpGet]
        public ActionResult Edit (string namee)
        {
            int buf = 0;
            ViewBag.Try = namee;
            Human test = new Human();
            foreach (var k in dbil.Humans)
            {
                if (k.name == namee)
                {
                    buf = -1; 
                    test = k;
                    dbil.Humans.Remove(k);
                    break;
                }
            }
            if (buf==0)
            {
                return View("Smt");
            }
            else
            {
                dbil.SaveChanges();
                return View(test);
            }
            
            
            
        }
        [HttpPost]
        public ActionResult Edit(Human hum)
        {
            dbil.Humans.Add(hum);
            dbil.SaveChanges();
            return View("Print", dbil.Humans.AsEnumerable());
        }
    }
}