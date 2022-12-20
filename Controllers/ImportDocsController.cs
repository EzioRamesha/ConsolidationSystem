using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsolidationSystem.Data;

namespace ConsolidationSystem.Controllers
{
    public class ImportDocsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImportDocs
        public ActionResult Index()
        {
            return View(db.ImportDocs.ToList());
        }

        // GET: ImportDocs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportDoc importDoc = db.ImportDocs.Find(id);
            if (importDoc == null)
            {
                return HttpNotFound();
            }
            return View(importDoc);
        }

        // GET: ImportDocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImportDocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InternalReference,DocDate,Description,DocNo,ItemNo,Currency,Type,Amount,CreatedDateTime,CreatedUserName,IsEnabled")] ImportDoc importDoc)
        {
            if (ModelState.IsValid)
            {
                importDoc.Id = Guid.NewGuid();
                db.ImportDocs.Add(importDoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(importDoc);
        }

        // GET: ImportDocs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportDoc importDoc = db.ImportDocs.Find(id);
            if (importDoc == null)
            {
                return HttpNotFound();
            }
            return View(importDoc);
        }

        // POST: ImportDocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InternalReference,DocDate,Description,DocNo,ItemNo,Currency,Type,Amount,CreatedDateTime,CreatedUserName,IsEnabled")] ImportDoc importDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(importDoc);
        }

        // GET: ImportDocs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportDoc importDoc = db.ImportDocs.Find(id);
            if (importDoc == null)
            {
                return HttpNotFound();
            }
            return View(importDoc);
        }

        // POST: ImportDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ImportDoc importDoc = db.ImportDocs.Find(id);
            db.ImportDocs.Remove(importDoc);
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
