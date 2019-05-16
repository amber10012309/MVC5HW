using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cust.Models;

namespace Cust.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶聯絡人Repository repo;
        public 客戶聯絡人Controller()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
        }
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶聯絡人
        public ActionResult Index(string sortOrder, string SearchValue)
        {
            ViewBag.客戶名稱Sort = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";
            ViewBag.職稱Sort = sortOrder == "職稱" ? "職稱_desc" : "職稱";
            ViewBag.姓名Sort = sortOrder == "姓名" ? "姓名_desc" : "姓名";
            ViewBag.手機Sort = sortOrder == "手機" ? "手機_desc" : "手機";
            ViewBag.電話Sort = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";

            var CustsContact = repo.Where職稱(SearchValue);

            switch (sortOrder)
            {
                case "客戶名稱_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "職稱":
                    CustsContact = CustsContact.OrderBy(s => s.職稱);
                    break;
                case "職稱_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.職稱);
                    break;
                case "姓名":
                    CustsContact = CustsContact.OrderBy(s => s.姓名);
                    break;
                case "姓名_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.姓名);
                    break;
                case "手機":
                    CustsContact = CustsContact.OrderBy(s => s.手機);
                    break;
                case "手機_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.手機);
                    break;
                case "電話":
                    CustsContact = CustsContact.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.電話);
                    break;
                case "Email":
                    CustsContact = CustsContact.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    CustsContact = CustsContact.OrderByDescending(s => s.Email);
                    break;
                default:
                    CustsContact = CustsContact.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
            }

            TempData["ExcelModel"] = CustsContact.ToList();
            return View(CustsContact);
        }

        public ActionResult FileSave()
        {
            if (((List<客戶聯絡人>)TempData["ExcelModel"]).Count > 0)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    ExcelExport exl = new ExcelExport();
                    exl.Export((List<客戶聯絡人>)TempData["ExcelModel"], stream);
                    TempData.Keep("ExcelModel");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel.xlsx");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            db.客戶聯絡人.Remove(客戶聯絡人);
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
