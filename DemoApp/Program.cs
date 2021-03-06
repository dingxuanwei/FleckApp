﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 10; i++)
            {
                var ctx = new
                {
                    ID = Guid.NewGuid().ToString(),
                    Auditor = "审核人",
                    AntiAuditReason = "反审核理由",
                    Notice = "通知人",
                    Applicant = "申请人",
                    MenuCode = "菜单编号" + i,
                    SubmitTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    OrderNo = "订单号"
                };

                string content = Newtonsoft.Json.JsonConvert.SerializeObject(ctx);

                var model = new HtmlMsg()
                {
                    Title = "推送测试",
                    Content = content,
                    RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ExpriedTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"),
                    RegName = "admin"
                };
                string s = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                WebClient wc = new WebClient();
                wc.Headers.Add("Content-Type", "application/json");
                var data = wc.UploadData("http://localhost:9918/MPService/PushHtmlMessage", Encoding.UTF8.GetBytes(s));
                var result = Encoding.UTF8.GetString(data);
                Console.WriteLine(i);
            }
            
            Console.ReadLine();
        }
    }
    /// <summary>
    /// Html推送，用于WebSocket
    /// </summary>
    class HtmlMsg
    {
        /// <summary>
        /// 标题，暂不列入推送，用于后台
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 具体推送内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public string ExpriedTime { get; set; }
        /// <summary>
        /// WebSocket注册名称，当WebSocket上线后，立即发送自己的名称即可在后续接收到推送消息
        /// </summary>
        public string RegName { get; set; }
    }

    /// <summary>
    /// 短信推送，暂未实现
    /// </summary>
    class SmsMsg
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string RequestTime { get; set; }
        public string ExpriedTime { get; set; }
        public string Phone { get; set; }
    }

    /// <summary>
    /// 邮件推送，暂未实现
    /// </summary>
    class EmailMsg
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string RequestTime { get; set; }
        public string ExpriedTime { get; set; }
        public string Address { get; set; }
    }
}
