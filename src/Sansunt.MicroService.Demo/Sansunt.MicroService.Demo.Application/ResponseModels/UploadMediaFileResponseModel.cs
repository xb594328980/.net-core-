using System;
using System.Collections.Generic;
using System.Text;

namespace Sansunt.MicroService.Demo.Application.ResponseModels
{
    /// <summary>
    /// 上传媒体文件相应模型
    /// create by xingbo 19/05/28
    /// </summary>
    public class UploadMediaFileResponseModel
    {
        /// <summary>
        /// 服务器展示地址
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 子路径
        /// </summary>
        public string SubPath { get; set; }
    }
}
