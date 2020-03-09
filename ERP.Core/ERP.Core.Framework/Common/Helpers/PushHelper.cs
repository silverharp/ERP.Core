using System;
using ERP.Framework.Common.Config;
using ERP.Framework.Common.Log;

namespace ERP.Framework.Common.Helpers
{
    /// <summary>
    /// 消息发送类
    /// </summary>
    public class PushHelper
    {
        /// <summary>
        /// 推送消息(通过配置文件"MsgType"判断推送消息)
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="taskName">任务名称</param>
        /// <returns></returns>
        public static void SendMsg(string content,string taskName)
        {
            switch ((string)SysConfig.Params.MsgType)
            {
                case "1": //Email
                    try
                    {
                        var email = new EmailHelper
                        {
                            SmtpHost = (string)SysConfig.Params.SmtpHost,
                            SmtpPort = (int)SysConfig.Params.SmtpPort,
                            FromEmailAddress = (string)SysConfig.Params.FromEmailAddress,
                            FormEmailPassword = (string)SysConfig.Params.FormEmailPassword,
                            ToList = (string)SysConfig.Params.ToEmaiAddress,
                            Subject = $"任务({taskName})",
                            Body = content
                        };
                        email.Send();
                        LogHelper.WriteLog("邮件发送成功",taskName);
                    }
                    catch (Exception ex)
                    { 
                        LogHelper.WriteError(ex, taskName);
                    }
                    
                    break;
                case "2"://微信
                    var wx = new WxHelper
                    {
                        AgentId = (int)SysConfig.Params.WxAgentId,
                        Secret = (string)SysConfig.Params.WxSecret,
                        CorpId = (string)SysConfig.Params.WxCorpId,
                    };
                    string msg;
                    wx.SendMessageToUser((string)SysConfig.Params.WxToUser, content, out msg);
                    LogHelper.WriteLog(msg, taskName);
                    break;
            }
        }
    }
}
