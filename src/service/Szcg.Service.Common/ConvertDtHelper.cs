using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Szcg.Service.Common
{

    public static class ConvertDtHelper<T> where T : new()
    {
        /// <summary>
        /// 根据参数DataTable 的数据，获得相关Model;
        /// </summary>
        /// <param name="dr">数据</param>
        /// <returns>单个数据Model 如：MemberModel </returns>
        public static T GetModel(DataRow dr)
        {
            T[] result = GetModels(dr.Table.Rows);
            if (result != null && result.Length > 0)
            {
                return result[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 根据参数DataTable 的数据，获得相关Model;
        /// </summary>
        /// <param name="dt">数据</param>
        /// <returns>单个数据Model 如：MemberModel </returns>
        public static T GetModel(DataTable dt)
        {
            List<T> list = GetModelList(dt);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 根据参数DataTable 的数据，获得相关Model 集合;
        /// </summary>
        /// <param name="drs">数据</param>
        /// <returns>数据Models数组 如：MemberModel[];OrderModel[] 等 </returns>
        public static T[] GetModels(DataRowCollection drs)
        {
            List<T> list = GetModelList(drs);
            if (list != null)
            {
                return list.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 根据参数DataTable 的数据，获得相关Model 集合;
        /// </summary>
        /// <param name="drs">数据(dt.Rows)</param>
        /// <returns>数据Model集合</returns>
        public static List<T> GetModelList(DataRowCollection drs)
        {
            Dictionary<string, PropertyInfo> dict = new Dictionary<string, PropertyInfo>();
            List<T> list = new List<T>();

            //将反射的字段存放到字典中
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                dict.Add(property.Name.ToUpper(), property);
            }

            foreach (DataRow dr in drs)
            {
                T instance = new T();
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    string name = dr.Table.Columns[j].ColumnName.ToUpper();
                    if (dict.ContainsKey(name))
                    {
                        PropertyInfo info = dict[name];
                        SetFieldValue(instance, info, dr[j]);
                    }
                }
                list.Add(instance);
            }

            return list;
        }

        /// <summary>
        /// 根据参数DataTable 的数据，获得相关Model 集合;
        /// </summary>
        /// <param name="dt">数据</param>
        /// <returns></returns>
        public static List<T> GetModelList(DataTable dt)
        {
            List<T> list = new List<T>();
            if (dt == null)
            {
                return list;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                list = GetModelList(dt.Rows);
            }
            return list;
        }

        /// <summary>
        /// 根据传入的实体类转化为字典
        /// </summary>
        /// <param name="t">实体类</param>
        /// <returns>字典</returns>
        public static Dictionary<string, object> ConvertToDict(T t)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.CanRead)
                {
                    object value = property.GetValue(t, null);
                    if (value != null && value.ToString() != "-999")
                    {
                        dict.Add(property.Name, value == null ? string.Empty : value);
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="instance">对象</param>
        /// <param name="info">属性</param>
        /// <param name="value">值</param>
        private static void SetFieldValue(object instance, PropertyInfo info, object value)
        {
            if (!info.CanWrite)
            {
                return;
            }

            if (value == null || value is DBNull)
            {
                info.SetValue(instance, null, null);
            }
            else
            {
                if (info.PropertyType == typeof(double) && value.GetType() != typeof(double))
                {
                    info.SetValue(instance, Convert.ToDouble(value.ToString()), null);
                }
                else if (info.PropertyType == typeof(string) && value.GetType() != typeof(string))
                {
                    info.SetValue(instance, value.ToString(), null);
                }
                else if (info.PropertyType == typeof(int) && value.GetType() != typeof(int))
                {
                    info.SetValue(instance, Convert.ToInt32(value.ToString()), null);
                }
                else if (info.PropertyType == typeof(long) && value.GetType() != typeof(long))
                {
                    info.SetValue(instance, Convert.ToInt64(value.ToString()), null);
                }
                else if (info.PropertyType == typeof(bool) && value.GetType() == typeof(int))
                {
                    info.SetValue(instance, Convert.ToBoolean(value), null);
                }
                else if (info.PropertyType == typeof(decimal) && value.GetType() != typeof(decimal))
                {
                    info.SetValue(instance, Convert.ToDecimal(value), null);
                }
                else
                {
                    info.SetValue(instance, value, null);
                }
            }
        }
    }
}
