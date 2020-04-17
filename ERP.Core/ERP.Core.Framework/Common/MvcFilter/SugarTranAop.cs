using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using ERP.Framework.Data;

namespace ERP.Framework.Common.MvcFilter
{
    public class SugarTranAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //using (var db = SugarHandler.Instance("localhost", "root", "", "erp.core"))
            //{
            //    invocation.Proceed();
            //}
        }
    }
}
