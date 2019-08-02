using Sansunt.MicroService.Demo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Application.Services
{
    /// <summary>
    /// 内置上传配置类
    /// 此类实现了分文件夹上传
    /// 
    /// </summary>
    public abstract class DefaultUploadSettingBase : IUploadSetting
    {
        /// <summary>获取一个文件夹内可以存放的子文件夹的个数</summary>
        /// <returns>子文件夹个数</returns>
        public abstract int GetMaxFolderCnt();
        /// <summary>获取一个文件夹中最多存放的文件个数</summary>
        /// <returns>文件个数</returns>
        public abstract int GetMaxFileCnt();


        /// <summary>
        /// 	<para>生成2级目录保存文件</para>
        /// </summary>
        /// <remarks>如:
        ///     <para>/root/0/0/file.jpg</para>
        ///     <para>/root/0/1/file.jpg</para>
        ///     <para>/root/1/0/file.jpg</para>
        ///     <para>/root/1/1/file.jpg</para>
        ///     <para>通过设置的文件夹下子文件夹和文件最大数量来计算</para>
        /// </remarks>
        public virtual string GeneratPathFromRoot(string rootDir, FileUploadModeEnum fileUploadMode = FileUploadModeEnum.LocalFile)
        {
            if (fileUploadMode != FileUploadModeEnum.LocalFile)
                throw new Exception("暂不支持ftp及其他模式");
            var rootpath = rootDir;
            #region 创建保存根目录
            if (!Directory.Exists(rootpath))
                Directory.CreateDirectory(rootpath);
            #endregion
            var root = new DirectoryInfo(rootpath);
            //根目录内不存在文件则创建2级目录并直接返回目录结构
            if (root.GetDirectories().Length == 0)
            {
                var path = Path.Combine(root.FullName, "0", "0");
                Directory.CreateDirectory(path);
                root = null;
                return path;
            }
            //寻找符合条件的2级文件夹，找到则直接返回路径
            var dir = root.GetDirectories().SelectMany(x => x.GetDirectories()).FirstOrDefault(x => x.GetFiles().Length < GetMaxFileCnt());
            if (dir != null)
            {
                var result = dir.FullName;
                root = null;
                dir = null;
                return result;
            }
            //寻找符合条件的1级文件夹找到则新建2级文件夹
            var lev1 = root.GetDirectories().FirstOrDefault(x => x.GetDirectories().Length < GetMaxFolderCnt());
            if (lev1 != null)
            {
                var last = lev1.GetDirectories().OrderBy(x => x.Name).Last().Name;
                var path = Path.Combine(lev1.FullName, (Convert.ToInt32(last) + 1).ToString());
                Directory.CreateDirectory(path);
                root = null;
                dir = null;
                lev1 = null;
                return path;
            }
            //创建1级文件夹并同时创建2级文件夹
            var lev1last = root.GetDirectories().OrderBy(x => x.Name).Last().Name;
            var lev1path = Path.Combine(root.FullName, (Convert.ToInt32(lev1last) + 1).ToString(), "0");
            Directory.CreateDirectory(lev1path);
            root = null;
            dir = null;
            lev1 = null;
            lev1last = null;
            return lev1path;
        }

        /// <summary>
        ///  图片显示在网页上的http基本地址
        /// <remarks>如：http://img.website.com/</remarks>
        /// </summary>
        /// <param name="webUrl"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public virtual string GetWebShowPath(string webUrl, string filepath)
        {
            var webPath = filepath.Replace("\\", "/").Replace("//", "/").TrimStart('\\').TrimStart('/');
            return $"{webUrl.TrimEnd('/').TrimEnd('\\')}/{webPath}";
        }

        public void Dispose()
        {
            
        }
    }
}