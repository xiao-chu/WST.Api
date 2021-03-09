using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WST.DAL
{
    public class Md5Helper
    {

        public static string Get_MD5(string strSource, string sEncode)
        {
            //new
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取密文字节数组
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.GetEncoding(sEncode).GetBytes(strSource));

            //转换成字符串，并取9到25位
            //string strResult = BitConverter.ToString(bytResult, 4, 8); 
            //转换成字符串，32位

            string strResult = BitConverter.ToString(bytResult);

            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult.ToLower();
        }
    }
}
