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
    public class Task_People_TController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Task_People_T
        public ActionResult Index()
        {
            var task_People_T = db.Task_People_T.Include(t => t.TaskPeopleInvolved_M).Include(t => t.TaskRelation_M).Include(t => t.Task);
            return View(task_People_T.ToList());
        }

        // GET: Task_People_T/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task_People_T task_People_T = db.Task_People_T.Find(id);
            if (task_People_T == null)
            {
                return HttpNotFound();
            }
            return View(task_People_T);
        }

        // GET: Task_People_T/Create
        public ActionResult Create(int TaskID)
        {
            ViewBag.TaskPeopleInvolvedID = db.TaskPeopleInvolved_M.ToList();
            List<Users_M> itm = db.Users_M.Where(x => x.Active == true).ToList();
            List<_User> itx = new List<_User>();
            _User _itx;

            foreach (var _itm in itm)
            {
                _itx = new _User();

                _itx.UserID2 = Convert.ToString(_itm.UserID);
                _itx.Name = _itm.Name + "-" + _itm.Designation + "-" + _itm.Department_M.Department + "-" + _itm.Branch_M.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _User { Name = "Select", UserID2 = "" });

            ViewBag.UserID = new SelectList(itx, "UserID2", "Name");
            ViewBag.Users = itx.ToList();

            Task tsk = db.Tasks.Find(TaskID);
            if (tsk.OwnerID == null)
            {
                ViewBag.TaskRelationID = new SelectList(db.TaskRelation_M, "TaskRelationID", "TaskRelation");
            }
            else {
                ViewBag.TaskRelationID = new SelectList(db.TaskRelation_M.Where(x => x.TaskRelationID != 1), "TaskRelationID", "TaskRelation");
            }

            
            
            
            ViewBag.TaskID = TaskID;
            return View();
        }

        // POST: Task_People_T/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskPeopleID,TaskID,TaskPeopleInvolvedID,TaskRelationID,UserID,Name,Mobile,IdentityNo,DateOfBirth,Gender,Nationality,RegNo,CVRNo,CreateUserID,CreatedDate")] Task_People_T task_People_T)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);
            if (ModelState.IsValid)
            {
                task_People_T.CreatedDate = DateTime.UtcNow.AddHours(3);
                task_People_T.CreateUserID = UserID;
                db.Task_People_T.Add(task_People_T);
                db.SaveChanges();

                Task tsk = db.Tasks.Find(task_People_T.TaskID);

                Utility utl = new Utility();
                if (task_People_T.TaskRelationID == 1) // if owner then add owner to taks an set status to assigned.
                {
                    tsk.OwnerID = task_People_T.UserID;
                    db.Entry(tsk).Property(x => x.OwnerID).IsModified = true;
                    db.SaveChanges();
                    utl.setStatusTask(tsk.TaskID, 3, Convert.ToInt32(Session["UserID"]));
                }

                TaskRelation_M tskrl = db.TaskRelation_M.Where(x => x.TaskRelationID == task_People_T.TaskRelationID).SingleOrDefault();

                Users_M user = db.Users_M.Where(x => x.UserID == tsk.OwnerID).SingleOrDefault();
                if (user.EmailID != null)
                {
                    if (user.EmailID != "" && user.EmailID.Contains("@"))
                    {
                        Core.Utility utlx = new Core.Utility();
                        utlx.sendemail("irrm@mehospital.com", user.EmailID, tskrl.EmailSubject, "<p>"+ tskrl.EmailHeader + "</p><p>" + tskrl.EmailFooter + "</p><p>Task No." + tsk.TaskNo + "</p>");
                    }
                }


                return RedirectToAction("../Tasks/Details/" + task_People_T.TaskID);
            }

            ViewBag.TaskPeopleInvolvedID = new SelectList(db.TaskPeopleInvolved_M, "TaskPeopleInvolvedID", "TaskPeopleInvolved", task_People_T.TaskPeopleInvolvedID);
            ViewBag.TaskRelationID = new SelectList(db.TaskRelation_M, "TaskRelationID", "TaskRelation", task_People_T.TaskRelationID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ReferenceNo", task_People_T.TaskID);
            return View(task_People_T);
        }

        // GET: Task_People_T/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task_People_T task_People_T = db.Task_People_T.Find(id);
            if (task_People_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskPeopleInvolvedID = new SelectList(db.TaskPeopleInvolved_M, "TaskPeopleInvolvedID", "TaskPeopleInvolved", task_People_T.TaskPeopleInvolvedID);
            ViewBag.TaskRelationID = new SelectList(db.TaskRelation_M, "TaskRelationID", "TaskRelation", task_People_T.TaskRelationID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ReferenceNo", task_People_T.TaskID);
            return View(task_People_T);
        }

        // POST: Task_People_T/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskPeopleID,TaskID,TaskPeopleInvolvedID,TaskRelationID,UserID,Name,Mobile,IdentityNo,DateOfBirth,Gender,Nationality,RegNo,CVRNo,CreateUserID,CreatedDate")] Task_People_T task_People_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task_People_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TaskPeopleInvolvedID = new SelectList(db.TaskPeopleInvolved_M, "TaskPeopleInvolvedID", "TaskPeopleInvolved", task_People_T.TaskPeopleInvolvedID);
            ViewBag.TaskRelationID = new SelectList(db.TaskRelation_M, "TaskRelationID", "TaskRelation", task_People_T.TaskRelationID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "ReferenceNo", task_People_T.TaskID);
            return View(task_People_T);
        }

        // GET: Task_People_T/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task_People_T task_People_T = db.Task_People_T.Find(id);
            if (task_People_T == null)
            {
                return HttpNotFound();
            }
            return View(task_People_T);
        }

        // POST: Task_People_T/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task_People_T task_People_T = db.Task_People_T.Find(id);
            db.Task_People_T.Remove(task_People_T);
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
