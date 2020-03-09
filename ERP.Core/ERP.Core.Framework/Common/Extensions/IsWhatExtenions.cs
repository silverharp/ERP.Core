using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ERP.Core.Framework.Common.Extensions
{
    /// <summary>
    /// 逻辑判段扩展
    /// </summary>
    public static class IsWhatExtenions
    {
        /// <summary>
        /// 值在的范围？
        /// </summary>
        /// <param name="thisValue">值</param>
        /// <param name="begin">大于等于begin</param>
        /// <param name="end">小于等于end</param>
        /// <returns>真假</returns>
        public static bool IsInRange(this int thisValue, int begin, int end)
        {
            return thisValue >= begin && thisValue <= end;
        }
        /// <summary>
        /// 值在的范围？
        /// </summary>
        /// <param name="thisValue">值</param>
        /// <param name="begin">大于等于begin</param>
        /// <param name="end">小于等于end</param>
        /// <returns>真假</returns>
        public static bool IsInRange(this DateTime thisValue, DateTime begin, DateTime end)
        {
            return thisValue >= begin && thisValue <= end;
        }

        /// <summary>
        /// 在里面吗?
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="thisValue">值</param>
        /// <param name="values">值集合</param>
        /// <returns>真假</returns>
        public static bool IsIn<T>(this T thisValue, params T[] values)
        {
            return values.Contains(thisValue);
        }

        /// <summary>
        /// 在里面吗?
        /// </summary> 
        /// <param name="thisValue">值</param>
        /// <param name="inValues">值集合</param>
       /// <returns>真假</returns>
        public static bool IsContainsIn(this string thisValue, params string[] inValues)
        {
            return inValues.Any(thisValue.Contains);
        }

        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsNullOrEmpty(this object thisValue)
        {
            if (thisValue == null || thisValue == DBNull.Value) return true;
            return thisValue.ToString() == "";
        }
        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsNullOrEmpty(this Guid? thisValue)
        {
            if (thisValue == null) return true;
            return thisValue == Guid.Empty;
        }
        /// <summary>
        /// 是null或""?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsNullOrEmpty(this Guid thisValue)
        {
            return thisValue == Guid.Empty;
        }

        /// <summary>
        /// 有值?(与IsNullOrEmpty相反)
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsValuable(this object thisValue)
        {
            if (thisValue == null) return false;
            return thisValue.ToString() != "";
        }
        /// <summary>
        /// 有值?(与IsNullOrEmpty相反)
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsValuable(this IEnumerable<object> thisValue)
        {
            if (thisValue == null || !thisValue.Any()) return false;
            return true;
        }

        /// <summary>
        /// 是零?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsZero(this object thisValue)
        {
            return (thisValue == null || thisValue.ToString() == "0");
        }

        /// <summary>
        /// 是INT?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsInt(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^\d+$");
        }
        /// <summary>
        /// 不是INT?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsNoInt(this object thisValue)
        {
            if (thisValue == null) return true;
            return !Regex.IsMatch(thisValue.ToString(), @"^\d+$");
        }

        /// <summary>
        /// 是金钱?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsMoney(this object thisValue)
        {
            if (thisValue == null) return false;
            return double.TryParse(thisValue.ToString(), out _);
        }

        /// <summary>
        /// 是时间?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsDate(this object thisValue)
        {
            if (thisValue == null) return false;
            return DateTime.TryParse(thisValue.ToString(), out _);
        }

        /// <summary>
        /// 是邮箱?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsEamil(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        /// <summary>
        /// 是手机?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsMobile(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^\d{11}$");
        }

        /// <summary>
        /// 是座机?
        /// </summary>
        /// <param name="thisValue">值</param>
        public static bool IsTelephone(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{8}$");

        }

        /// <summary>
        /// 是身份证?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsIDcard(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
        }

        /// <summary>
        /// 是传真?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsFax(this object thisValue)
        {
            if (thisValue == null) return false;
            return Regex.IsMatch(thisValue.ToString(), @"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$");
        }

        /// <summary>
        ///是适合正则匹配?
        /// </summary>
        /// <param name="thisValue">值</param>
        /// <param name="pattern">正则</param> 
       /// <returns>真假</returns>
        public static bool IsMatch(this object thisValue, string pattern)
        {
            if (thisValue == null) return false;
            Regex reg = new Regex(pattern);
            return reg.IsMatch(thisValue.ToString());
        }
        /// <summary>
        /// 是true?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsTrue(this object thisValue)
        {
            return Convert.ToBoolean(thisValue);
        }
        /// <summary>
        /// 是false?
        /// </summary>
        /// <param name="thisValue">值</param>
       /// <returns>真假</returns>
        public static bool IsFalse(this object thisValue)
        {
            return !Convert.ToBoolean(thisValue);
        }
    }
}
