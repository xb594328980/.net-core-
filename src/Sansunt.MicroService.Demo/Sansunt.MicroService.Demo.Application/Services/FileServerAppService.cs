using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using FluentFTP;
using Microsoft.Extensions.Configuration;
using Sansunt.MicroService.Demo.Application.Interfaces;

using Sansunt.Infra.Tools;
using Sansunt.Infra.Tools.Security.Encryptors;
using Sansunt.MicroService.Demo.Infra.Config;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Application.Services
{
    /// <summary>
    /// 文件操作管理
    /// <remarks>
    /// create by xingbo 19/05/17
    /// </remarks>
    /// </summary>
    public class FileServerAppService : DefaultUploadSettingBase, IFileServerAppService
    {
        private readonly ConfigManage config = new ConfigManage();
        private readonly Md5Encryptor _md5Encryptor;
        public FileServerAppService(IConfiguration config)
        {
            _md5Encryptor = new Md5Encryptor();
        }
        /// <summary>
        /// 生成随机文件名
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <remarks>
        /// 当前日期年月日时分秒毫秒4位+3随机数
        /// </remarks>
        /// <returns></returns>
        public string GenerateRandomFileName(string ext)
        {
            var basename = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var rnd = new Random();
            for (var i = 0; i < 3; i++)
            {
                basename += rnd.Next(9);
            }
            return basename + ext;
        }
        /// <summary>
        /// 按照相关参数生成文件名
        /// </summary>
        /// <param name="extName">文件扩展名，不能包含.</param>
        /// <param name="list">参与生成文件名的参数数组</param>
        /// <returns></returns>
        public string GenerateFileName(string extName, params string[] list)
        {
            var Params = list.Join("", "");//组合参数
            return $"{_md5Encryptor.Encrypt(Params)}.{extName.Replace(".", "")}";
        }

        /// <summary>
        /// 判断文件时候存在，存在则返回完成文件路径
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileName">文件名称，可能包含父路径</param>
        /// <returns></returns>
        public string Exists(FileTypeEnum fileType, string fileName)
        {
            var uploadConfig = config.GetFileConfig(fileType);
            if (uploadConfig.FileUploadMode == FileUploadModeEnum.LocalFile)
            {
                var path = Path.Combine(config.LocalConfig.Path, fileName);
                return File.Exists(path) ? path : null;
            }
            else
            {
                string ftpSavePath = $"{uploadConfig.FtpConfig.FtpHeadDire.Trim('/')}/{fileName.Trim('/').Trim('\\')}";//组合ftp存储路径url
                StringBuilder showPath = new StringBuilder(uploadConfig.FtpConfig.WebUrl.TrimEnd('/'));//组合展示url
                using (FtpClient client = new FtpClient(uploadConfig.FtpConfig.FtpAddress))
                {
                    client.Credentials = new NetworkCredential(uploadConfig.FtpConfig.FtpUser, uploadConfig.FtpConfig.FtpPwd);
                    client.Connect();
                    if (client.FileExists(ftpSavePath))
                        return $"{showPath.ToString().Trim('/')}/{fileName.Trim('/').Trim('\\')}";
                    return null;
                }
            }

        }

        public string Upload(string url, FileTypeEnum fileType, out string webUrl)
        {
            HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
            var filename = myrp.GetResponseHeader("Content-disposition").Replace("attachment; filename=", "").Replace("\"", "");
            filename = string.IsNullOrWhiteSpace(filename) ? GenerateRandomFileName(".png") : filename;
            long totalBytes = myrp.ContentLength;
            Stream st = myrp.GetResponseStream();
            return Upload(st, fileType, filename, out webUrl);
        }
        public string Upload(Stream file, FileTypeEnum fileType, string filename, out string webUrl)
        {
            byte[] result = StreamToBytes(file);
            return Upload(result, fileType, filename, out webUrl);
        }

        public string Upload(byte[] file, FileTypeEnum fileType, string fileName, out string webUrl)
        {
            var uploadConfig = config.GetFileConfig(fileType);
            string savePath = Path.Combine(config.LocalConfig.Path);
            string filePath = $"{uploadConfig.Dic.Trim('/').Trim('\\')}/{fileName.Trim('/').Trim('\\')}";
            if (uploadConfig.FileUploadMode == FileUploadModeEnum.LocalFile)
            {
                savePath = Path.Combine(savePath, uploadConfig.Dic);
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                savePath = Path.Combine(savePath, fileName);
                Stream so = new FileStream(savePath, FileMode.Create);
                so.Write(file, 0, file.Length);
                so.Close();
                webUrl = uploadConfig.WebUrl;
                return filePath.Trim('/').Replace("\\", "/");
            }
            else
            {
                string ftpSavePath = $"{uploadConfig.FtpConfig.FtpHeadDire.Trim('/')}/{uploadConfig.Dic.Trim('/').Trim('\\')}/{fileName.Trim('/').Trim('\\')}";//组合ftp存储路径url
                StringBuilder showPath = new StringBuilder(uploadConfig.FtpConfig.WebUrl.TrimEnd('/'));//组合展示url
                using (FtpClient client = new FtpClient(uploadConfig.FtpConfig.FtpAddress))
                {
                    client.Credentials = new NetworkCredential(uploadConfig.FtpConfig.FtpUser, uploadConfig.FtpConfig.FtpPwd);
                    client.Connect();
                    if (client.FileExists(ftpSavePath))
                        throw new AggregateException("文件已存在");
                    if (!client.Upload(file, ftpSavePath, FtpExists.Overwrite, true))
                        throw new AggregateException("上传失败，请重新尝试上传");
                    webUrl = uploadConfig.FtpConfig.WebUrl;
                    return filePath.Trim('/').Replace("\\", "/");
                }
            }
        }



        #region 辅助方法


        /// <summary>
        /// Stream转byte[]
        /// </summary>
        /// <remarks>
        /// create by xingbo 19/05/17
        /// </remarks>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
            //            List<byte> bytes = new List<byte>();
            //            int temp = stream.ReadByte();
            //            while (temp != -1)
            //            {
            //                bytes.Add((byte)temp);
            //                temp = stream.ReadByte();
            //            }
            //            return bytes.ToArray();
        }

        public override int GetMaxFolderCnt()
        {
            return config.LocalConfig.MaxFolderCnt;
        }

        public override int GetMaxFileCnt()
        {
            return config.LocalConfig.MaxFileCnt;
        }



        #endregion
    }

}
