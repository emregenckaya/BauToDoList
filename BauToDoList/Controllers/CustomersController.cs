﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BauToDoList.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace BauToDoList.Controllers
{
    public class CustomersController : Controller
    {
        private appDbContext db = new appDbContext();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var customer = new Customer();
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Phone,Fax,WebSite,Address,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Customer customer)
        {
            customer.CreatedDate = DateTime.Now;
            customer.CreatedBy = User.Identity.Name;
            customer.UpdatedDate = DateTime.Now;
            customer.UpdatedBy = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                //burda son customer eklemenin id sini çekme metodunu bul. iki kategori için kullanıcaz.
               await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Phone,Fax,WebSite,Address,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
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
        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from data in db.Customers.ToList()
                              select new
                              {
                                  KimlikNo= data.Id,
                                  Ad = data.Name,
                                  EPosta = data.Email,
                                  Telefon = data.Phone,
                                  Faks = data.Fax,
                                  WebSitesi= data.WebSite,
                                  Adres=data.Address,
                                  OlusturmaTarihi = data.CreatedDate,
                                  OlusturanKullanici = data.CreatedBy,
                                  GuncellemeTarihi = data.UpdatedDate,
                                  GuncelleyenKullanici = data.UpdatedBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Musteriler.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grid.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Kimlik No,Müşteri Adı,Email,Telefon,Faks,Web Sitesi,Adres,Oluşturma Tarihi,Oluşturan Kullanıcı,Güncelleme Tarihi,Güncelleyen Kullanıcı");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Musteriler.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            var customer = db.Customers;
            foreach (var customers in customer)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    customers.Id,
                    customers.Name,
                    customers.Email,
                    customers.Phone,
                    customers.Fax,
                    customers.WebSite,
                    customers.Address,
                    customers.CreatedDate,
                    customers.CreatedBy,
                    customers.UpdatedDate,
                    customers.UpdatedBy));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
