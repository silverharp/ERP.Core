using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SqlSugar;

namespace ERP.Core.Framework.Common.Models
{
    [Serializable]
    public abstract class BaseEntity
    {
		public BaseEntity()
		{
			this.Id = Guid.NewGuid().ToString();
			this.CreateTime = DateTime.Now;
		}

		[Description("主键Id")]
		[SugarColumn(IsNullable = false, IsPrimaryKey = true)]
		public virtual string Id { get; protected set; }

		[SugarColumn(IsNullable = false)]
		public virtual int Version { get; private set; }

		[SugarColumn(IsNullable = false)]
		public virtual DateTime CreateTime { get; private set; }

		public virtual void SetId(string id)
		{
			this.Id = id;
		}

		public virtual void SetVersion(int version)
		{
			this.Version = version;
		}

		public virtual void SetCreateTime(DateTime createTime)
		{
			this.CreateTime = createTime;
		}
	}
}
