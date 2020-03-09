using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;


namespace ERP.Core.Framework.Common.Helpers
{
    /// <summary>
    /// 微信企业号操作类
    /// </summary>
    public class WxHelper
    {
        #region [ 属性 ]  
        /// <summary>  
        /// 应用CorpID
        /// </summary>  
        public string CorpId { get; set; }
        /// <summary>  
        /// Secret 
        /// </summary>  
        public string Secret { get; set; }
        /// <summary>  
        /// AgentId  
        /// </summary>  
        public int AgentId { get; set; }
        #endregion
        #region 公共

        /// <summary>
        /// 获取企业号的accessToken
        /// </summary>
        /// <param name="corpid">企业号ID</param>
        /// <param name="corpsecret">管理组密钥</param>
        /// <returns></returns>
        private string GetAccessToken(string corpid, string corpsecret)
        {
            string getAccessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";
            string accessToken = "";

            string respText = "";

            //获取josn数据
            string url = string.Format(getAccessTokenUrl, corpid, corpsecret);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream resStream = response.GetResponseStream())
            {
                if (resStream != null)
                {
                    StreamReader reader = new StreamReader(resStream, Encoding.Default);
                    respText = reader.ReadToEnd();
                    resStream.Close();
                }

            }

            try
            { 
                Dictionary<string, object> respDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(respText);

                //通过键access_token获取值
                accessToken = respDic["access_token"].ToString();
            }
            catch (Exception)
            {
                // ignored
            }

            return accessToken;
        }


        #endregion

        #region 发送文本消息 
        /// <summary>
        /// 发送信息(用户)
        /// </summary> 
        /// <param name="userId">成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param> 
        /// <param name="content">消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）</param>
        /// <param name="msg">错误消息</param>
        /// <returns>true 成功 false 失败</returns>
        public bool SendMessageToUser(string userId, string content,out string msg)
        {
            var flag = SendMessage(userId, "", "", content, Encoding.UTF8,out msg); 
            return flag; 
        }
        /// <summary>
        /// 发送信息(部门)
        /// </summary>  
        /// <param name="partyId">部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="content">消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）</param>
        /// <param name="msg">错误消息</param>
        /// <returns>true 成功 false 失败</returns>
        public  bool SendMessageToParty(string partyId, string content, out string msg)
        {
            var flag = SendMessage("", partyId, "", content, Encoding.UTF8, out msg); 
            return flag; 
        }
        /// <summary>
        /// 发送信息(标签)
        /// </summary>  
        /// <param name="tagId">标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param> 
        /// <param name="content">消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）</param>
        /// <param name="msg">错误消息</param>
        /// <returns>true 成功 false 失败</returns>
        public  bool SendMessageToTag(string tagId, string content, out string msg)
        {
            var flag = SendMessage("", "", tagId, content, Encoding.UTF8, out msg); 
            return flag; 
        }
        /// <summary>
        /// 发送信息
        /// </summary> 
        /// <param name="userId">成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="partyId">部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="tagId">标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param> 
        /// <param name="content">消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）</param>
        /// <param name="dataEncode">编码方式</param>
        /// <param name="msg">错误消息</param>
        /// <returns>true 成功 false 失败</returns>
        public  bool SendMessage(string userId, string partyId, string tagId, string content, Encoding dataEncode, out string msg)
        {
            msg = "";
            #region 读取配置  
            var corpId = CorpId;
            var corpsecret = Secret;
            var agentid = AgentId;
            if (string.IsNullOrWhiteSpace(corpId))
            {
                msg = "未配置应用CorpID";
                return false;
            }
            if (string.IsNullOrWhiteSpace(corpsecret))
            {
                msg = "未配置应用Secret";
                return false;
            }
            if (agentid==0)
            {
                msg = "未配置应用ID";
                return false;
            }
            #endregion

            string accessToken = GetAccessToken(corpId, corpsecret);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                msg = "access_token获取失败"; 
                return false;
            }
            string postUrl = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={accessToken}";
            string paramData = GetJsonText(userId, partyId, tagId, agentid, content); 
            var flag= PostWebRequest(postUrl, paramData, dataEncode, out msg);
            return flag;
        }

        /// <summary>
        /// 获取text消息Json
        /// </summary>
        /// <param name="userId">成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="partyId">部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="tagId">标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看</param>
        /// <param name="content">消息内容，最长不超过2048个字节，注意：主页型应用推送的文本消息在微信端最多只显示20个字（包含中英文）</param>
        /// <returns>值</returns>
        private static string GetJsonText(string userId, string partyId, string tagId, int agentId, string content)
        {
            string responeJsonStr = "{";
            responeJsonStr += "\"touser\": \"" + userId + "\",";
            responeJsonStr += "\"toparty\": \"" + partyId + "\",";
            responeJsonStr += "\"totag\": \"" + tagId + "\",";
            responeJsonStr += "\"msgtype\": \"text\",";
            responeJsonStr += "\"agentid\": \"" + agentId + "\",";
            responeJsonStr += "\"text\": {";
            responeJsonStr += "  \"content\": \"" + content + "\"";
            responeJsonStr += "},";
            responeJsonStr += "\"safe\":\"0\"";
            responeJsonStr += "}";
            return responeJsonStr;
        }
        #endregion


        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <param name="msg">消息</param>
        /// <returns>真假</returns>
        private static bool PostWebRequest(string postUrl, string paramData, Encoding dataEncode, out string msg)
        {
            try
            {
                bool flag;
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                var stream = response.GetResponseStream();
                if (stream == null)
                {
                    msg = "发送失败";
                    return false;
                }
                StreamReader sr = new StreamReader(stream, Encoding.Default);
                var ret = sr.ReadToEnd(); 
                Dictionary<string, object> respDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (respDic["errmsg"].ToString() == "ok")
                {
                    msg = "发送成功:" + ret;
                    flag = true;
                }
                else
                {
                    msg = "发送失败:" + ret;
                    flag = false;
                }
                sr.Close();
                response.Close();
                newStream.Close();
                return flag;
            }
            catch (Exception ex)
            {
                msg = "发送失败:" + ex.Message;
                return false;
            }
        }
    }
}
