using ERP.Framework.Common.Enums;

namespace ERP.Framework.Common.Models
{
    /// <summary>
    /// 排序实体
    /// </summary>
    public class OrderByClause
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public OrderSequence Order { get; set; }
    }
}
