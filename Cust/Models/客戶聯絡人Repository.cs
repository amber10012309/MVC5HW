using System;
using System.Linq;
using System.Collections.Generic;

namespace Cust.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public IQueryable<客戶聯絡人> Where職稱(string 職稱)
        {
            return String.IsNullOrEmpty(職稱) ? base.All() : base.All().Where(p => p.職稱 == 職稱);
        }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}