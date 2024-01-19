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
    public class IncidentSubOutcome_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: IncidentSubOutcome_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var incidentSubOutcome_M = db.IncidentSubOutcome_M.Include(i => i.IncidentMainOutcome_M);
            return View(incidentSubOutcome_M.ToList());
        }

        // GET: IncidentSubOutcome_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubOutcome_M incidentSubOutcome_M = db.IncidentSubOutcome_M.Find(id);
            if (incidentSubOutcome_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubOutcome_M);
        }

        // GET: IncidentSubOutcome_M/Create
        public ActionResult Create()
        {
            ViewBag.IncidentMainOutcomeID = new SelectList(db.IncidentMainOutcome_M, "IncidentMainOutcomeID", "IncidentMainOutcome");
            return View();
        }

        // POST: IncidentSubOutcome_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentSubOutcomeID,IncidentMainOutcomeID,IncidentSubOutcome,AnswerType")] IncidentSubOutcome_M incidentSubOutcome_M)
        {
            if (ModelState.IsValid)
            {
                db.IncidentSubOutcome_M.Add(incidentSubOutcome_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentMainOutcomeID = new SelectList(db.IncidentMainOutcome_M, "IncidentMainOutcomeID", "IncidentMainOutcome", incidentSubOutcome_M.IncidentMainOutcomeID);
            return View(incidentSubOutcome_M);
        }

        // GET: IncidentSubOutcome_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubOutcome_M incidentSubOutcome_M = db.IncidentSubOutcome_M.Find(id);
            if (incidentSubOutcome_M == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentMainOutcomeID = new SelectList(db.IncidentMainOutcome_M, "IncidentMainOutcomeID", "IncidentMainOutcome", incidentSubOutcome_M.IncidentMainOutcomeID);
            return View(incidentSubOutcome_M);
        }

        // POST: IncidentSubOutcome_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentSubOutcomeID,IncidentMainOutcomeID,IncidentSubOutcome,AnswerType")] IncidentSubOutcome_M incidentSubOutcome_M)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentSubOutcome_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentMainOutcomeID = new SelectList(db.IncidentMainOutcome_M, "IncidentMainOutcomeID", "IncidentMainOutcome", incidentSubOutcome_M.IncidentMainOutcomeID);
            return View(incidentSubOutcome_M);
        }

        // GET: IncidentSubOutcome_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentSubOutcome_M incidentSubOutcome_M = db.IncidentSubOutcome_M.Find(id);
            if (incidentSubOutcome_M == null)
            {
                return HttpNotFound();
            }
            return View(incidentSubOutcome_M);
        }

        // POST: IncidentSubOutcome_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentSubOutcome_M incidentSubOutcome_M = db.IncidentSubOutcome_M.Find(id);
            db.IncidentSubOutcome_M.Remove(incidentSubOutcome_M);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddChoice(FormCollection col)
        {

            IncidentSubOutcomeAnswerChoice_T anschoice = new IncidentSubOutcomeAnswerChoice_T();

            anschoice.IncidentSubOutcomeID = Convert.ToInt32(col["IncidentSubOutcomeID"]);
            anschoice.ChoiceAnswer = Convert.ToString(col["Choice"]);

            db.IncidentSubOutcomeAnswerChoice_T.Add(anschoice);
            db.SaveChanges();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult ViewChoiceOutcome(int IncidentSubOutcomeID)
        {
            var anschoice = db.IncidentSubOutcomeAnswerChoice_T.Where(x => x.IncidentSubOutcomeID == IncidentSubOutcomeID).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = anschoice.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult DeleteChoiceOutcome(int IncidentSubOutcomeID, int AnswerChoiceID)
        {
            db.Database.ExecuteSqlCommand("Delete from IncidentSubOutcomeAnswerChoice_T where IncidentSubOutcomeID=" + IncidentSubOutcomeID + " and AnswerChoiceID=" + AnswerChoiceID);

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
