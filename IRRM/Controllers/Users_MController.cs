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
    public class Users_MController : Controller
    {
        private IRRMEntities db = new IRRMEntities();

        // GET: Users_M
        public ActionResult Index()
        {
            if (Session["SA"] == null) { return RedirectToAction("../Login"); }
            var users_M = db.Users_M.Include(u => u.Department_M).Include(u => u.Branch_M);
            return View(users_M);
        }

        // GET: Users_M/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_M users_M = db.Users_M.Find(id);
            if (users_M == null)
            {
                return HttpNotFound();
            }
            return View(users_M);
        }

        // GET: Users_M/Create
        public ActionResult Create()
        {

            List<Department_M> depm = db.Department_M.Where(x => x.Active == true).ToList();
            List<_Department> depx = new List<_Department>();
            _Department _dex;
            foreach (var _itm in depm)
            {
                _dex = new _Department();
                _dex.DepartmentID = Convert.ToString(_itm.DepartmentID);
                _dex.Department = _itm.Department;
                depx.Add(_dex);
            }
            depx.Insert(0, new _Department { Department = "Select", DepartmentID = "" });

            ViewBag.DepartmentID = new SelectList(depx, "DepartmentID", "Department");





            List<Branch_M> itm = db.Branch_M.Where(x => x.Active == true).ToList();
            List<_Branch> itx = new List<_Branch>();
            _Branch _itx;
            foreach (var _itm in itm)
            {
                _itx = new _Branch();
                _itx.BranchID = Convert.ToString(_itm.BranchID);
                _itx.Branch = _itm.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _Branch { Branch = "Select", BranchID = "" });
            ViewBag.BranchID = new SelectList(itx, "BranchID", "Branch");




            List<UserRole_SM> rolm = db.UserRole_SM.ToList();
            List<_Role> roli = new List<_Role>();
            _Role _rox;
            foreach (var _rlm in rolm)
            {
                _rox = new _Role();
                _rox.UserRoleID = Convert.ToString(_rlm.UserRoleID);
                _rox.Role = _rlm.Role;
                roli.Add(_rox);
            }
            roli.Insert(0, new _Role { Role = "Select", UserRoleID = "" });
            ViewBag.UserRoleID = new SelectList(roli, "UserRoleID", "Role");




            return View();
        }

        // POST: Users_M/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserRoleID,UserName,UserPassword,Name,Phone,Designation,DepartmentID,Comments,Active,BranchID,LicenseNo,EmployeeNo,EmailID")] Users_M users_M)
        {
            if (ModelState.IsValid)
            {
                users_M.BranchID = 1; // remove when branch module is active
                db.Users_M.Add(users_M);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Department_M, "DepartmentID", "Department", users_M.DepartmentID);
            ViewBag.BranchID = new SelectList(db.Branch_M, "BranchID", "Branch", users_M.BranchID);
            return View(users_M);
        }

        // GET: Users_M/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_M users_M = db.Users_M.Find(id);
            if (users_M == null)
            {
                return HttpNotFound();
            }
           
           



            List<Department_M> depm = db.Department_M.Where(x => x.Active == true).ToList();
            List<_Department> depx = new List<_Department>();
            _Department _dex;
            foreach (var _itm in depm)
            {
                _dex = new _Department();
                _dex.DepartmentID = Convert.ToString(_itm.DepartmentID);
                _dex.Department = _itm.Department;
                depx.Add(_dex);
            }
            depx.Insert(0, new _Department { Department = "Select", DepartmentID = "" });

            

            ViewBag.DepartmentID = new SelectList(depx, "DepartmentID", "Department", users_M.DepartmentID);



            List<Branch_M> itm = db.Branch_M.Where(x => x.Active == true).ToList();
            List<_Branch> itx = new List<_Branch>();
            _Branch _itx;
            foreach (var _itm in itm)
            {
                _itx = new _Branch();
                _itx.BranchID = Convert.ToString(_itm.BranchID);
                _itx.Branch = _itm.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _Branch { Branch = "Select", BranchID = "" });
          

            ViewBag.BranchID = new SelectList(itx, "BranchID", "Branch", users_M.BranchID);


            List<UserRole_SM> rolm = db.UserRole_SM.ToList();
            List<_Role> roli = new List<_Role>();
            _Role _rox;
            foreach (var _rlm in rolm)
            {
                _rox = new _Role();
                _rox.UserRoleID = Convert.ToString(_rlm.UserRoleID);
                _rox.Role = _rlm.Role;
                roli.Add(_rox);
            }
            roli.Insert(0, new _Role { Role = "Select", UserRoleID = "" });


            ViewBag.UserRoleID = new SelectList(roli, "UserRoleID", "Role", users_M.UserRoleID);





            return View(users_M);
        }

        // POST: Users_M/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserRoleID,UserName,UserPassword,Name,Phone,Designation,DepartmentID,Comments,Active,BranchID,LicenseNo,EmployeeNo,EmailID")] Users_M users_M)
        {
            if (ModelState.IsValid)
            {
                users_M.BranchID = 1; // remove when branch module is active
                db.Entry(users_M).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Department_M> depm = db.Department_M.Where(x => x.Active == true).ToList();
            List<_Department> depx = new List<_Department>();
            _Department _dex;
            foreach (var _itm in depm)
            {
                _dex = new _Department();
                _dex.DepartmentID = Convert.ToString(_itm.DepartmentID);
                _dex.Department = _itm.Department;
                depx.Add(_dex);
            }
            depx.Insert(0, new _Department { Department = "Select", DepartmentID = "" });



            ViewBag.DepartmentID = new SelectList(depx, "DepartmentID", "Department", users_M.DepartmentID);



            List<Branch_M> itm = db.Branch_M.Where(x => x.Active == true).ToList();
            List<_Branch> itx = new List<_Branch>();
            _Branch _itx;
            foreach (var _itm in itm)
            {
                _itx = new _Branch();
                _itx.BranchID = Convert.ToString(_itm.BranchID);
                _itx.Branch = _itm.Branch;
                itx.Add(_itx);
            }
            itx.Insert(0, new _Branch { Branch = "Select", BranchID = "" });


            ViewBag.BranchID = new SelectList(itx, "BranchID", "Branch", users_M.BranchID);


            List<UserRole_SM> rolm = db.UserRole_SM.ToList();
            List<_Role> roli = new List<_Role>();
            _Role _rox;
            foreach (var _rlm in rolm)
            {
                _rox = new _Role();
                _rox.UserRoleID = Convert.ToString(_rlm.UserRoleID);
                _rox.Role = _rlm.Role;
                roli.Add(_rox);
            }
            roli.Insert(0, new _Role { Role = "Select", UserRoleID = "" });


            ViewBag.UserRoleID = new SelectList(roli, "UserRoleID", "Role", users_M.UserRoleID);




            return View(users_M);
        }

        // GET: Users_M/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_M users_M = db.Users_M.Find(id);
            if (users_M == null)
            {
                return HttpNotFound();
            }
            return View(users_M);
        }

        // POST: Users_M/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users_M users_M = db.Users_M.Find(id);
            db.Users_M.Remove(users_M);
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
