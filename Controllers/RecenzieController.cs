using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class RecenzieController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: Recenzie
        public ActionResult Index(string searchString)
        {
            var recenzii = from m in db.Recenzii.Include("Film")
                         select m;
            recenzii = recenzii.OrderByDescending(s => s.Nota);

            if (!String.IsNullOrEmpty(searchString))
            {
                var list = searchString.Split(' ');
                
                if (list.Length > 1)
                {
                    var titlu = list[0];
                    var nota = int.Parse(list[1]);
                    recenzii = recenzii.Where(s => s.Titlu.Contains(titlu) && s.Nota == nota);
                }
                else
                {
                    int n;
                    bool isNumeric = int.TryParse(searchString, out n);
                    if (isNumeric)
                    {
                        recenzii = recenzii.Where(s => s.Nota == n);
                    }
                    else
                    {
                        recenzii = recenzii.Where(s => s.Titlu.Contains(searchString));
                    }
                }
            }

            return View(recenzii.ToList());
        }

        // GET: Recenzie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recenzie = db.Recenzii.Include(x => x.Film).Where(b => b.RecenzieId == id).First();
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            return View(recenzie);
        }

        // GET: Recenzie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recenzie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecenzieId,Titlu,Descriere,Nota,NumeUtilizator")] Recenzie recenzie)
        {
            if (ModelState.IsValid)
            {
                db.Recenzii.Add(recenzie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recenzie);
        }

        // GET: Recenzie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recenzie recenzie = db.Recenzii.Find(id);
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            return View(recenzie);
        }

        // POST: Recenzie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecenzieId,Titlu,Descriere,Nota,NumeUtilizator")] Recenzie recenzie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recenzie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recenzie);
        }

        // GET: Recenzie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recenzie recenzie = db.Recenzii.Find(id);
            if (recenzie == null)
            {
                return HttpNotFound();
            }
            return View(recenzie);
        }

        // POST: Recenzie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recenzie recenzie = db.Recenzii.Find(id);
            db.Recenzii.Remove(recenzie);
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
