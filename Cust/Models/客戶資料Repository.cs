using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace Cust.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.是否已刪除);
        }

        public void Update(客戶資料 entity)
        {
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            this.UnitOfWork.Commit();
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified; 
            this.UnitOfWork.Commit();
        }

        public List<SelectListItem> Get客戶分類List()
        {
            return (from c in this.All()
                    select new SelectListItem { Text = c.客戶分類, Value = c.客戶分類 }).Distinct().ToList();
        }
        public IQueryable<客戶資料> Find分類(string CustClass)
        {
            return this.All().Where(p => p.客戶分類 == CustClass);
        }

    }

    public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}