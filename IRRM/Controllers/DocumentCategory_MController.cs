using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;

namespace IRRM.Controllers
{
    public class DocumentCategory_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentCategory_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            return View(db.DocumentCategory_M.ToList());
        }

        // GET: DocumentCategory_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCategory_M documentCategory_M = db.DocumentCategory_M.Find(id);
            if (documentCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentCategory_M);
        }

        // GET: DocumentCategory_M/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentCategory_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocCategoryID,DocCode,DocCategory,Remarks,Active")] DocumentCategory_M documentCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.DocumentCategory_M.Add(documentCategory_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentCategory_M);
        }

        // GET: DocumentCategory_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCategory_M documentCategory_M = db.DocumentCategory_M.Find(id);
            if (documentCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentCategory_M);
        }

        // POST: DocumentCategory_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocCategoryID,DocCode,DocCategory,Remarks,Active")] DocumentCategory_M documentCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentCategory_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentCategory_M);
        }

        // GET: DocumentCategory_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentCategory_M documentCategory_M = db.DocumentCategory_M.Find(id);
            if (documentCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentCategory_M);
        }

        // POST: DocumentCategory_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentCategory_M documentCategory_M = db.DocumentCategory_M.Find(id);
            db.DocumentCategory_M.Remove(documentCategory_M);
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
