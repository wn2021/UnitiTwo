using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        //产品名称
        public string pro_name { get; set; }
        //品牌名称
        public string pro_brand { get; set; }
        //产地
        public string pro_place { get; set; }
        //规格
        public string pro_purity{ get; set; }
        //最小起订量
        public string pro_min { get; set; }
        //所在仓库
        public string pro_depot { get; set; }
        //数量
        public int pro_num { get; set; }
        //图片链接
        public string pro_img { get; set; }
        //单价
        public decimal pro_price { get; set; }
    }
}
