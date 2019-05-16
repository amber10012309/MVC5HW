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
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo;
        public 客戶資料Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }
        // GET: 客戶資料
        public ActionResult Index(string sortOrder, string SearchValue, string 分類ListSelect)
        {
            ViewBag.客戶名稱Sort = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";
            ViewBag.統一編號Sort = sortOrder == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewBag.電話Sort = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.傳真Sort = sortOrder == "傳真" ? "傳真_desc" : "傳真";
            ViewBag.地址Sort = sortOrder == "地址" ? "地址_desc" : "地址";
            ViewBag.EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.客戶分類Sort = sortOrder == "客戶分類" ? "客戶分類_desc" : "客戶分類";

            var Custs = String.IsNullOrEmpty(分類ListSelect) ? repo.All() : repo.Find分類(分類ListSelect);

            if (!String.IsNullOrEmpty(SearchValue))
            {
                Custs = Custs.Where(c => c.客戶名稱.Contains(SearchValue)
                                       || c.統一編號.Contains(SearchValue)
                                       || c.電話.Contains(SearchValue)
                                       || c.傳真.Contains(SearchValue)
                                       || c.地址.Contains(SearchValue)
                                       || c.Email.Contains(SearchValue)
                                       );
            }

            ViewData["分類List"] = repo.Get客戶分類List();

            switch (sortOrder)
            {
                case "客戶名稱_desc":
                    Custs = Custs.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    Custs = Custs.OrderBy(s => s.統一編號);
                    break;
                case "統一編號_desc":
                    Custs = Custs.OrderByDescending(s => s.統一編號);
                    break;

                case "電話":
                    Custs = Custs.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    Custs = Custs.OrderByDescending(s => s.電話);
                    break;
                case "傳真":
                    Custs = Custs.OrderBy(s => s.傳真);
                    break;
                case "傳真_desc":
                    Custs = Custs.OrderByDescending(s => s.傳真);
                    break;
                case "地址":
                    Custs = Custs.OrderBy(s => s.地址);
                    break;
                case "地址_desc":
                    Custs = Custs.OrderByDescending(s => s.地址);
                    break;
                case "Email":
                    Custs = Custs.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    Custs = Custs.OrderByDescending(s => s.Email);
                    break;
                case "客戶分類":
                    Custs = Custs.OrderBy(s => s.客戶分類);
                    break;
                case "客戶分類_desc":
                    Custs = Custs.OrderByDescending(s => s.客戶分類);
                    break;
                default:
                    Custs = Custs.OrderBy(s => s.客戶名稱);
                    break;
            }

            TempData["ExcelModel"] = Custs.ToList();

            return View(Custs.ToList());
        }

        public ActionResult FileSave()
        {
            if (((List<客戶資料>)TempData["ExcelModel"]).Count > 0)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    ExcelExport exl = new ExcelExport();
                    exl.Export((List<客戶資料>)TempData["ExcelModel"], stream);
                    TempData.Keep("ExcelModel");
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel.xlsx");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.是否已刪除 = false;
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶資料);
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
