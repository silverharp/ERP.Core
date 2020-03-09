namespace ERP.Core.Framework.Common.Models
{
    /// <summary>
    /// 键值实体
    /// </summary>
    public class TreeModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 上级编码       
        /// </summary>
        public string ParentNo { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 能增加子节点的标志
        /// </summary>
        public bool CanAdd { get; set; }
    }
}

