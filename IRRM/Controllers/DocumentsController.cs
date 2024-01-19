using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;

namespace IRRM.Controllers
{
    public class DocumentsController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Documents
        public ActionResult Index()
        {
           // if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            return View(db.Documents_V.OrderByDescending(x=>x.DocumentID).ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
           // if (Session["UserID"] == null) { return RedirectToAction("../Login"); }


            string sql = "select * from Documents_V where 1=1 ";

            if (col["Title"] != null && col["Title"] != "")
            {
                sql += " and Title like '%" + col["Title"] + "%'";
            }

            if (col["ExpiryFrom"] != null && col["ExpiryFrom"] != "")
            {
                sql += " and ValidUpto>='" + col["ExpiryFrom"] + "'";
            }

            if (col["ExpiryTo"] != null && col["ExpiryTo"] != "")
            {
                sql += " and ValidUpto<='" + col["ExpiryTo"] + "'";
            }

            if (col["DocCategoryID"] != null && col["DocCategoryID"] != "0")
            {
                sql += " and DocCategoryID=" + col["DocCategoryID"] + "";
            }


            if (col["DocSubCategoryID"] != null && col["DocSubCategoryID"] != "0")
            {
                sql += " and DocSubCategoryID=" + col["DocSubCategoryID"] + "";
            }

            if (col["CurrentStatus"] != null && col["CurrentStatus"] != "")
            {
                sql += " and CurrentStatus='" + col["CurrentStatus"] + "'";
            }

            sql += " order by documentid desc";
            List<Documents_V> docv = db.Database.SqlQuery<Documents_V>(sql).ToList();

            return View(docv);
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            //ViewBag.Category = db.DocumentCategory_M.Where(x => x.Active == true).ToList();
            //ViewBag.SubCategory = db.DocumentSubCategory_M.Where(x => x.Active == true).ToList();

            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);

            Document document = new Document();

            document.CreatedOn = DateTime.Now;
            document.CreatorID = UserID;
            document.CurrentStatus = "Triggered";
            db.Documents.Add(document);
            db.SaveChanges();

            return RedirectToAction("Edit/" + document.DocumentID, "Documents");
           // return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "DocumentID,DocumentRefID,ReferenceNo,DocCategoryID,DocSubCategoryID,Title,Description,StakeholderID,FileAttached,ValidFrom,ValidUpto,DocumentManager,LocationID,SpecifyLocation,CurrentStatus,CreatorID,CreatedOn")] Document document)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Documents.Add(document);
        //        db.SaveChanges();
        //        return RedirectToAction("Edit/" + document.DocumentID, "Documents");
        //    }

        //    return View(document);
        //}

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            ViewBag.Category = db.DocumentCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.SubCategory = db.DocumentSubCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.Department = db.Department_M.Where(x => x.Active == true && x.BranchID == 1).ToList();

            ViewBag.DocumentStakeholder = db.DocumentStakeholder_M.Where(x => x.Active == true).ToList();

            ViewBag.DocCopyLoc = db.DocumentCopyDept_V.Where(x => x.DocumentID == id).ToList();

            ViewBag.DocViewers =db.DocumentViewers_V.Where(x => x.DocumentID == id).ToList();
            ViewBag.DocumentAccessGroup = db.DocumentAccessGroup_M.Where(x => x.Active == true).ToList();
            ViewBag.DocumentAccessGroupUsers = db.DocumentAccessGroupUsers_V.Where(x => x.DocumentID == id).ToList();

            ViewBag.Users = db.Users_M.Where(x => x.Active == true).ToList();




            Document document = db.Documents.Find(id);
            
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentID,DocumentRefID,ReferenceNo,DocCategoryID,DocSubCategoryID,Title,Description,StakeholderID,FileAttached,FileAttached2,FileName,FileExtension,ValidFrom,ValidUpto,DocumentManager,DepartmentID,SpecifyLocation,CurrentStatus,CreatorID,CreatedOn")] Document document, HttpPostedFileBase FileAttached,FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);


