using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Application.Interfaces
{
    /// <summary>
    /// 文件操作管理
    /// <remarks>
    /// create by xingbo 19/05/17
    /// </remarks>
    /// </summary>
    public interface IFileServerAppService : IUploadSetting, IDisposable
    {
        /// <summary>
        /// 按照相关参数生成文件名
        /// </summary>
        /// <param name="extName">文件扩展名，不能包含.</param>
        /// <param name="list">参与生成文件名的参数数组</param>
        /// <returns></returns>
        string GenerateFileName(string extName, params string[] list);

        /// <summary>
        /// 判断文件时候存在，存在则返回完成文件路径
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileName">文件名称，可能包含父路径</param>
        /// <returns></returns>
        string Exists(FileTypeEnum fileType, string fileName);
        /// <summary>
        /// 生成随机文件名
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <remarks>
        /// 当前日期年月日时分秒毫秒4位+3随机数
        /// </remarks>
        /// <returns></returns>
        string GenerateRandomFileName(string ext);

        /// <summary>
        /// 通过路径下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        string Upload(string url, FileTypeEnum fileType, out string webUrl);

        /// <summary>
        /// 上传文件到指定文件夹
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileType"></param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        string Upload(byte[] file, FileTypeEnum fileType, string fileName, out string webUrl);

        /// <summary>
        /// 上传到指定文件夹并指定名字
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileType">fileType</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        string Upload(Stream file, FileTypeEnum fileType, string filename, out string webUrl);

        /// <summary>
        /// Stream转byte[]
        /// </summary>
        /// <remarks>
        /// create by xingbo 19/05/17
        /// </remarks>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        byte[] StreamToBytes(Stream stream);
    }
}
