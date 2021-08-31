using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Chart
{
    public class cloumn_chart {
        /// <summary>
        /// 标题
        /// </summary>
        public cloumn_chart_title title { get; set; }
        /// <summary>
        /// 提示框
        /// </summary>
        public cloumn_chart_tooltip tooltip { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public cloumn_chart_legend legend { get; set; }
        /// <summary>
        /// 网格线
        /// </summary>
        public cloumn_chart_grid grid { get; set; }
        /// <summary>
        /// 横坐标
        /// </summary>
        public cloumn_chart_xAxis xAxis { get; set; }
        /// <summary>
        /// 纵坐标
        /// </summary>
        public cloumn_chart_yAxis yAxis { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<cloumn_chart_series> series { get; set; }              
    }
    public class cloumn_chart_series
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
                public bool show { get; set; }
                public string position { get; set; }
            }
        }
    }
    public class cloumn_chart_yAxis {
        public string type { get; set; }
        public string[] data { get; set; }
    }
    public class cloumn_chart_xAxis
    {
        public string type { get; set; }
        public string[] data { get; set; }
        /// <summary>
        /// 间距
        /// </summary>
        public decimal[] boundaryGap { get; set; }
    }
    public class cloumn_chart_grid {
        public string left { get; set; }
        public string right { get; set; }

        public string bottom { get; set; }

        public bool containLabel { get; set; }
    }
    /// <summary>
    /// 图例，说明，解释
    /// </summary>
    public class cloumn_chart_legend {
        public string[] data { get; set; }
    }
    /// <summary>
    /// 提示框
    /// </summary>
    public class cloumn_chart_tooltip
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
        public axisPointer axisPointer { get; set; }
    }
    /// <summary>
    /// 图标标题
    /// </summary>
    public class cloumn_chart_title {
        public string text { get; set; }
        public string subtext { get; set; }
    }
    public class axisPointer {
        public string type { get; set; }
        public label lbl { get; set; }
        public class label
        {
            public string backgroundColor { get; set; }
        }
    }
}
