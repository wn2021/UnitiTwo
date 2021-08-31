using Business;
using Model;
using Model.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TreeGrid_CellEdit()
        {
            return View();
        }

        [HttpPost]
        [Display(Description = "根据用户或权限组获取可选菜单")]
        public string GetTasks()
        {

            List<V_tasks> list = new BLTest().GetTasks();
            if (list == null) { list = new List<V_tasks>(); }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
            //return Json(new { data = list }, JsonRequestBehavior.AllowGet);
            //return View(list);
        }

        [HttpGet]
        public ActionResult Cloumn_Chart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetColumnChartData(string param) {
            cloumn_chart chart = new cloumn_chart();
            if (param == "001") {
                chart = this.GetLineChart();
            }
            else
            {
                cloumn_chart_title title = new cloumn_chart_title();
                title.text = "世界人口总量";
                chart.title = title;
                cloumn_chart_legend legend = new cloumn_chart_legend();
                legend.data = new string[] { "2011年", "2012年" };
                chart.legend = legend;

                cloumn_chart_xAxis xAxis = new cloumn_chart_xAxis();
                xAxis.type = "value";
                xAxis.boundaryGap = new decimal[] { 0, new decimal(0.01) };
                chart.xAxis = xAxis;

                cloumn_chart_yAxis yAxis = new cloumn_chart_yAxis();
                yAxis.type = "category";
                yAxis.data = new string[] { "巴西", "印尼", "美国", "印度", "中国", "世界人口(万)" };
                chart.yAxis = yAxis;

                cloumn_chart_tooltip tooltip = new cloumn_chart_tooltip();
                tooltip.trigger = "axis";
                axisPointer tooltip_axisPointer = new axisPointer();
                tooltip_axisPointer.type = "shadow";
                tooltip.axisPointer = tooltip_axisPointer;
                chart.tooltip = tooltip;

                List<cloumn_chart_series> lst = new List<cloumn_chart_series>();
                cloumn_chart_series ser = new cloumn_chart_series();
                ser.data = new decimal[] { 18203, 23489, 29034, 104970, 131744, 630230 };
                ser.name = "2011年";
                ser.type = "bar";
                lst.Add(ser);
                ser = new cloumn_chart_series();
                ser.data = new decimal[] { 19325, 23438, 31000, 121594, 134141, 681807 };
                ser.name = "2012年";
                ser.type = "bar";
                lst.Add(ser);
                chart.series = lst;
            }
            return Json(new { data = chart }, JsonRequestBehavior.AllowGet);
        }
        private cloumn_chart GetLineChart() {
            cloumn_chart chart = new cloumn_chart();
            cloumn_chart_title title = new cloumn_chart_title();
            title.text = "周访问量渠道汇总";
            chart.title = title;
            cloumn_chart_legend legend = new cloumn_chart_legend();
            legend.data = new string[] { "邮件营销", "联盟广告", "视频广告", "直接访问", "搜索引擎" };
            chart.legend = legend;

            cloumn_chart_xAxis xAxis = new cloumn_chart_xAxis();
            xAxis.type = "category";
            xAxis.data= new string[] { "周一", "周二", "周三", "周四", "周五", "周六" ,"周日"};
            //xAxis.boundaryGap = new decimal[] { 0, new decimal(0.01) };
            chart.xAxis = xAxis;

            cloumn_chart_yAxis yAxis = new cloumn_chart_yAxis();
            yAxis.type = "value";
            chart.yAxis = yAxis;

            cloumn_chart_tooltip tooltip = new cloumn_chart_tooltip();
            axisPointer tooltip_axisPointer = new axisPointer();
            tooltip_axisPointer.type = "cross";
            axisPointer.label lbl = new axisPointer.label();
            lbl.backgroundColor = "#6a7985";
            tooltip_axisPointer.lbl = lbl;
            tooltip.axisPointer = tooltip_axisPointer;
            tooltip.trigger = "axis";
            chart.tooltip = tooltip;

            List<cloumn_chart_series> lst = new List<cloumn_chart_series>();
            
            cloumn_chart_series.series_label ser_lbl = new cloumn_chart_series.series_label();
            cloumn_chart_series.series_label.sl_normal ser_lbl_normal = new cloumn_chart_series.series_label.sl_normal();
            ser_lbl_normal.show = true;
            ser_lbl_normal.position = "top";
            ser_lbl.normal = ser_lbl_normal;

            cloumn_chart_series.series_areaStyle sas = new cloumn_chart_series.series_areaStyle();
            cloumn_chart_series.series_areaStyle.sa_normal sas_noraml = new cloumn_chart_series.series_areaStyle.sa_normal();
            sas.normal = sas_noraml;

            cloumn_chart_series ser = new cloumn_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 120, 132, 101, 134, 90, 230, 210 };
            ser.name = "邮件营销";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new cloumn_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 220, 182, 191, 234, 290, 330, 310 };
            ser.name = "联盟广告";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new cloumn_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 150, 232, 201, 154, 190, 330, 410 };
            ser.name = "视频广告";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new cloumn_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 320, 332, 301, 334, 390, 330, 320 };
            ser.name = "直接访问";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            ser = new cloumn_chart_series();
            ser.label = ser_lbl;
            ser.areaStyle = sas;
            ser.data = new decimal[] { 820, 932, 901, 934, 1290, 1330, 1320 };
            ser.name = "搜索引擎";
            ser.type = "line";
            ser.stack = "总量";
            lst.Add(ser);
            chart.series = lst;
            return chart;
        }
    }
}