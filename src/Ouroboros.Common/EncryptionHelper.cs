using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Common
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// 此代码示例通过创建哈希字符串适用于任何 MD5 哈希函数 （在任何平台） 上创建 32 个字符的十六进制格式哈希字符串
        /// 官网案例改编
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Get32MD5One(string source)
        {
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(source));
                System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                string hash = sBuilder.ToString();
                return hash.ToUpper();
            }
        }

        /// <summary>
        /// 获取16位md5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Get16MD5One(string source)
        {
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(source));
                //转换成字符串，并取9到25位
                string sBuilder = BitConverter.ToString(data, 4, 8);
                //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
                sBuilder = sBuilder.Replace("-", "");
                return sBuilder.ToString().ToUpper();
            }
        }

        //// <summary>
        /// </summary>
        /// <param name="strSource">需要加密的明文</param>
        /// <returns>返回32位加密结果，该结果取32位加密结果的第9位到25位</returns>
        public static string Get32MD5Two(string source)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //获取密文字节数组
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(source));
            //转换成字符串，32位
            string strResult = BitConverter.ToString(bytResult);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult.ToUpper();
        }

        //// <summary>
        /// </summary>
        /// <param name="strSource">需要加密的明文</param>
        /// <returns>返回16位加密结果，该结果取32位加密结果的第9位到25位</returns>
        public static string Get16MD5Two(string source)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //获取密文字节数组
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(source));
            //转换成字符串，并取9到25位
            string strResult = BitConverter.ToString(bytResult, 4, 8);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult.ToUpper();
        }

        //SHA为不可逆加密方式
        public static string SHA1Encrypt(string normalTxt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(normalTxt);
            var SHA = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var encryptbytes = SHA.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public static string SHA256Encrypt(string normalTxt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(normalTxt);
            var SHA256 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            var encryptbytes = SHA256.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public static string SHA384Encrypt(string normalTxt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(normalTxt);
            var SHA384 = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            var encryptbytes = SHA384.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public string SHA512Encrypt(string normalTxt)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(normalTxt);
            var SHA512 = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            var encryptbytes = SHA512.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }

        /// <summary>
        /// 将base64格式，转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary> 
        ///  DES加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESEncryption(string Text, string sKey = null)
        {
            sKey = sKey ?? "Ouroboros";
            try
            {
                System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
                byte[] inputByteArray;
                inputByteArray = System.Text.Encoding.Default.GetBytes(Text);
                string md5SKey = Get32MD5One(sKey).Substring(0, 8);
                des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(md5SKey);
                des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(md5SKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.StringBuilder ret = new System.Text.StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch { return "error"; }
        }

        /// <summary> 
        /// DES解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESDecrypt(string Text, string sKey = null)
        {
            sKey = sKey ?? "Ouroboros";
            try
            {
                System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();
                int len;
                len = Text.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                string md5SKey = Get32MD5One(sKey).Substring(0, 8);
                des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(md5SKey);
                des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(md5SKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch { return "error"; }
        }

        /// <summary> 
        /// RSA加密数据 
        /// </summary> 
        /// <param name="express"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string RSAEncryption(string express, string KeyContainerName = null)
        {

            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "Ouroboros"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] plaindata = System.Text.Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }
        /// <summary> 
        /// RSA解密数据 
        /// </summary> 
        /// <param name="express"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string RSADecrypt(string ciphertext, string KeyContainerName = null)
        {
            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "Ouroboros";
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return System.Text.Encoding.Default.GetString(decryptdata);
            }
        }

    }
}
