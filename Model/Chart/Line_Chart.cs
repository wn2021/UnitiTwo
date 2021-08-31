using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class line_chart
    {
        /// <summary>
        /// 颜色系
        /// </summary>
        public string[] color { get; set; }
        //标题
        public line_chart_title title {get;set;}
        //图例
        public line_chart_legend legend { get; set; }
        //提示文字
        public line_chart_tooltip tooltip { get; set; }
        //横坐标
        public line_chart_xAxis xAxis { get; set; }
        //纵坐标
        public line_chart_yAxis yAxis { get; set; }
        //数据
        public List<line_chart_series> series { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public class line_chart_title
        {
            /// <summary>
            /// 大标题
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// 小标题
            /// </summary>
            public string subtext { get; set; }
            public string x { get; set; }
        }
        /// <summary>
        /// 图例
        /// </summary>
        public class line_chart_legend
        {
            public string[] data { get; set; }
        }
        /// <summary>
        /// 提示文字
        /// </summary>
        public class line_chart_tooltip
        {
            /// <summary>
            ///  触发方式'item':数据项图形触发，主要在散点图，饼图等无类目轴的图表中使用
            ///  'axis':坐标轴触发，主要在柱状图，折线图等会使用类目轴的图表中使用
            /// </summary>
            public string trigger { get; set; }
            /// <summary>
            /// 文本对齐方式
            /// </summary>
            public string textStyle { get; set; }
            public lct_axisPointer axisPointer { get; set; }
            public class lct_axisPointer
            {
                public string type { get; set; }
                public lcta_label label { get; set; }
                public class lcta_label
                {
                    public string backgroundColor { get; set; }
                }
            }
        }
        /// <summary>
        /// 横坐标
        /// </summary>
        public class line_chart_xAxis
        {
            /// <summary>
            /// category还是value
            /// </summary>
            public string type { get; set; }
            public string[] data { get; set; }
            /// <summary>
            /// 间距
            /// </summary>
            public bool boundaryGap { get; set; }
        }
        /// <summary>
        /// 纵座标
        /// </summary>
        public class line_chart_yAxis
        {
            /// <summary>
            /// category还是value
            /// </summary>
            public string type { get; set; }
            public string[] data { get; set; }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public class line_chart_series
        {
            public string name { get; set; }
            public string stack { get; set; }
            public string type { get; set; }
            public decimal[] data { get; set; }
            public series_areaStyle areaStyle { get; set; }
            public series_label label { get; set; }
            public class series_areaStyle
            {
                public sa_normal normal { get; set; }
                public class sa_normal { }
            }
            public class series_label
            {
                public sl_normal normal { get; set; }
                public class sl_normal
                {
                    /// <summary>
                    /// 是否标注每项的值
                    /// </summary>
                    public bool show { get; set; }
                    /// <summary>
                    /// 值的位置
                    /// </summary>
                    public string position { get; set; }
                }
            }
        }
    }
}
