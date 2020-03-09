using ERP.Core.Base.Model;
using ERP.Core.Framework.Data;
using System.Collections.Generic;
using ERP.Core.Base.IRepository;

namespace ERP.Core.Base.Repository
{
    public class SysUserRepository : SugarRepository, ISysUserRepository
    {
        public string hello()
        {
            return "hello autofac";
        }

        public int Add1(List<SysUser> entity)
        {
            return base.Add(entity);
        }
    }
}
