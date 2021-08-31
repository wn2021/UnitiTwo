using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VueWeb.Helper
{
    public class BaseDataPackage<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}