using System;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Summer
{
    public class PlayerPrefsExtension
    {

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static float GetFloat(string key)
        {
            float result = PlayerPrefs.GetFloat(key);
            return result;
        }

        //public static float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue)

        public static int GetInt(string key)
        {
            int result = PlayerPrefs.GetInt(key);
            return result;
        }
        //public static int GetInt(string key, [DefaultValue("0")] int defaultValue);

        public static string GetString(string key)
        {
            string result = PlayerPrefs.GetString(key);
            return result;
        }

        //public static string GetString(string key, [DefaultValue("\"\"")] string defaultValue)

        public static bool HasKey(string key)
        {
            return false;
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }


    public class PlayerPrefsHelper
    {
        private static byte[] keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region  方法一 C#中对字符串加密解密（对称算法）  
        /// <summary>  
        /// DES加密字符串  
        /// </summary>  
        /// <param name="encrypt_string">待加密的字符串</param>  
        /// <param name="encrypt_key">加密密钥,要求为8位</param>  
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>  
        public static string EncryptDes(string encrypt_string, string encrypt_key)
        {
            try
            {
                byte[] rgb_key = Encoding.UTF8.GetBytes(encrypt_key.Substring(0, 8));
                byte[] rgb_iv = keys;
                byte[] input_byte_array = Encoding.UTF8.GetBytes(encrypt_string);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream m_stream = new MemoryStream();
                CryptoStream c_stream = new CryptoStream(m_stream, dCSP.CreateEncryptor(rgb_key, rgb_iv), CryptoStreamMode.Write);
                c_stream.Write(input_byte_array, 0, input_byte_array.Length);
                c_stream.FlushFinalBlock();
                c_stream.Close();
                return Convert.ToBase64String(m_stream.ToArray());
            }
            catch
            {
                Debug.LogError("-------------------------EncryptDes-------------------------");
                return encrypt_string;
            }
        }

        /// <summary>  
        /// DES解密字符串  
        /// </summary>  
        /// <param name="decrypt_string">待解密的字符串</param>  
        /// <param name="decrypt_key">解密密钥,要求为8位,和加密密钥相同</param>  
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>  
        public static string DecryptDes(string decrypt_string, string decrypt_key)
        {
            try
            {
                byte[] rgb_key = Encoding.UTF8.GetBytes(decrypt_key);
                byte[] rgb_iv = keys;
                byte[] input_byte_array = Convert.FromBase64String(decrypt_string);
                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                MemoryStream m_stream = new MemoryStream();
                CryptoStream c_stream = new CryptoStream(m_stream, dcsp.CreateDecryptor(rgb_key, rgb_iv), CryptoStreamMode.Write);
                c_stream.Write(input_byte_array, 0, input_byte_array.Length);
                c_stream.FlushFinalBlock();
                c_stream.Close();
                return Encoding.UTF8.GetString(m_stream.ToArray());
            }
            catch
            {
                Debug.LogError("-------------------------DecryptDes-------------------------");
                return decrypt_string;
            }
        }
        #endregion
    }
}

