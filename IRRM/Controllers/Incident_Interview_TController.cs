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
    public class Incident_Interview_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Incident_Interview_T
        public ActionResult Index()
        {
            return View(db.Incident_Interview_T.ToList());
        }

        // GET: Incident_Interview_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Interview_T incident_Interview_T = db.Incident_Interview_T.Find(id);
            if (incident_Interview_T == null)
            {
                return HttpNotFound();
            }
            return View(incident_Interview_T);
        }

        // GET: Incident_Interview_T/Create
        public ActionResult Create(int IncidentID)
        {
            ViewBag.IncidentID = IncidentID;



            List<Users_M> itm = db.Users_M.Where(x => x.Active == true).ToList();
            List<_User> itx = new List<_User>();
            _User _itx;

            foreach (var _itm in itm)
            {
                _itx = new _User();

                _itx.UserID2 = Convert.ToString(_itm.UserID);
                _itx.Name = _itm.Name;
                itx.Add(_itx);
            }
            itx.Insert(0, new _User { Name = "Select", UserID2 = "" });


            ViewBag.InterviewerID = new SelectList(itx, "UserID2", "Name");



            //List<Incident_People_T> ipinAlldata = db.Incident_People_T.Where(x => x.IncidentID == IncidentID).ToList();
            //List<_Incident_People> ipinNewlist = new List<_Incident_People>();
            //_Incident_People _ipinnewItem;

            //foreach (var ipin in ipinAlldata)
            //{

            //    _ipinnewItem = new _Incident_People();
            //    _ipinnewItem.IncidentPeopleID = ipin.IncidentPeopleID.ToString();
            //    _ipinnewItem.Name = ipin.Name;
            //    ipinNewlist.Add(_ipinnewItem);
            //}


            List<_IncPeople> ipinAlldata = db.Database.SqlQuery<_IncPeople>(@"select distinct ip.UserID,IncidentPeopleID,IncidentID, ip.Name, ip.Mobile,ip.UserID, u.Name as UserName,u.Phone as UserMobile,IncidentRelation, IncidentPeopleInvolved   from [dbo].[Incident-People_T] ip
left join Users_M u on ip.UserID=u.UserID
left join IncidentPeopleInvolved_M pi on ip.IncidentPeopleInvolvedID= pi.IncidentPeopleInvolvedID
left join IncidentRelation_M ir on ip.IncidentRelationID = ir.IncidentRelationID where ir.IncidentRelationid>1 and  ip.incidentid=" + IncidentID).ToList();

            List<_IncPeople> ipinNewlist = new List<_IncPeople>();

            _IncPeople _ipinnewItem;

            foreach (var item in ipinAlldata)
            {
                _ipinnewItem = new _IncPeople();
                _ipinnewItem.UserID = item.UserID;
                _ipinnewItem.IncidentID = item.IncidentID;
                _ipinnewItem.IncidentPeopleInvolved = item.IncidentPeopleInvolved;
                _ipinnewItem.IncidentRelation = item.IncidentRelation;
                if (item.UserID != null)
                {
                    _ipinnewItem.Name = item.UserName;
                    _ipinnewItem.Mobile = item.UserMobile;
                }
                else
                {
                    _ipinnewItem.Name = item.Name;
                    _ipinnewItem.Mobile = item.Mobile;
                }
                _ipinnewItem.IncidentPeopleID = item.IncidentPeopleID;
                ipinNewlist.Add(_ipinnewItem);
            }




            // ipinNewlist.Insert(0, new _IncPeople { IncidentPeopleID = "", Name = "Select" });

            ViewBag.IntervieweeID = ipinNewlist.ToList();


            return View();
        }

        // POST: Incident_Interview_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Incident_Interview_T incident_Interview_T,FormCollection col)
        {
            
                incident_Interview_T.IncidentPeopleID = Convert.ToInt32(col["IntervieweeID"]);
                incident_Interview_T.InterviewerID = Convert.ToInt32(Session["UserID"]);
                incident_Interview_T.CreatedOn = DateTime.Now;
                db.Incident_Interview_T.Add(incident_Interview_T);
                db.SaveChanges();
            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = "OK",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        // GET: Incident_Interview_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Interview_T incident_Interview_T = db.Incident_Interview_T.Find(id);
            if (incident_Interview_T == null)
            {
                return HttpNotFound();
            }
            return View(incident_Interview_T);
        }

        // POST: Incident_Interview_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentInterviewID,IncidentID,InterviewerID,IntervieweeID,InterviewDate,CreatedOn,Name,Details")] Incident_Interview_T incident_Interview_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incident_Interview_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incident_Interview_T);
        }

        // GET: Incident_Interview_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Interview_T incident_Interview_T = db.Incident_Interview_T.Find(id);
            if (incident_Interview_T == null)
            {
                return HttpNotFound();
            }
            return View(incident_Interview_T);
        }

        // POST: Incident_Interview_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incident_Interview_T incident_Interview_T = db.Incident_Interview_T.Find(id);
            db.Incident_Interview_T.Remove(incident_Interview_T);
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
