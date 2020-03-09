using System;
using System.Collections.Generic;
using System.Text;
using ERP.Core.Framework.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.Core.Framework.Common.MvcFilter
{
    /// <summary>
    /// sqlsugar的aop特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SugarFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}
