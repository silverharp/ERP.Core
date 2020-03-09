using System.Collections.Generic;
using System.Reflection;

namespace ERP.Core.Framework.Common.Models
{
    /// <summary>
    /// 导出列数据集合
    /// </summary>
    public class ExportColumnModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 转换键
        /// </summary>
        public string ConvertKey { get; set; }
        /// <summary>
        /// 数据转换集合
        /// </summary>
        public List<KeyValueModel> Datas { get; set; }
    }

    /// <summary>
    /// 导出列对应数据及位置
    /// </summary>
    public class ExportColumnPropertyModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public int Point { get; set; }
    }
}
