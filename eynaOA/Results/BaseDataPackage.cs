using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eynaOA.Results
{
    public class BaseDataPackage<T>
    {
        public string result { get; set; }
        public T data { get; set; }


    }
}