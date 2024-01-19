using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IRRM.Models;
using System.Text;

namespace IRRM.Controllers
{
    public class IncidentSubRootCause_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentSubRootCause_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var incidentSubRootCause_M = db.IncidentSubRootCause_M.Include(i => i.IncidentMainRootCause_M);
            return View(incidentSubRootCause_M.ToList());
        }

        // GET: IncidentSubRootCause_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubRootCause_M incidentSubRootCause_M = db.IncidentSubRootCause_M.Find(id);
            if (incidentSubRootCause_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubRootCause_M);
        }

        // GET: IncidentSubRootCause_M/Create
        public ActionResult Create()
        {
            List<IncidentMainRootCause_M> itm = db.IncidentMainRootCause_M.Where(x => x.Active == true).ToList();

            List<_IncidentMainRootCause> itx = new List<_IncidentMainRootCause>();
            _IncidentMainRootCause _itx;

            foreach (var _itm in itm)
            {
                _itx = new _IncidentMainRootCause();

                _itx.IncidentMainRootCauseID = Convert.ToString(_itm.IncidentMainRootCauseID);
                _itx.IncidentMainRootCause = _itm.IncidentMainRootCause;
                itx.Add(_itx);
            }
            itx.Insert(0, new _IncidentMainRootCause { IncidentMainRootCause = "Select", IncidentMainRootCauseID = "" });


            ViewBag.IncidentMainRootCauseID = new SelectList(itx, "IncidentMainRootCauseID", "IncidentMainRootCause");
            return View();
        }

        // POST: IncidentSubRootCause_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentSubRootCauseID,IncidentMainRootCauseID,IncidentSubRootCause,AnswerType")] IncidentSubRootCause_M incidentSubRootCause_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentSubRootCause_M.Add(incidentSubRootCause_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentMainRootCauseID = new SelectList(db.IncidentMainRootCause_M, "IncidentMainRootCauseID", "IncidentMainRootCause", incidentSubRootCause_M.IncidentMainRootCauseID);
            return View(incidentSubRootCause_M);
        }

        // GET: IncidentSubRootCause_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubRootCause_M incidentSubRootCause_M = db.IncidentSubRootCause_M.Find(id);
            if (incidentSubRootCause_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentMainRootCauseID = new SelectList(db.IncidentMainRootCause_M, "IncidentMainRootCauseID", "IncidentMainRootCause", incidentSubRootCause_M.IncidentMainRootCauseID);
            return View(incidentSubRootCause_M);
        }

        // POST: IncidentSubRootCause_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentSubRootCauseID,IncidentMainRootCauseID,IncidentSubRootCause,AnswerType")] IncidentSubRootCause_M incidentSubRootCause_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentSubRootCause_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentMainRootCauseID = new SelectList(db.IncidentMainRootCause_M, "IncidentMainRootCauseID", "IncidentMainRootCause", incidentSubRootCause_M.IncidentMainRootCauseID);
            return View(incidentSubRootCause_M);
        }

        // GET: IncidentSubRootCause_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubRootCause_M incidentSubRootCause_M = db.IncidentSubRootCause_M.Find(id);
            if (incidentSubRootCause_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubRootCause_M);
        }

        // POST: IncidentSubRootCause_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentSubRootCause_M incidentSubRootCause_M = db.IncidentSubRootCause_M.Find(id);
            db.IncidentSubRootCause_M.Remove(incidentSubRootCause_M);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddChoice(FormCollection col) {

            IncidentSubRootCauseAnswerChoice_T anschoice = new IncidentSubRootCauseAnswerChoice_T();

            anschoice.IncidentSubRootCauseID = Convert.ToInt32(col["IncidentSubRootCauseID"]);
            anschoice.ChoiceAnswer = Convert.ToString(col["Choice"]);

            db.IncidentSubRootCauseAnswerChoice_T.Add(anschoice);
            db.SaveChanges();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult ViewChoice(int IncidentSubRootCauseID)
        {
            var anschoice = db.IncidentSubRootCauseAnswerChoice_T.Where(x => x.IncidentSubRootCauseID == IncidentSubRootCauseID).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = anschoice.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeleteChoice(int IncidentSubRootCauseID, int AnswerChoiceID)
        {
            db.Database.ExecuteSqlCommand("Delete from IncidentSubRootCauseAnswerChoice_T where IncidentSubRootCauseID=" + IncidentSubRootCauseID + " and AnswerChoiceID=" + AnswerChoiceID);

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
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
