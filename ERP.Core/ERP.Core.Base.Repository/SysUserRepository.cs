using ERP.Base.Model;
using ERP.Framework.Data;
using System.Collections.Generic;
using ERP.Base.IRepository;

namespace ERP.Base.Repository
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