            if (FileAttached != null)
            {
                document.FileName = Convert.ToString(Guid.NewGuid());
                string extension = Path.GetExtension(FileAttached.FileName);
                extension = extension.TrimStart('.');
                string folderpath = Server.MapPath("~/DocumentManager/");
                DirectoryInfo dir = new DirectoryInfo(folderpath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                FileAttached.SaveAs(folderpath + "/" + document.DocumentID + "-" + document.FileName +"." + extension);

                document.FileAttached = FileAttached.FileName;
                document.FileExtension = "." + extension;


            }
            else {
                document.FileAttached = Convert.ToString(col["FileAttached2"]);
            }

            document.CreatedOn = DateTime.Now;
            document.CreatorID = UserID;
            db.Entry(document).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Edit/" + document.DocumentID, "Documents");

        }

        [HttpPost]
        public ActionResult GiveAccess(FormCollection col)
        {
            int DocumentID = Convert.ToInt32(col["DocumentID"]);
            int DocAccessGroupID = Convert.ToInt32(col["DocAccessGroupID"]);
            int UserID = Convert.ToInt32(col["UserID"]);
            DocumentAccess_T docacc = new DocumentAccess_T();
            docacc.DocumentID = DocumentID;
            docacc.UserID = UserID;
            docacc.DocAccessGroupID = DocAccessGroupID;
            db.DocumentAccess_T.Add(docacc);
            db.SaveChanges();


            Document doc = db.Documents.Find(DocumentID);
            doc.CurrentStatus = "Active";
            db.Entry(doc).Property(x => x.CurrentStatus).IsModified = true;
            db.SaveChanges();


            return RedirectToAction("Edit/" + DocumentID, "Documents");
        }



        public ActionResult Register(int id) {

            Utility utl = new Utility();

            string newregno = utl.Getregistryno("DR");

            Document doc = db.Documents.Find(id);
            doc.DocumentNo = newregno;
            db.Entry(doc).Property(x => x.DocumentNo).IsModified = true;
            doc.CurrentStatus = "Registered";
            db.Entry(doc).Property(x => x.CurrentStatus).IsModified = true;
            doc.RegisteredOn = DateTime.Now;
            db.Entry(doc).Property(x => x.RegisteredOn).IsModified = true;
            db.SaveChanges();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        [HttpGet]
        public ActionResult Invalid(int id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            db.Database.ExecuteSqlCommand("delete from  Documents where DocumentID=" + id);
            return Content("OK");
        }

        [HttpGet]
        public ActionResult DeleteDocCopyLoc(int id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            db.Database.ExecuteSqlCommand("delete from  [DocumentCopyLocation_T] where DocCopyLocationID=" + id);
            return Content("OK");
        }

        [HttpGet]
        public ActionResult DeleteDocAccess(int id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            db.Database.ExecuteSqlCommand("delete from  [DocumentAccess_T] where DocAccessID=" + id);
            return Content("OK");
        }



        

        [HttpGet]
        public ActionResult DocumentViewer(int id)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            DocumentViewers_T docv = new DocumentViewers_T();
            int UserID = Convert.ToInt32(Session["UserID"]);

            docv.UserID = UserID;
            docv.ViewedOn = DateTime.Now;
            docv.DocumentID = id;
            db.DocumentViewers_T.Add(docv);
            db.SaveChanges();

            return Content("OK");
        }




        [HttpPost]
        public ActionResult CopyLocation(FormCollection col) {

            int DocumentID = Convert.ToInt32(col["DocumentID"]);

            DocumentCopyLocation_T doccopy = new DocumentCopyLocation_T();
            doccopy.DocumentID = DocumentID;
            doccopy.DepartmentID = Convert.ToInt32(col["DepartmentID"]);
            doccopy.Details = Convert.ToString(col["Details"]);
            db.DocumentCopyLocation_T.Add(doccopy);
            db.SaveChanges();

            return RedirectToAction("Edit/" + DocumentID, "Documents");
        }











        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }



        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult DocumentSearchPartial()
        {



            ViewBag.DocCategoryID = db.DocumentCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.DocSubCategoryID = db.DocumentSubCategory_M.Where(x => x.Active == true).ToList();
            
            //ViewBag.Users = db.Users_M.ToList();


            return PartialView();
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
