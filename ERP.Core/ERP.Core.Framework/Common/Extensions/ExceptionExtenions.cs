using System;

namespace ERP.Framework.Common.Extensions
{
    /// <summary>
    /// 异常信息扩展
    /// </summary>
    public static class ExceptionExtenions
    {
        /// <summary>
        /// 解析异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static string Resolve(this Exception ex)
        {
            var msg = ex.Message;
            if (ex.Message.IndexOf("PK_", StringComparison.Ordinal) > -1)
            {
                msg ="主键重复";
            }
            else if (ex.Message.IndexOf("FK_", StringComparison.Ordinal) > -1)
            {
                msg = "外键数据冲突";
            } 
            return msg;
        }
    }
}
