using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ERP.Framework.Common.Log;
using Newtonsoft.Json;

namespace ERP.Framework.Common.Helpers
{
    /// <summary>
    /// json的操作类
    /// </summary>
    public class JsonHelper
    {
        #region 对象和json互转 
        
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <param name="isformatting">是否缩进JSON格式 </param>
        /// <returns>json字符串</returns>
        public static string ObjectToJson(object o, bool isformatting = false)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, //忽略NULL值
                //DateFormatString = "yyyy-MM-dd HH:mm:ss"  //格式日期
            };
            string json = JsonConvert.SerializeObject(o, isformatting ? Formatting.Indented : Formatting.None, settings);
            //json = json.Replace("\"", "\'");
            json = json.Replace("\r", "").Replace("\n", "");
            return json;
        }
        /// <summary>
        /// 将对象集合序列化为JSON格式
        /// </summary>
        /// <param name="o">对象集合</param>
        /// <param name="isformatting">是否缩进JSON格式 </param>
        /// <returns>json字符串</returns>
        public static string ObjectToJson(List<object> o, bool isformatting = false)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, //忽略NULL值
                //DateFormatString = "yyyy-MM-dd HH:mm:ss"  //格式日期
            };
            string json = JsonConvert.SerializeObject(o, isformatting ? Formatting.Indented : Formatting.None, settings);
            //json = json.Replace("\"", "\'");
            return json;
        }


        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// Student sdudent4 = JsonHelper.DeserializeJsonToObject<Student/>("{\"ID\":\"112\",\"Name\":\"石子儿\"}");
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T JsonToObject<T>(object json) where T : class
        {
            var strJson = json is string ? json.ToString() : ObjectToJson(json);
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(strJson);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                T t = o as T;
                return t;
            }
            catch
            {
                LogHelper.WriteLog($"JSON参数:{strJson}","ErrorJson");
                return null;
            }
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// List<Student/> sdudentList4 = JsonHelper.DeserializeJsonToList<Student/>("[{\"ID\":\"112\",\"Name\":\"石子儿\"}]");
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> JsonToList<T>(object json) where T : class
        {
            string strJson;
            if (json is string)
            {
                strJson = json.ToString();
            }
            else
            {
                strJson = ObjectToJson(json);
            }
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(strJson);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }
        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// var tempEntity = new { ID = 0, Name = string.Empty }; 
        ///tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);
        ///var tempStudent = new Student();
        ///tempStudent = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempStudent);
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T JsonToAnonymous<T>(dynamic json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }
        #endregion


        #region DataTable转实体 
        /// <summary>
        /// DataTable转实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="row">DataRow</param>
        /// <returns>实体</returns> 
        public static T DataRowToEntity<T>(DataRow row) where T : new()
        {
            T entity = new T();
            foreach (var item in entity.GetType().GetProperties())
            {
                if (row.Table.Columns.Contains(item.Name))
                {   //取值   
                    object value = row[item.Name];
                    //如果非空，则赋给对象的属性   
                    if (value != DBNull.Value)
                    {
                        var valueStr = value.ToString().Trim();
                        if (string.IsNullOrWhiteSpace(valueStr))
                        {
                            item.SetValue(entity, GetParseValue(item.PropertyType, valueStr), null);
                        }
                    }
                }
            }
            return entity;
        }
        /// <summary>
        /// DataTable转实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>实体</returns>
        public static List<T> DataTableToEntities<T>(DataTable table) where T : new()
        {
            List<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (table.Columns.Contains(item.Name))
                    {
                        //取值   
                        object value = row[item.Name];
                        //如果非空，则赋给对象的属性   
                        if (value != DBNull.Value)
                            item.SetValue(entity, value, null);
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        /// <summary>
        /// DataTable转实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>实体</returns>
        public static List<T> DataTableToEntitiesMatch<T>(DataTable table) where T : new()
        {
            List<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (table.Columns.Contains(item.Name))
                    {
                        //取值   
                        object value = row[item.Name];
                        //如果非空，则赋给对象的属性   
                        if (value != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(value.ToString()))
                            {
                                Type type = item.PropertyType;
                                //判断type类型是否为泛型，因为nullable是泛型类,
                                if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                                {
                                    type = Nullable.GetUnderlyingType(type);
                                }
                                item.SetValue(entity, Convert.ChangeType(value, type), null);
                                //item.SetValue(entity, value, null);
                            }
                        }
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object GetParseValue(Type type, string value)
        {
            switch (type.ToString().ToUpper())
            {
                case ExcelHelper.DataTypeString.Decimal:
                    return Convert.ToDecimal(value);
                case ExcelHelper.DataTypeString.NullableDecimal:
                    return Convert.ToDecimal(value);
                case ExcelHelper.DataTypeString.DataTime:
                case ExcelHelper.DataTypeString.NullableDateTime:
                    return Convert.ToDateTime(value);
                case ExcelHelper.DataTypeString.Int:
                case ExcelHelper.DataTypeString.NullableInt:
                    return Convert.ToInt32(value);
                case ExcelHelper.DataTypeString.String:
                    return value;
                case ExcelHelper.DataTypeString.Single:
                    return Convert.ToSingle(value);
                case ExcelHelper.DataTypeString.Boolean:
                case ExcelHelper.DataTypeString.NullableBoolean:
                    return Convert.ToBoolean(value);
                case ExcelHelper.DataTypeString.Double:
                    return Convert.ToDouble(value);
                case ExcelHelper.DataTypeString.Guid:
                    return new Guid(value);
                case ExcelHelper.DataTypeString.NullableGuid:
                    return new Guid(value);
                case ExcelHelper.DataTypeString.Byte:
                case ExcelHelper.DataTypeString.NullByte:
                    return Convert.ToByte(value);
                default:
                    return value;
            }
        }
         
        #endregion

    }
}

