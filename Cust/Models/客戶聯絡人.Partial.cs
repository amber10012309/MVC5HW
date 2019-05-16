namespace Cust.Models
{
    using Cust.DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (客戶資料Entities db = new 客戶資料Entities())
            {
                var contact = (客戶聯絡人)validationContext.ObjectInstance;
                var customer = db.客戶資料.Where(c => c.Id == contact.客戶Id && !c.是否已刪除).FirstOrDefault();

                if (customer == null)
                    yield return new ValidationResult("該客戶不存在!");

                if (customer.客戶聯絡人.Any(c => c.Email == this.Email.ToString().Trim() && c.Id != contact.Id))
                {
                    yield return new ValidationResult(string.Format("{0}下已存在 Email 為 {1} 的聯絡人，請另外使用別的 Email！", customer.客戶名稱, this.Email));
                }
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [手機驗證Attribute]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
