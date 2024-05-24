using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResumeManagement.Models;
using ResumeManagement.Models.ViewModels;

namespace ResumeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private appResumeDBConfig db = new appResumeDBConfig();

        public ActionResult Index()
        {
            var employees = db.Employees.Include(c => c.QualificationEntries.Select(b => b.Qualification))
                                        .OrderByDescending(e => e.EmployeeId)
                                        .ToList();
            return View(employees); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddNewQualification(int? id)
        {
            ViewBag.qualifications = new SelectList(db.Qualifications.ToList(), "QualificationId", "QualificationName", (id != null) ? id.ToString() : "");
            return PartialView("_AddNewQualification");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel vObj, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    EmployeeName = vObj.EmployeeName,
                    JoinDate = vObj.JoinDate,
                    salary = vObj.salary,
                    isActive = vObj.isActive
                };
                HttpPostedFileBase file = vObj.PicturePath;
                string filepath = Path.Combine("/images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                file.SaveAs(Server.MapPath(filepath));
                employee.Picture = filepath;

                foreach (var item in qualificationId)
                {
                    QualificationEntry qe = new QualificationEntry()
                    {
                        Employee = employee,
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item
                    };
                    db.QualificationEntries.Add(qe);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Employee employee = db.Employees.First(x => x.EmployeeId == id);
            var qualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();
            EmployeeViewModel vObj = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                salary = employee.salary,
                JoinDate = employee.JoinDate,
                Picture = employee.Picture,
                isActive = employee.isActive
            };
            if (qualifications.Count > 0)
            {
                foreach (var item in qualifications)
                {
                    vObj.QualificationList.Add(item.QualificationId);
                }
            }
            return View(vObj);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel vObj, int[] qualificationId)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeName = vObj.EmployeeName,
                    JoinDate = vObj.JoinDate,
                    salary = vObj.salary,
                    isActive = vObj.isActive,
                    EmployeeId = vObj.EmployeeId
                };
                HttpPostedFileBase file = vObj.PicturePath;
                if (file != null)
                {
                    string filepath = Path.Combine("/images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filepath));
                    employee.Picture = filepath;
                }
                else
                {
                    employee.Picture = vObj.Picture;
                }
                var existingQualification = db.QualificationEntries.Where(x => x.EmployeeId == employee.EmployeeId).ToList();
                foreach (var item in existingQualification)
                {
                    db.QualificationEntries.Remove(item);
                }
                foreach (var item in qualificationId)
                {
                    QualificationEntry qe = new QualificationEntry()
                    {
                        EmployeeId = employee.EmployeeId,
                        QualificationId = item
                    };
                    db.QualificationEntries.Add(qe);
                }
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                var existingQualifications = db.QualificationEntries.Where(x => x.EmployeeId == id).ToList();
                if (existingQualifications.Any())
                {
                    foreach (var item in existingQualifications)
                    {
                        db.QualificationEntries.Remove(item);
                    }
                }
                db.Entry(employee).State = EntityState.Deleted;
                db.SaveChanges();
            }
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