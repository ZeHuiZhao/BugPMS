using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
///Security 的摘要说明
/// </summary>
public class Security
{
    public Security()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    #region 使用散列方式加密 MD5加密

    /// <summary>
    /// 使用MD5对字符串进行加密 
    /// </summary>
    /// <param name="str">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string MD5Encrypts(string str)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        return BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str))).Replace("-", "");
        /*上边两行是明源提供的加密算法*/

        #region 和明源对接数据之前的加密算法，对接之后使用明源提供的加密算法
        //string result = string.Empty;
        //string addition = "as#$%drtdfg&^";
        ////先将要加密的字符串转换成byte数组
        //byte[] inputData = System.Text.Encoding.Default.GetBytes(str + addition);
        ////在通过MD5类加密，并得到加密后的byte[]类型
        //byte[] data = System.Security.Cryptography.MD5.Create().ComputeHash(inputData);

        //StringBuilder strBuild = new StringBuilder();
        //for (int i = 0; i < data.Length; i++)
        //{
        //    //将每个byte数据转换成16进制。"X":表示大写16进制；"X2"：表示大写16进制保留2位；"x"：表示小写16进制
        //    strBuild.Append(data[i].ToString("X2"));
        //}
        //result = strBuild.ToString();
        //return result; 
        #endregion
        
    }

    #endregion
    #region 加密Key
    //$%*&#$%@
    private static string _QueryStringKey = "$%*&#$%@"; //URL传输参数加密Key这个key可以自己设置支持8位这个东西很重要的
    #endregion

    #region 加密算法
    /// <summary>
    /// 加密算法
    /// </summary>
    /// <param name="QueryString"></param>
    /// <returns></returns>
    public static string EncryptQueryString(string QueryString)
    {
        return Encrypt(QueryString, _QueryStringKey);
    }
    #endregion

    #region 解密算法
    /// <summary>
    /// 解密算法
    /// </summary>
    /// <param name="QueryString"></param>
    /// <returns></returns>
    public static string DecryptQueryString(string QueryString)
    {
        return Decrypt(QueryString, _QueryStringKey);
    }
    #endregion


    public static string Encrypt(string pToEncrypt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //把字符串放到byte数组中 

        byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);


        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey); //建立加密对象的密钥和偏移量
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey); //原文使用ASCIIEncoding.ASCII方法的GetBytes方法 
        MemoryStream ms = new MemoryStream(); //使得输入密码必须输入英文文本
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }

    public static string Decrypt(string pToDecrypt, string sKey)
    {
        if (pToDecrypt.Length < 10)
        {
            return "";
        }
        else
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey); //建立加密对象的密钥和偏移量，此值重要，不能修改 
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder(); //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }

    public static bool ValidateString(string EnString, string FoString, int Mode)
    {
        switch (Mode)
        {
            default:
            case 1:
                if (Decrypt(EnString, _QueryStringKey) == FoString.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }
    }
}