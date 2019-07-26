using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sansunt.Infra.Tools.Security.Encryptors
{
    /// <summary>
    /// des加解密
    /// create by xingbo 19/06/14
    /// </summary>
    public class DesEncryptor : IEncryptor
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        private readonly string _key;

        public DesEncryptor(string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "sanshang" : key.Trim();
            if (key.Length > 8) key = key.Substring(0, 8);
            if (key.Length < 8) key = key.PadLeft(8, 'x');
            _key = key;
        }
        public string Encrypt(string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_key);
            var keyIv = keyBytes;
            var inputByteArray = Encoding.UTF8.GetBytes(data);
            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIv), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public string Decrypt(string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_key);
            var keyIv = keyBytes;
            var inputByteArray = Convert.FromBase64String(data);
            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIv), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
