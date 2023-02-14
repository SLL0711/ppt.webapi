using System;
using System.Collections.Generic;
using System.Text;
using wt.basic.db.DBModels;

namespace wt.basic.service.Model
{
    public class PptDetailsModel
    {
        /// <summary>
        /// PPT的ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// PPT名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string MinPicture { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 轮播图
        /// </summary>
        public List<string> Turn_picture { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// ppt是否被收藏
        /// </summary>
        public Boolean FavrState { get; set; }
    }
}
