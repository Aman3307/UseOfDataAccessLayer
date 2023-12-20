using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sp0908.DAL;
using Sp0908.Models;

namespace Sp0908.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_DAL _EmployeeDAL = new Employee_DAL();
        // GET: Employee
        public ActionResult Index()
        {
            var EmployeeList = _EmployeeDAL.GetAllEmployees();
            if(EmployeeList.Count==0)
            {
                TempData["InfoMessage"] = "Currently Employees Not available in the databse";
            }
            return View(EmployeeList);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            bool Isinserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Isinserted = _EmployeeDAL.InsertEmployees(employee);
                    if (Isinserted)
                    {
                        TempData["SuccessMessage"] = "product details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "unable to save the product details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = _EmployeeDAL.GetEmployeeById(id).FirstOrDefault();
            if(employees == null)
            {
                TempData["InfoMessage"] = "Product not avalable with Id" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // POST: Employee/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Boolean IsUpdated = _EmployeeDAL.UpdateEmployee(employee);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully";
                    }
                    else
                    {
                        TempData["ErrorMeaage"] = "Details could not be updated";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _EmployeeDAL.GetEmployeeById(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Product not avalable with Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Employee/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _EmployeeDAL.DeleteEmployee(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;   return View(); 
            }
        }
    }
}
