using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class radar_map
    {
        public radar_map_title title { get; set; }
        public radar_map_legend legend { get; set; }
        public radar_map_radar radar { get; set; }
        public radar_map_series series { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public class radar_map_title
        {
            public string text { get; set; }
            public string subtext { get; set; }
            public string x { get; set; }
        }
        /// <summary>
        /// 图例
        /// </summary>
        public class radar_map_legend
        {
            public string[] data { get; set; }
        }
        /// <summary>
        /// 雷达
        /// </summary>
        public class radar_map_radar
        {
            public radar_name name { get; set; }
            public List<rader_indicator> indicator { get; set; }
            public class radar_name
            {
                /// <summary>
                /// 文本样式
                /// </summary>
                public radar_name_textStyle textStyle { get; set; }
                public class radar_name_textStyle
                {
                    public string color { get; set; }
                    public string backgroundColor { get; set; }
                    public decimal borderRadius { get; set; }
                    public decimal[] padding { get; set; }
                }
            }
            /// <summary>
            /// 指标
            /// </summary>
            public class rader_indicator
            {
                public string name { get; set; }
                public decimal max { get; set; }
            }
        }
        /// <summary>
        /// 雷达数据
        /// </summary>
        public class radar_map_series
        {
            public string name { get; set; }
            public string type { get; set; }
            public List<series_data> data { get; set; }
            public series_label label { get; set; }
            public class series_data
            {
                public decimal[] value { get; set; }
                public string name { get; set; }
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
