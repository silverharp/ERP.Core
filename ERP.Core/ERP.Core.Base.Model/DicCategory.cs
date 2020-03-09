using ERP.Framework.Common.Models;
using SqlSugar;

namespace ERP.Base.Model
{
    [SugarTable("SYS_DIC_CATEGORY")]
    public class DicCategory : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int IndexField { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public virtual DicCategory Parent { get; set; }

        /// <summary>
        /// 是否叶
        /// </summary>
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 节点深度
        /// </summary>
        public virtual int NodeLevel { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 树父节点集合
        /// </summary>
        public virtual string TreeIds { get; set; }
    }
}
