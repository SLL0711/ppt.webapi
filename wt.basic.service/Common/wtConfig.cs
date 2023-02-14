using System;
using System.Collections.Generic;
using System.Text;

namespace wt.basic.service.Common
{
    public class wtConfig
    {
        public Token token { get; set; }

        /// <summary>
        /// 限制ppt附件大小
        /// </summary>
        public int pptMaxLeng { get; set; }
        /// <summary>
        /// 限制headshot头像的大小
        /// </summary>
        public int headshotMaxLeng { get; set; }
        public FileServer fileserver { get; set; }
    }

    public class Token
    {
        /// <summary>
        /// token颁发机构
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 观众
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间（小时）
        /// </summary>
        public double TimeExpiresHour { get; set; }
        /// <summary>
        /// 允许过期偏差分钟数
        /// </summary>
        public int ClockSkew { get; set; }
        /// <summary>
        /// 验证密钥
        /// </summary>
        public string SecretKey { get; set; }
    }

    public class FileServer
    {
        /// <summary>
        /// 静态文件服务器物理路径
        /// </summary>
        public string address { get; set; }
    }
}
