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
    public class TasksController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Tasks
        public ActionResult Index()
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);
            
            string qry = "";
            if (Convert.ToInt32(Session["RoleID"]) == 1)
            { // admin
                qry = @"select *  from TaskIndex_V t where TaskStatusID<6  order by t.taskid desc";
            }
            else
            {
                int userid = Convert.ToInt32(Session["UserID"]);
                //qry = @"select *  from TaskIndex_V t where registered=1 and TaskStatusID<6 and (t.OwnerID=" + userid + " or t.createduserid= " + userid + ")  order by t.taskid desc";
                qry = @"select *  from TaskIndex_V t where registered=1 and TaskStatusID<6 and (t.OwnerID=" + userid + " or t.createduserid= " + userid + " or taskid in (select taskid from [Task-People_T] where userid=" + userid + ") )  order by t.taskid desc";
            }

            ViewBag.tasks = db.Database.SqlQuery<TaskIndex_V>(qry).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);
            string qry = "";
           

            string searchconditions = "";

            if (Convert.ToInt32(Session["RoleID"]) != 1)
            { // not admin
                searchconditions += " and registered=1  and (t.OwnerID=" + UserID + " or t.createduserid= " + UserID + " or taskid in (select taskid from [Task-People_T] where userid=" + UserID + ") ) ";
            }


            if (Convert.ToString(col["RegisteredonFrom"]) != "")
            {
                searchconditions += " and Registeredon>='" + Convert.ToDateTime(col["RegisteredonFrom"]).ToString("yyyy-MM-dd") + " 00:00:00' ";
            }



            if (Convert.ToString(col["RegisteredonTo"]) != "")
            {
                searchconditions += " and Registeredon<='" + Convert.ToDateTime(col["RegisteredonTo"]).ToString("yyyy-MM-dd") + " 23:59:59' ";
            }

            if (Convert.ToString(col["AssignedTo"]) != "")
            {
                searchconditions += " and AssignedTo='" + Convert.ToString(col["AssignedTo"]) + "'";
            }

            if (Convert.ToString(col["TaskCategoryID"]) != "0")
            {
                searchconditions += " and CategoryID=" + Convert.ToString(col["TaskCategoryID"]);
            }


            if (Convert.ToString(col["TaskSubCategoryID"]) != "0")
            {
                searchconditions += " and SubCategoryID=" + Convert.ToString(col["TaskSubCategoryID"]);
            }

            if (Convert.ToString(col["LocationID"]) != "0")
            {
                searchconditions += " and LocationID=" + Convert.ToString(col["LocationID"]);
            }


            if (Convert.ToString(col["TaskPriorityID"]) != "0")
            {
                searchconditions += " and PriorityID=" + Convert.ToString(col["TaskPriorityID"]);
            }


            if (Convert.ToString(col["TaskStatusID"]) != "0")
            {
                searchconditions += " and TaskStatusID=" + Convert.ToString(col["TaskStatusID"]);
            }

            if (Convert.ToString(col["OwnerID"]) != "0")
            {
                searchconditions += " and OwnerID=" + Convert.ToString(col["OwnerID"]);
            }

            qry = @"select *  from TaskIndex_V t where 1=1 " + searchconditions + "   order by t.taskid desc";
       



            ViewBag.tasks = db.Database.SqlQuery<TaskIndex_V>(qry).ToList();

            return View();
        }

        public ActionResult TaskSearchPartial()
        {



            ViewBag.CategoryID = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.SubCategoryID = db.TaskSubCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();


            ViewBag.PriorityID = db.TaskPriorities_M.ToList();
            ViewBag.StatusID = db.TaskStatus_M.Where(x => x.TaskStatusID > 1).ToList();
            ViewBag.Users = db.Users_M.ToList();


            return PartialView();
        }


        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            int roleflag = -1;
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);
            TaskIndex_V task = db.TaskIndex_V.Where(x => x.TaskID == id).SingleOrDefault();

            ViewBag.Comments = db.TaskComment_V.Where(x => x.TaskID == id).OrderByDescending(x=>x.CommentID).ToList();

            ViewBag.DocumentType = db.DocumentType_SM.ToList();
            string startdate = db.Database.SqlQuery<string>("select convert(varchar, [startdate], 111)  as startdate from tasks where taskid=" + id).SingleOrDefault();
            string duedate = db.Database.SqlQuery<string>("select convert(varchar, [duedate], 111)  as duedate from tasks where taskid=" + id).SingleOrDefault();

            if (startdate != null)
            {
                ViewBag.startdate = startdate.Replace("/", "-");
            }
            if (duedate != null)
            {
                ViewBag.duedate = duedate.Replace("/", "-");
            }

            if (Convert.ToString(Session["RoleID"]) == "1")
            {
                roleflag = 0;
                if (task.OwnerID == UserID)
                {

                    if (task.TaskStatusID == 3)
                    {
                        Utility utl = new Utility();
                        utl.setStatusTask(task.TaskID, 4, Convert.ToInt32(Session["UserID"]));
                        return RedirectToAction("Details", "Tasks", id);
                    }

                }
            }
            else
            {
                if (task.OwnerID == UserID)
                {
                    roleflag = 1;

                    if (task.TaskStatusID == 3)
                    {
                        Utility utl = new Utility();
                        utl.setStatusTask(task.TaskID, 4, Convert.ToInt32(Session["UserID"]));
                        return RedirectToAction("Details", "Tasks", id);
                    }

                }


            }


               
            if (task == null)
            {
                return HttpNotFound();
            }

            


            ViewBag.Status = db.TaskStatusUser_V.Where(x => x.TaskID == id).ToList();


            ViewBag.people = db.PeopleUser_V.Where(x=>x.TaskID == id).ToList();

            ViewBag.roleflag = roleflag;


            if (task.TaskStatusID < 5 && task.TaskDueDate <= DateTime.UtcNow.AddHours(3))
            {
                ViewBag.OverDue = "Yes";
                ViewBag.OverDueDays = Convert.ToInt32((DateTime.UtcNow.AddHours(3) - Convert.ToDateTime(task.TaskDueDate)).TotalDays);
            }



            return View(task);
        }

        [HttpPost]
        public ActionResult Comment (FormCollection col) {

            int id = Convert.ToInt32(col["TaskID"]);

            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int UserID = Convert.ToInt32(Session["UserID"]);

            Task_Comments tc = new Task_Comments();
            tc.UserID = UserID;
            tc.TaskID = id;
            tc.Comment = Convert.ToString(col["Comment"]);
            tc.CommentDateTime = DateTime.UtcNow.AddHours(3);
            db.Task_Comments.Add(tc);
            db.SaveChanges();

            return RedirectToAction("../Tasks/Details/" + id);
        }



        public ActionResult Report(int id) {

            TaskIndex_V tv = db.TaskIndex_V.Where(x => x.TaskID == id).SingleOrDefault();
            return View(tv);
        
        }


        [HttpPost]
        public ActionResult RegisterTask(int taskid, string prefix)
        {
            Utility utl = new Utility();
            string newregno = utl.Getregistryno(prefix);
            String CreatedOn = Convert.ToDateTime(DateTime.UtcNow.AddHours(3)).ToString("yyyy/MM/dd HH:mm:ss");
            string sql = "Update Tasks set registered=1,registeredon='" + CreatedOn + "',TaskNo='" + newregno + "' where taskid=" + taskid;
            db.Database.ExecuteSqlCommand(sql);
            utl.setStatusTask(taskid, 2, Convert.ToInt32(Session["UserID"]));
            return Content("OK");
        }

        [HttpPost]
        public ActionResult OwnerNote(FormCollection col)
        {
            int tksid = Convert.ToInt32(col["TaskID"]);
            Task task = db.Tasks.Find(tksid);
            task.OwnerNote = Convert.ToString(col["OwnerNotes"]);
            db.Entry(task).Property(x => x.OwnerNote).IsModified = true;
            task.TaskCost = Convert.ToString(col["TaskCost"]);
            db.Entry(task).Property(x => x.TaskCost).IsModified = true;

            //string sd = Convert.ToString(col["StartDate"]).Replace('-', '/') + " 00:00:00";
            //string dd = Convert.ToString(col["DueDate"]).Replace('-', '/') + " 00:00:00";

            task.StartDate = Convert.ToDateTime(col["StartDate"]);
            db.Entry(task).Property(x => x.StartDate).IsModified = true;

            task.DueDate = Convert.ToDateTime(col["DueDate"]);
            db.Entry(task).Property(x => x.DueDate).IsModified = true;

            task.ProviderName = Convert.ToString(col["ProviderName"]);
            db.Entry(task).Property(x => x.ProviderName).IsModified = true;

            task.AssignedTo = Convert.ToString(col["AssignedTo"]);
            db.Entry(task).Property(x => x.AssignedTo).IsModified = true;

            task.Progress = Convert.ToInt32(col["Progress"]);
            db.Entry(task).Property(x => x.Progress).IsModified = true;

            db.SaveChanges();
            return Content("OK");
        }

        [HttpPost]
        public ActionResult Feedback(FormCollection col)
        {
            int tksid = Convert.ToInt32(col["TaskID"]);
            Task task = db.Tasks.Find(tksid);
            
            task.FeedbackType = Convert.ToString(col["FeedbackType"]);
            db.Entry(task).Property(x => x.FeedbackType).IsModified = true;

            task.Feedback = Convert.ToString(col["Feedbackbox"]);
            db.Entry(task).Property(x => x.Feedback).IsModified = true;

            db.SaveChanges();
            return Content("OK");
        }


        [HttpPost]
        public ActionResult TaskCompleted(FormCollection col)
        {
            int tksid = Convert.ToInt32(col["TaskID"]);
            Utility utl = new Utility();
            utl.setStatusTask(tksid, 5, Convert.ToInt32(Session["UserID"]));
            return Content("OK");
        }

        public ActionResult TaskClosed(FormCollection col)
        {
            int tksid = Convert.ToInt32(col["TaskID"]);
            Utility utl = new Utility();
            utl.setStatusTask(tksid, 6, Convert.ToInt32(Session["UserID"]));
            return Content("OK");
        }




        [HttpGet]
        public ActionResult InvalidTask(int taskid)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            db.Database.ExecuteSqlCommand("delete from  Tasks where taskid=" + taskid);
            return Content("OK");
        }

        [HttpGet]
        public ActionResult DeletePeople(int TaskPeopleID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }


            Task_People_T tp = db.Task_People_T.Find(TaskPeopleID);

            if (tp.TaskRelationID == 1){

                Task tsk = db.Tasks.Find(tp.TaskID);
                tsk.OwnerID = null;
                db.Entry(tsk).Property(x => x.OwnerID).IsModified = true;
                db.SaveChanges();
            }
            
            db.Task_People_T.Remove(tp);
            db.SaveChanges();

            return Content("OK");
        }






        // GET: Tasks/Create
        public ActionResult Create(int ReferenceID, string ReferenceNo)
        {

            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);


            ViewBag.ReferenceID = ReferenceID;
            ViewBag.ReferenceNo = ReferenceNo;

            ViewBag.Category = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.SubCategory = db.TaskSubCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.Priority = db.TaskPriorities_M.ToList();

            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();

            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,Registered,CreatedUserID,IncidentID,ReferenceNo,ReferenceID,TaskNo,CategoryID,SubCategoryID,PriorityID,OwnerID,AssignedTo,TaskCost,CreatedDate,StartDate,DueDate,TaskStatusID,OwnerNote,Description,LocationID")] Task task)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            int UserID = Convert.ToInt32(Session["UserID"]);

            

            task.CreatedDate = DateTime.UtcNow.AddHours(3);
                task.TaskStatusID = 1;
                task.CreatedUserID = UserID;
                db.Tasks.Add(task);
                db.SaveChanges();

                Task_Status_T ts = new Task_Status_T();

                ts.TaskID = task.TaskID;
                ts.CreatedOn = DateTime.UtcNow.AddHours(3);
                ts.TaskStatusID = 1;
                ts.CreatedUserID = UserID;
                db.Task_Status_T.Add(ts);
                db.SaveChanges();

                ViewBag.Success = "YES";


            

            ViewBag.Category = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.Priority = db.TaskPriorities_M.ToList();
            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();


            Core.Utility mailutl = new Core.Utility();

            string mailbody = "Dear Administrator,<p>New Task has been triggered, please check the system.</p>";

            List<Users_M> admins = db.Users_M.Where(x => x.UserRoleID == 1).ToList();

            foreach (var e in admins)
            {

                if (e.EmailID != null)
                {
                    try
                    {
                        mailutl.sendemail("irrm@mehospital.com", e.EmailID, "Task Triggered " + task.TaskNo, mailbody);
                    }
                    catch { }
                }
            }

            return View(task);
        }



        public ActionResult GetTaskSubCategory(int CategoryID)
        {

            List<_TaskSubCategory> _ist = new List<_TaskSubCategory>();
            _ist = (from ist in db.TaskSubCategory_M
                    where ist.TaskCategoryID == CategoryID && ist.Active == true
                    select new _TaskSubCategory { TaskSubCategoryID = ist.TaskSubCategoryID, SubCategory = ist.SubCategory }).ToList();


            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = _ist.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };

        }



        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = db.TaskCategory_M.Where(x => x.Active == true).ToList();
            ViewBag.SubCategory = db.TaskSubCategory_M.Where(x => x.Active == true && x.TaskCategoryID == task.CategoryID).ToList();

            ViewBag.PriorityID = new SelectList(db.TaskPriorities_M, "TaskPriorityID", "Priority", task.PriorityID);
            ViewBag.Location = db.Location_M.Where(x => x.Active == true).ToList();
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,Registered,Registeredon,CreatedUserID,IncidentID,ReferenceNo,TaskNo,CategoryID,SubCategoryID,PriorityID,LocationID,OwnerID,AssignedTo,ProviderName,TaskStatusID,OwnerNote,Description,TaskCost,StartDate,DueDate,Progress")] Task task)
        {

            if (task.PriorityID != null)
            {
                double investdays = (double)db.TaskPriorities_M.Where(x => x.TaskPriorityID == task.PriorityID).Select(x => x.InvestigationDays).SingleOrDefault();
                task.TaskDueDate = Convert.ToDateTime(task.Registeredon).AddDays(investdays);
            }


            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("../Tasks/Details/" + task.TaskID);

        }

        // GET: Tasks/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult AddAttachment(FormCollection col, HttpPostedFileBase UploadMedia)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }
            int TaskID = Convert.ToInt32(col["TaskID"]);


            if (UploadMedia != null)
            {

                var id = new TaskDocument_T()
                {
                    TaskID = TaskID,
                    DocumentTypeID = Convert.ToInt32(col["DocumentTypeID"]),
                    Description = Convert.ToString(col["Description"]),
                    FileName = UploadMedia.FileName,
                    DocGUID = Guid.NewGuid(),
                };

                db.TaskDocument_T.Add(id);
                db.SaveChanges();

                string extension = Path.GetExtension(UploadMedia.FileName);
                extension = extension.TrimStart('.');

                string folderpath = Server.MapPath("~/Documents/Tasks/" + id.TaskID + "/");
                DirectoryInfo dir = new DirectoryInfo(folderpath);
                if (!dir.Exists)
                {
                    dir.Create();
                }


                UploadMedia.SaveAs(folderpath + "/" + Convert.ToString(id.DocGUID) + "." + extension);
            }



            List<_IncidentDocument> iv = new List<_IncidentDocument>();
            iv = (from d in db.TaskDocument_T
                  join t in db.DocumentType_SM on d.DocumentTypeID equals t.DocumentTypeID
                  where d.TaskID == TaskID
                  select new _IncidentDocument { IncidentDocumentID = d.TaskDocumentID, DocumentType = t.DocumentType, FileName = d.FileName, Description = d.Description, IncidentID = d.TaskID }).Take(1).ToList();

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = iv.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        public ActionResult ViewAttachment(int TaskID)
        {



            List<_TaskDocument> iv = new List<_TaskDocument>();
            iv = (from d in db.TaskDocument_T
                  join t in db.DocumentType_SM on d.DocumentTypeID equals t.DocumentTypeID
                  where d.TaskID == TaskID
                  select new _TaskDocument { TaskDocumentID = d.TaskDocumentID, DocumentType = t.DocumentType, FileName = d.FileName, Description = d.Description, TaskID = d.TaskID, DocGUID = d.DocGUID }).ToList();

            List<_TaskDocument> dox = new List<_TaskDocument>();

            foreach (var doc in iv)
            {

                _TaskDocument docer = new _TaskDocument();
                docer.TaskDocumentID = doc.TaskDocumentID;
                docer.TaskID = doc.TaskID;
                docer.DocumentType = doc.DocumentType;
                docer.Description = doc.Description;
                docer.FileName = doc.FileName;

                string[] exten = doc.FileName.Split('.');

                docer.ActualFileName = doc.DocGUID + "." + exten[1];

                dox.Add(docer);
            }

            return new JsonpResult
            {
                ContentEncoding = Encoding.UTF8,
                Data = dox.ToList(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        public ActionResult DeleteAttachment(int TaskDocumentID, int TaskID)
        {
            if (Session["UserID"] == null) { return RedirectToAction("../Login"); }

            db.Database.ExecuteSqlCommand("Delete from TaskDocument_T where TaskID=" + TaskID + " and TaskDocumentID=" + TaskDocumentID);

            return Content("OK");
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
