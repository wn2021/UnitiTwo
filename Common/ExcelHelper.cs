
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ExcelHelper
    {
        public static MemoryStream RenderToExcel(string[] captions, DataTable table, string fileName)
        {

            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                {
                    IRow headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < captions.Length; i++)
                    {
                        headerRow.CreateCell(i).SetCellValue(captions[i]);
                    }
                    int rowIndex = 1;
                    foreach (DataRow row in table.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }
                        rowIndex++;
                    }
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                }
                FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Close();
                return ms;
            }

        }
        /// <summary>
        /// 根据List导出数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="captions"></param>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel<T>(string[] captions, IList<T> list, string fileName) {
            return RenderToExcel(captions, list, fileName, false);
        }
        /// <summary>
        /// 根据List导出数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MemoryStream RenderToExcel<T>(string[] captions, IList<T> list, string fileName,bool withSum)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                Type t = typeof(T);
                PropertyInfo[] properties = t.GetProperties();
                IWorkbook workbook = new HSSFWorkbook();
                ICellStyle decimalStyle = workbook.CreateCellStyle();  //首先建单元格格式
                ICellStyle cellStyle = workbook.CreateCellStyle();  //首先建单元格格式
                IFont font = workbook.CreateFont();
                font.Boldweight = short.MaxValue;
                ISheet sheet = workbook.CreateSheet();
                {
                    //创建表头
                    IRow headerRow = sheet.CreateRow(0);
                    if (captions != null && captions.Length > 0)
                    {
                        for (int i = 0; i < captions.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(captions[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(properties[i].Name);
                        }
                    }
                    //填充内容
                    int rowIndex = 1;
                    int sumIndex = 0;//合计行列索引
                    foreach (T row in list)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        for (int j = 0; j < properties.Length; j++)
                        {
                            if (properties[j].GetValue(row) == null)
                            {
                                continue;
                            }
                            if (withSum && rowIndex == list.Count)
                            {
                                cellStyle.SetFont(font);
                                cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                                // 合计行
                                if (sumIndex == 0)
                                {
                                    sumIndex = j;
                                    //合并单元格时默认保存左上角的值
                                    ICell MyCell = dataRow.CreateCell(0);
                                    MyCell.SetCellValue(properties[j].GetValue(row).ToString());
                                    MyCell.CellStyle = cellStyle;
                                    //合并单元格（起始行，终止行，起始列，终止列）
                                    sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, sumIndex));
                                }
                                else
                                {
                                    ICell MyCell = dataRow.CreateCell(j);
                                    MyCell.SetCellValue(properties[j].GetValue(row).ToString());
                                    MyCell.CellStyle = cellStyle;
                                }
                            }
                            else
                            {
                                //非合计行
                                if (properties[j].PropertyType.FullName.Contains("Decimal"))
                                {
                                    decimalStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT;
                                    ICell MyCell = dataRow.CreateCell(j);
                                    MyCell.SetCellValue(double.Parse(properties[j].GetValue(row).ToString()));
                                    MyCell.CellStyle = decimalStyle;
                                    //dataRow.CreateCell(j).SetCellValue(double.Parse(properties[j].GetValue(row).ToString()));
                                }
                                else  {
                                    dataRow.CreateCell(j).SetCellValue(properties[j].GetValue(row).ToString());
                                }
                            }
                        }
                        rowIndex++;
                    }
                    for (int j = 0; j < properties.Length; j++)
                    {
                        sheet.AutoSizeColumn(j);//列宽自适应
                    }
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                }
                FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
                workbook.Write(file);
                file.Close();
                return ms;
            }
            catch(Exception ex) {
                return ms;
            }
            finally {  }
        }
    }
}
