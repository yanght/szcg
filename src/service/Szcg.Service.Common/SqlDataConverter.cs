using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szcg.Service.Common
{
    public class SqlDataConverter
    {
        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        public static short ToInt16(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        public static int ToInt32(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static long ToInt64(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static double ToDouble(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public static Boolean ToBoolean(object obj)
        {
            return obj != null && !(obj is DBNull) && Convert.ToBoolean(obj);
        }

        public static byte[] ToBytes(object obj)
        {
            return obj == null || obj is DBNull ? new byte[0] : (byte[]) obj;
        }

        public static Guid ToGuid(object obj)
        {
            if (obj == null || obj is DBNull)
            {
                return Guid.Empty;
            }
            else
            {
                return new Guid(obj.ToString());
            }
        }

        public static string ToString(object obj)
        {
            return ToString(obj, false);
        }

        public static string ToString(object obj, bool toLower)
        {
            if (obj == null || obj is DBNull)
            {
                return string.Empty;
            }
            else
            {
                string s = Convert.ToString(obj);
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                else
                {
                    return toLower ? s.ToLower() : s;
                }
            }
        }
    }
}
