using System;
using System.Collections.Generic;
using System.Text;

namespace wt.basic.service.Model
{
    public class PptShowModel
    {
        /// <summary>
        /// PPT的ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string MinPicture { get; set; }
        /// <summary>
        /// PPT名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Tags { get; set; }
        /// <summary>
        /// 创建人头像
        /// </summary>
        public string Headshot { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// ppt是否被收藏
        /// </summary>
        public Boolean FavrState { get; set; }
        /// <summary>
        /// 收藏量
        /// </summary>
        public int FavrNum { get; set; }
        /// <summary>
        /// 下载量
        /// </summary>
        public int DownNum { get; set; }
    }
}
