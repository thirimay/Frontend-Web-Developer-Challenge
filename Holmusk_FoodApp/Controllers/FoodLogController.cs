using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Holmusk_FoodApp.Models;

namespace Holmusk_FoodApp.Controllers
{
    [Authorize]
    public class FoodLogController : Controller
    {
        private MyDBContext db = new MyDBContext();
        int month;
        // GET: FoodLog
        public ActionResult Index(string duration, string searchtext)
        {
            if (!string.IsNullOrEmpty(duration) && duration != "0")
            {
                month = DateTime.Now.AddMonths(-Math.Abs(Convert.ToInt32(duration)) + 1).Month;
            }

            var username = User.Identity.Name;

            if (!string.IsNullOrEmpty(searchtext))
            {
                var foodlog = from x in db.FoodLog
                              where x.UserId == username && x.LogDate.Month >= month
                              join y in db.Food on x.FoodId equals y.Foodid
                              where y.FoodName.Contains(searchtext)
                              select new FoodLogViewModel
                              {
                                  FoodLogId = x.FoodLogId,
                                  FoodName = y.FoodName,
                                  UserId = x.UserId,
                                  FoodId = x.FoodId,
                                  Quantity = x.Quantity,
                                  Unit = ((FoodUnit)y.Unit).ToString(),
                                  LogDate = x.LogDate,
                              };
                return View(foodlog.ToList());
            }

            if (duration != "0")
            {
                var foodlog2 = from x in db.FoodLog
                               where x.UserId == username && x.LogDate.Month >= month
                               join y in db.Food on x.FoodId equals y.Foodid
                               select new FoodLogViewModel
                               {
                                   FoodLogId = x.FoodLogId,
                                   FoodName = y.FoodName,
                                   UserId = x.UserId,
                                   FoodId = x.FoodId,
                                   Quantity = x.Quantity,
                                   Unit = ((FoodUnit)y.Unit).ToString(),
                                   LogDate = x.LogDate,
                               };
                return View(foodlog2.ToList());
            }

            var foodlog3 = from x in db.FoodLog
                           where x.UserId == username
                           join y in db.Food on x.FoodId equals y.Foodid
                           select new FoodLogViewModel
                           {
                               FoodLogId = x.FoodLogId,
                               FoodName = y.FoodName,
                               UserId = x.UserId,
                               FoodId = x.FoodId,
                               Quantity = x.Quantity,
                               Unit = ((FoodUnit)y.Unit).ToString(),
                               LogDate = x.LogDate,
                           };
            return View(foodlog3.ToList());
        }

        public ActionResult Log()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Log([Bind(Include = "FoodName,FoodId,Quantity,LogDate")] FoodLogViewModel foodlog)
        {
            
            if (ModelState.IsValid)
            {
                if (foodlog.FoodId == 0)
                {
                    Food myfood = new Food();
                    myfood.FoodName = foodlog.FoodName;
                    myfood.Unit = FoodUnit.g;
                    db.Food.Add(myfood);
                    db.SaveChanges();
                    foodlog.FoodId = myfood.Foodid;
                }

                FoodLog myfoodlog = new FoodLog();
                myfoodlog.FoodId = foodlog.FoodId;
                myfoodlog.Quantity = foodlog.Quantity;
                myfoodlog.LogDate = foodlog.LogDate;
                myfoodlog.UserId = User.Identity.Name;
                db.FoodLog.Add(myfoodlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodlog);
        }
    }
}