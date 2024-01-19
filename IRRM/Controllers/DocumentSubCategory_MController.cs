using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;

namespace IRRM.Controllers
{
    public class DocumentSubCategory_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: DocumentSubCategory_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var documentSubCategory_M = db.DocumentSubCategory_M.Include(d => d.DocumentCategory_M);
            return View(documentSubCategory_M.ToList());
        }


        public ActionResult DocumentSubCategory(int id)
        {
            List<DocumentSubCat> docsubcat = (from sb in db.DocumentSubCategory_M
                                              where sb.DocCategoryID == id
                                              select new DocumentSubCat { DocSubCategoryID = sb.DocSubCategoryID, DocSubCategory = sb.DocSubCategory }
                                              ).ToList();
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = docsubcat.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }





        // GET: DocumentSubCategory_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentSubCategory_M documentSubCategory_M = db.DocumentSubCategory_M.Find(id);
            if (documentSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentSubCategory_M);
        }

        // GET: DocumentSubCategory_M/Create
        public ActionResult Create()
        {
            //ViewBag.DocCategoryID = new SelectList(db.DocumentCategory_M, "DocCategoryID", "DocCode");
            ViewBag.Category = db.DocumentCategory_M.Where(x => x.Active == true).ToList();

            return View();
        }

        // POST: DocumentSubCategory_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocSubCategoryID,DocCategoryID,DocSubCategory,Remarks,Active")] DocumentSubCategory_M documentSubCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.DocumentSubCategory_M.Add(documentSubCategory_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentSubCategory_M);
        }

        // GET: DocumentSubCategory_M/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Category = db.DocumentCategory_M.Where(x => x.Active == true).ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentSubCategory_M documentSubCategory_M = db.DocumentSubCategory_M.Find(id);
            if (documentSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentSubCategory_M);
        }

        // POST: DocumentSubCategory_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocSubCategoryID,DocCategoryID,DocSubCategory,Remarks,Active")] DocumentSubCategory_M documentSubCategory_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentSubCategory_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentSubCategory_M);
        }

        // GET: DocumentSubCategory_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentSubCategory_M documentSubCategory_M = db.DocumentSubCategory_M.Find(id);
            if (documentSubCategory_M == null)
            {
                return HttpNotFound();
            }
            return View(documentSubCategory_M);
        }

        // POST: DocumentSubCategory_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentSubCategory_M documentSubCategory_M = db.DocumentSubCategory_M.Find(id);
            db.DocumentSubCategory_M.Remove(documentSubCategory_M);
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
