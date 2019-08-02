using System;
using System.IO;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Application.Interfaces
{
    /// <summary>上传设置接口</summary>
    /// <remarks>
    /// 	<para>可为不同类型的上传提供不同的此接口的实现。</para>
    /// 	<para>通过此接口，可以设置：</para>
    /// 	<para>文件保存根路径</para>
    /// 	<para>此类型存储的文件夹</para>
    /// 	<para>一个文件夹下最多子文件夹个数，最多文件个数</para>
    /// 	<para>显示在网页上的基本地址</para>
    /// 	<para>保存在库里的数据格式</para>
    /// </remarks>
    public interface IUploadSetting : IDisposable
    {
        /// <summary>
        /// 一个目录下最多存放多少个子目录
        /// </summary>
        /// <returns></returns>
        int GetMaxFolderCnt();

        /// <summary>
        /// 一个目录下最多存放多少个文件
        /// </summary>
        /// <returns></returns>
        int GetMaxFileCnt();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootDir">保存基础目录，包含根目录及分类目录</param>
        /// <param name="fileUploadMode">存储目录下的保存目录及文件名</param>
        /// <returns></returns>
        string GeneratPathFromRoot(string rootDir, FileUploadModeEnum fileUploadMode = FileUploadModeEnum.LocalFile);


        /// <summary>
        /// 通过一个文件路径，算出他展示在网页上的地址
        /// create by xingbo 19/05/28
        /// </summary>
        /// <param name="webUrl">web展示地址</param>
        /// <param name="filepath">存储目录下的保存目录及文件名</param>
        /// <returns></returns>
        string GetWebShowPath(string webUrl, string filepath);


    }
}