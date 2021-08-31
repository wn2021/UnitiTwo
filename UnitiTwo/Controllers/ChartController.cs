using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Cloumn_Chart()
        {
            return View();
        }
        #region 线型图
        [HttpGet]
        public ActionResult LineChart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetLineChart(string param)
        {
            line_chart chart = new line_chart();
            line_chart.line_chart_title title = new line_chart.line_chart_title();            
            title.text = "周访问量渠道汇总";
            if (param == "001") { title.text = "薪资统计表"; }
            //title.x = "center";
            chart.title = title;

            line_chart.line_chart_legend legend = new line_chart.line_chart_legend();
            legend.data = new string[] { "邮件营销", "联盟广告", "视频广告", "直接访问", "搜索引擎" };
            if (param == "001") { legend.data = new string[] { "工资", "奖金" }; }
            chart.legend = legend;

            line_chart.line_chart_xAxis xAxis = new line_chart.line_chart_xAxis();
            xAxis.type = "category";
            xAxis.data = new string[] { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };
            if (param == "001") { xAxis.data = new string[] { "10月", "11月", "12月" }; }
            xAxis.boundaryGap = false;
            chart.xAxis = xAxis;

            line_chart.line_chart_yAxis yAxis = new line_chart.line_chart_yAxis();
            yAxis.type = "value";
            chart.yAxis = yAxis;

            line_chart.line_chart_tooltip tooltip = new line_chart.line_chart_tooltip();
            line_chart.line_chart_tooltip.lct_axisPointer axisPointer = new line_chart.line_chart_tooltip.lct_axisPointer();
            axisPointer.type = "cross";
            line_chart.line_chart_tooltip.lct_axisPointer.lcta_label label = new line_chart.line_chart_tooltip.lct_axisPointer.lcta_label();
            label.backgroundColor = "#6a7985";
            axisPointer.label = label;
            tooltip.axisPointer = axisPointer;
            tooltip.trigger = "axis";
            chart.tooltip = tooltip;

            List<line_chart.line_chart_series> lst = new List<line_chart.line_chart_series>();

            line_chart.line_chart_series.series_label ser_lbl = new line_chart.line_chart_series.series_label();
            line_chart.line_chart_series.series_label.sl_normal ser_lbl_normal = new line_chart.line_chart_series.series_label.sl_normal();
            ser_lbl_normal.show = true;
            ser_lbl_normal.position = "top";
            ser_lbl.normal = ser_lbl_normal;

            line_chart.line_chart_series.series_areaStyle sas = new line_chart.line_chart_series.series_areaStyle();
            line_chart.line_chart_series.series_areaStyle.sa_normal sas_noraml = new line_chart.line_chart_series.series_areaStyle.sa_normal();
            sas.normal = sas_noraml;

            line_chart.line_chart_series ser = new line_chart.line_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 120, 132, 101, 134, 90, 230, 210 };
            ser.name = "邮件营销";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new line_chart.line_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 220, 182, 191, 234, 290, 330, 310 };
            ser.name = "联盟广告";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new line_chart.line_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 150, 232, 201, 154, 190, 330, 410 };
            ser.name = "视频广告";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new line_chart.line_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 320, 332, 301, 334, 390, 330, 320 };
            ser.name = "直接访问";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new line_chart.line_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 820, 932, 901, 934, 1290, 1330, 1320 };
            ser.name = "搜索引擎";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            if (param == "001") {
                lst = new List<line_chart.line_chart_series>();
                ser = new line_chart.line_chart_series();
                ser.label = ser_lbl;
                ser.areaStyle = sas;
                ser.data = new decimal[] { 820, 932, 901 };
                ser.name = "工资";
                ser.type = "line";
                ser.stack = "合计";
                lst.Add(ser);
                ser = new line_chart.line_chart_series();
                ser.label = ser_lbl;
                ser.areaStyle = sas;
                ser.data = new decimal[] { 1820, 1932, 2901 };
                ser.name = "奖金";
                ser.type = "line";
                ser.stack = "合计";
                lst.Add(ser);
            }
            chart.series = lst;
            return Json(new { data = chart }, JsonRequestBehavior.AllowGet);
        }
        #endregion 线型图
        #region 饼图
        [HttpGet]
        public ActionResult PieChart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetPieChart(string param)
        {
            pie_chart chart = new pie_chart();

            pie_chart.pie_chart_title title = new pie_chart.pie_chart_title();
            title.text = "某站点用户访问来源";
            title.subtext = "纯属虚构";
            title.x = "center";
            chart.title = title;

            pie_chart.pie_chart_tooltip tooltip = new pie_chart.pie_chart_tooltip();
            tooltip.formatter = "{a} <br/>{b} : {c} ({d}%)";
            tooltip.trigger = "item";
            chart.tooltip = tooltip;

            pie_chart.pie_chart_legend legend = new pie_chart.pie_chart_legend();
            legend.orient = "vertical";
            legend.left = "left";
            legend.data = new string[] { "直接访问", "邮件营销", "联盟广告", "视频广告", "搜索引擎" };
            chart.legend = legend;

            pie_chart.pie_chart_series series = new pie_chart.pie_chart_series();
            series.name = "访问来源";
            series.radius = "55%";
            series.type = "pie";
            series.center = new string[] { "50%", "60%" };
            List<pie_chart.pie_chart_series.series_data> lst = new List<pie_chart.pie_chart_series.series_data>();
            pie_chart.pie_chart_series.series_data sd = new pie_chart.pie_chart_series.series_data();
            sd.name = "直接访问";
            sd.value = 335;
            lst.Add(sd);
            sd = new pie_chart.pie_chart_series.series_data();
            sd.name = "邮件营销";
            sd.value = 310;
            lst.Add(sd);
            sd = new pie_chart.pie_chart_series.series_data();
            sd.name = "联盟广告";
            sd.value = 234;
            lst.Add(sd);
            sd = new pie_chart.pie_chart_series.series_data();
            sd.name = "视频广告";
            sd.value = 135;
            lst.Add(sd);
            sd = new pie_chart.pie_chart_series.series_data();
            sd.name = "搜索引擎";
            sd.value = 1548;
            lst.Add(sd);
            series.data = lst;
            pie_chart.pie_chart_series.series_itemStyle itemStyle = new pie_chart.pie_chart_series.series_itemStyle();
            pie_chart.pie_chart_series.series_itemStyle.si_emphasis emphasis = new pie_chart.pie_chart_series.series_itemStyle.si_emphasis();
            emphasis.shadowBlur = 10;
            emphasis.shadowOffsetX = 0;
            emphasis.shadowColor = "rgba(0, 0, 0, 0.5)";
            itemStyle.emphasis = emphasis;
            series.itemStyle = itemStyle;
            chart.series = series;

            return Json(new { data = chart }, JsonRequestBehavior.AllowGet);
        }
        #endregion 饼图
        #region 雷达图
        [HttpGet]
        public ActionResult RadarMap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetRadarMap(string param) {
            radar_map map = new radar_map();
            radar_map.radar_map_title title = new radar_map.radar_map_title();
            title.text = "预算达成情况";
            title.subtext = "纯属虚构";
            map.title = title;
            radar_map.radar_map_legend legend = new radar_map.radar_map_legend();
            legend.data = new string[] { "预算分配（Allocated Budget）", "实际开销（Actual Spending）" };
            map.legend = legend;
            radar_map.radar_map_radar radar = new radar_map.radar_map_radar();
            radar_map.radar_map_radar.radar_name name = new radar_map.radar_map_radar.radar_name();
            radar_map.radar_map_radar.radar_name.radar_name_textStyle textStyle = new radar_map.radar_map_radar.radar_name.radar_name_textStyle();
            textStyle.color = "#fff";
            textStyle.backgroundColor = "#999";
            textStyle.borderRadius = 3;
            textStyle.padding = new decimal[] { 3, 5 };
            name.textStyle = textStyle;
            radar.name = name;
            List<radar_map.radar_map_radar.rader_indicator> indicators = new List<radar_map.radar_map_radar.rader_indicator>();
            radar_map.radar_map_radar.rader_indicator indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "销售（sales）";
            indicator.max = 6500;
            indicators.Add(indicator);
            indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "管理（Administration）";
            indicator.max = 16000;
            indicators.Add(indicator);
            indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "信息技术（Information Techology）";
            indicator.max = 30000;
            indicators.Add(indicator);
            indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "客服（Customer Support）";
            indicator.max = 38000;
            indicators.Add(indicator);
            indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "研发（Development）";
            indicator.max = 52000;
            indicators.Add(indicator);
            indicator = new radar_map.radar_map_radar.rader_indicator();
            indicator.name = "市场（Marketing）";
            indicator.max = 25000;
            indicators.Add(indicator);
            radar.indicator = indicators;
            map.radar = radar;
            radar_map.radar_map_series series = new radar_map.radar_map_series();
            series.name = "预算 vs 开销（Budget vs spending）";
            series.type = "radar";
            radar_map.radar_map_series.series_data data= new radar_map.radar_map_series.series_data();
            List<radar_map.radar_map_series.series_data> lst = new List<radar_map.radar_map_series.series_data>();
            data.name = "预算分配（Allocated Budget）";
            data.value = new decimal[] { 4300, 10000, 28000, 35000, 50000, 19000 };
            lst.Add(data);
            data = new radar_map.radar_map_series.series_data();
            data.name = "实际开销（Actual Spending）";
            data.value = new decimal[] { 5000, 14000, 28000, 31000, 42000, 21000 };
            lst.Add(data);
            series.data = lst;
            radar_map.radar_map_series.series_label label = new radar_map.radar_map_series.series_label();
            radar_map.radar_map_series.series_label.sl_normal normal = new radar_map.radar_map_series.series_label.sl_normal();
            normal.show = true;
            normal.position = "top";
            label.normal = normal;
            series.label = label;
            map.series = series;
            return Json(new { data = map }, JsonRequestBehavior.AllowGet);
        }
        #endregion 雷达图
    }
}