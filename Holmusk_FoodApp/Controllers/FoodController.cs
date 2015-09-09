using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Holmusk_FoodApp.Models;

namespace Holmusk_FoodApp.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Food
        public ActionResult Index()
        {
            return View(db.Food.ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Search(string q)
        {
            var result = new List<KeyValuePair<string, string>>();
            var foodlist = db.Food.Where(m => m.FoodName.Contains(q)).ToList().OrderBy(y=>y.FoodName);

            foreach (var item in foodlist)
            {
                FoodUnit myunit = (FoodUnit)item.Unit;
                var foodname = item.FoodName + "(" + myunit + ")";
                result.Add(new KeyValuePair<string, string>(item.Foodid.ToString(), foodname));
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Food/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Foodid,FoodName,Description,Unit,Cholesterol,Vitamins,Energy,Sugar,Protein,Calcium")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Food.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Foodid,FoodName,Description,Unit,Cholesterol,Vitamins,Energy,Sugar,Protein,Calcium")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Food/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Food.Find(id);
            db.Food.Remove(food);
            db.SaveChanges();
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
