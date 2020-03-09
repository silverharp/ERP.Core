using System.Collections.Generic;
using ERP.Base.Model;

namespace ERP.Base.IRepository
{
    public interface ISysUserRepository
    {
        public string hello();

        public int Add1(List<SysUser> entity);
    }
}
