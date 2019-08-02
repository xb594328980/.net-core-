using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sansunt.Infra.Tools;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Extensions.Extensions
{
    /// <summary>
    /// 扩展文件
    /// create by xingbo 19/06/28
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="files">文件列表</param>
        /// <returns></returns>
        public static FileTypeEnum GetFileType(this List<IFormFile> files, out string fileExtension)
        {
            if (files == null || files.Count != 1)
                throw new AggregateException("单次请求有且只能上传1张图片");
            List<string> picTypeLimit = new List<string> { ".JPG", ".JPEG", ".PNG", ".JIF" };
            List<string> videoTypeLimit = new List<string> { ".MOV", ".M4V", ".AVI", ".3GP", ".BMP", ".MP4" };
            FileTypeEnum filetype;
            var currFile = files.First();
            //必须存在图片文件名称
            if (string.IsNullOrWhiteSpace(currFile.FileName))
                throw new AggregateException($"文件名称不正确");
            //当图片上被选中时，拿到文件的扩展名
            string currentFileExtension = Path.GetExtension(currFile.FileName).ToUpper();
            //此处对上传的文件类型进行限定操作
            if (picTypeLimit.Any(x => x.Contains(currentFileExtension)))
                filetype = FileTypeEnum.Pic;
            else if (videoTypeLimit.Any(x => x.Contains(currentFileExtension)))
                filetype = FileTypeEnum.Video;
            else
                throw new AggregateException($"图片支持:{picTypeLimit.Join()},视频支持：{videoTypeLimit.Join()}");
            fileExtension = currentFileExtension;
            return filetype;
        }
    }
}
