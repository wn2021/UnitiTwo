using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class pie_chart
    {
        public pie_chart_title title { get; set; }
        public pie_chart_tooltip tooltip { get; set; }
        public pie_chart_legend legend { get; set; }
        public pie_chart_series series { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public class pie_chart_title
        {
            public string text { get; set; }
            public string subtext { get; set; }
            public string x { get; set; }
        }
        public class pie_chart_tooltip {
            public string trigger { get; set; }
            public string formatter { get; set; }
        }
        /// <summary>
        /// 图例
        /// </summary>
        public class pie_chart_legend {
            public string orient { get; set; }
            public string left { get; set; }
            public string[] data { get; set; }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public class pie_chart_series {
            public string name { get; set; }
            public string type { get; set; }
            public string radius { get; set; }
            public string[] center { get; set; }
            public List<series_data> data { get; set; }
            public series_itemStyle itemStyle { get; set; }
            public class series_data {
                public decimal value { get; set; }
                public string name { get; set; }
            }
            public class series_itemStyle {
                public si_emphasis emphasis { get; set; }
                public class si_emphasis{
                    public decimal shadowBlur { get; set; }
                    public decimal shadowOffsetX { get; set; }
                    public string shadowColor { get; set; }
                }
            }
        }
        
    }
}
