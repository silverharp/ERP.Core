using System.Collections.Generic;

namespace ERP.Framework.Common.Models
{
    /// <summary>
    /// 导出实体
    /// </summary>
    public class ExportParams
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<QueryCondition> Conditions { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        public List<ExportColumnModel> Columns { get; set; }
        /// <summary>
        /// 其它参数
        /// </summary>
        public dynamic OtherParams { get; set; }

    }

    
}
