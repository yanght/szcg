/* ****************************************************************************************
 * 版权所有：杭州天夏科技
 * 用    途：综合评价责任单位模块逻辑层
 * 结构组成：
 * 作    者：yannis
 * 创建日期：2007-06-10
 * 历史记录：
 * ****************************************************************************************
 * 修改人员：               
 * 修改日期： 
 * 修改说明：   
 * ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using bacgDL.zhpj;
using Teamax.Common;
using bacgDL.zhpj.departappraise;

namespace bacgBL.zhpj
{
    public class DepartAppraise
    {

        public DataTable getDepartAppraise(int departcode, int type, int year, int number, string field, string order)
        {
            StructQuery sq=new StructQuery();
            try
            {
                using (bacgDL.zhpj.departappraise.DepartAppraise dl = new bacgDL.zhpj.departappraise.DepartAppraise())
                {
                    sq.intYears = year;
                    if(type == 0)
                       sq.intWeeks = number;
                    if (type == 1)
                       sq.intMonths = number;
                    if (type == 2)
                       sq.intQuarter = number;
                    if (type == 3)
                       sq.intYears = year;
                    SetStatDate(sq);
                    return dl.GetDepartData(departcode, year, type, number, sq.startDate, sq.endDate, field, order);

                    //if (type == 0)    //时段类型为周
                    //{
                    //    sq.intYears = year;
                    //    sq.intWeeks = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDataByWeek(departcode, year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 1)    //时段类型为月
                    //{
                    //    sq.intYears = year;
                    //    sq.intMonths = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDataByMonth(departcode, year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else if (type == 2)    //时段类型为季度
                    //{
                    //    sq.intYears = year;
                    //    sq.intQuarter = number;
                    //    SetStatDate(sq);
                    //    return dl.GetDataByQuarter(departcode, year, type, number, sq.startDate, sq.endDate);
                    //}
                    //else    //时段类型为年
                    //{
                    //    sq.intYears = year;
                    //    SetStatDate(sq);
                    //    return dl.GetDataByYear(departcode, year, type, number, sq.startDate, sq.endDate);
                    //}
                    //return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetStatDate(StructQuery sq)
        {
            DateTime startDate, endDate;
            if (sq.intMonths != 0)
            {
                startDate = new DateTime(sq.intYears, sq.intMonths, 1);
                if (sq.intMonths != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intMonths + 1, 1);
                    endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intQuarter != 0)
            {
                startDate = new DateTime(sq.intYears, (sq.intQuarter - 1) * 3 + 1, 1);

                if (sq.intQuarter * 3 != 12)
                {
                    endDate = new DateTime(sq.intYears, sq.intQuarter * 3 + 1, 1);
                    endDate.AddDays(-1);
                }
                else
                {
                    endDate = new DateTime(sq.intYears, 12, 31);
                }
            }
            else if (sq.intWeeks != 0)
            {
                DateTime dt = new DateTime(sq.intYears, 1, 1);
                int weeknow = Convert.ToInt32(dt.DayOfWeek);
                startDate = dt.AddDays(-1 * weeknow + (sq.intWeeks - 1) * 7 + 1);
                endDate = dt.AddDays(-1 * weeknow + (sq.intWeeks) * 7);
            }
            else
            {
                startDate = new DateTime(sq.intYears, 1, 1);
                endDate = new DateTime(sq.intYears, 12, 31);
            }
            sq.startDate = startDate;
            sq.endDate = endDate;
        }

        public DataTable ProjectStatOutPut(int departcode, int type, int year, int number,string field, string order)
        {
            DataTable dt = getDepartAppraise(departcode, type, year, number, field, order).Copy();
            dt.Columns.Remove("parentcode");
            dt.Columns.Remove("departcode");
            StructQuery sq = new StructQuery();
            sq.intYears = year;
            if (type == 0)
                sq.intWeeks = number;
            if (type == 1)
                sq.intMonths = number;
            if (type == 2)
                sq.intQuarter = number;
            if (type == 3)
                sq.intYears = year;
            SetStatDate(sq);

            dt.TableName = "评价统计  统计周期： " + sq.startDate.ToShortDateString() + "至" + sq.endDate.ToShortDateString();
            return dt;
        }

        #region GetDepartList:根据模块大类编码，获取相应的部门数据集
        /// <summary>
        /// 根据模块大类编码，获取相应的部门数据集
        /// </summary>
        /// <param name="bigModelId">哪类模块编码</param>
        /// <param name="strErr">输出错误信息</param>
        /// <returns></returns>
        public static DataTable GetDepartList(string bigModelId, ref string strErr)
        {
            try
            {
                using (bacgDL.zhpj.departappraise.DepartAppraise dl = new bacgDL.zhpj.departappraise.DepartAppraise())
                {
                    return dl.GetDepartList(bigModelId);
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
