using System;
using System.Collections.Generic;
using System.Text;
using ERP.Framework.Common.Models;
using SqlSugar;

namespace ERP.Base.Model
{
    [SugarTable("SYS_DIC_ITEM")]
    public class DicItem : BaseEntity
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
        /// 所属字典类别
        /// </summary>
        public virtual DicCategory DicCategory { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }
    }
}
