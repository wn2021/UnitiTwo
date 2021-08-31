using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Calendar
    {
        public static async Task<string> IsHolidayByDate(DateTime date)
        {
            var isHoliday = false;
            var webClient = new System.Net.WebClient();
            var PostVars = new System.Collections.Specialized.NameValueCollection
             {
                 { "d", date.ToString("yyyyMMdd") }//参数
             };
            try
            {
                var day = date.DayOfWeek;

                //判断是否为周末
                if (day == DayOfWeek.Sunday || day == DayOfWeek.Saturday)
                    return "1";

                //0为工作日，1为周末，2为法定节假日
                var byteResult = await webClient.UploadValuesTaskAsync("http://tool.bitefu.net/jiari/", "POST", PostVars);//请求地址,传参方式,参数集合
                var result = Encoding.UTF8.GetString(byteResult);//获取返回值
                return result;
            }
            catch(Exception ex)
            {
                string errMsg=ex.Message;
                isHoliday = false;
            }
            return "0";
        }
    }
}
