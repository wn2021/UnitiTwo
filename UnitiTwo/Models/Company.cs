using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitiTwo.Models
{
    public class Company
    {
        public int id { get; set; }
        public string CompanyName { get; set; } //公司名
        public string Address { get; set; } //地址
        public string mobile { get; set; }  //电话
        public string link { get; set; } //联系人
        public DateTime intodate { get; set; } //注册时间
    }

    public class Notice {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Public_Date { get; set; }
        public int Content_Type { get; set; }
        public string Content { get; set; }
        public int ReadId { get; set; }
    }
}