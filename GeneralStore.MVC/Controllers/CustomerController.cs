using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _customerDb = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customerList = _customerDb.Customers.ToList();
            List<Customer> orderedCustomerList = customerList.OrderBy(cust => cust.LastName).ToList();
            return View(orderedCustomerList);
        }

        // GET: Customer
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerDb.Customers.Add(customer);
                _customerDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET : DELETE 
        // Customer/Delete/{id} 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerDb.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST : DELETE
        // Customer/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Customer customer = _customerDb.Customers.Find(id);
            _customerDb.Customers.Remove(customer);
            _customerDb.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET : Edit
        // Customer/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerDb.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST : Edit
        // Customer/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerDb.Entry(customer).State = EntityState.Modified;
                _customerDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET : Details
        // Customer/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerDb.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
    }
}