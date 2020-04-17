using System;
using ERP.Framework.Common.Models;
using SqlSugar;

namespace ERP.Base.Model
{
    [SugarTable("SYS_USER")]
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 是否停用
        /// </summary>
        public virtual bool IsStop { get; set; }
    }
}
