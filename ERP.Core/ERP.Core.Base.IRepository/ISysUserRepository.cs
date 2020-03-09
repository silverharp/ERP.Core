using System.Collections.Generic;
using ERP.Core.Base.Model;

namespace ERP.Core.Base.IRepository
{
    public interface ISysUserRepository
    {
        public string hello();

        public int Add1(List<SysUser> entity);
    }
}
