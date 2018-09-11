
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Summer
{
    //=============================================================================
    /// Author : mashaomin
    /// CreateTime : 2017-8-3
    /// FileName : FileHelper.cs
    /// 关于一些String的处理
    /// TODO 转移到Editor中去，保留读取和写入功能
    //=============================================================================
    public class FileHelper
    {
        /// <summary>
        /// 递归获取所有的目录
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="lstDirect"></param>
        public static void GetAllDirectorys(string strPath, ref List<string> lstDirect)
        {
            if (Directory.Exists(strPath) == false)
            {
                Console.WriteLine("请检查，路径不存在：{0}", strPath);
                return;
            }
            DirectoryInfo diFliles = new DirectoryInfo(strPath);
            DirectoryInfo[] directories = diFliles.GetDirectories();
            var max = directories.Length;
            for (int dirIdx = 0; dirIdx < max; dirIdx++)
            {
                try
                {
                    var dir = directories[dirIdx];
                    //dir.FullName是某个子目录的绝对地址，把它记录起来
                    lstDirect.Add(dir.FullName);
                    GetAllDirectorys(dir.FullName, ref lstDirect);
                }
                catch
                {
                    LogManager.Error("[GetAllFiles] Error");
                }
            }
        }

        /// <summary>  
        /// 遍历当前目录及子目录，获取所有文件 
        /// </summary>  
        /// <param name="strPath">文件路径</param>  
        /// <returns>所有文件</returns>  
        public static List<FileInfo> GetAllFiles(string strPath)
        {
            List<FileInfo> lstFiles = new List<FileInfo>();
            List<string> lstDirect = new List<string>();
            lstDirect.Add(strPath);
            GetAllDirectorys(strPath, ref lstDirect);

            var max = lstDirect.Count;
            for (int idx = 0; idx < max; idx++)
            {
                try
                {
                    DirectoryInfo diFliles = new DirectoryInfo(lstDirect[idx]);
                    lstFiles.AddRange(diFliles.GetFiles());
                }
                catch
                {
                    LogManager.Error("[GetAllFiles] Error");
                }
            }
            return lstFiles;
        }

        /// <summary>
        /// 读取指定路径的文本内容
        /// </summary>
        /// <param name="strPath">路径</param>
        /// <returns>文本内容</returns>
        public static string ReadAllText(string strPath)
        {
            string txt = string.Empty;
#if UNITY_EDITOR
            txt = File.ReadAllText(strPath);
#endif
            return txt;
        }

        public static bool IsExit(string strPath)
        {
            FileInfo fib = new FileInfo(strPath);
            if (fib.Exists)
                return true;
            return false;
        }

        public static void WriteTxtByFile(string srtPath, string content)
        {
            FileInfo fib = new FileInfo(srtPath);
            if (fib.Exists)
            {
                fib.Delete();
            }

            FileStream fs = fib.Create();
            byte[] array = Encoding.UTF8.GetBytes(content);//将字符串转换成字节数组  
            fs.Write(array, 0, array.Length);//将字节数组写入到文本文件  
            fs.Close();
            fs = null;
            UnityEngine.Debug.Log("写入成功:" + srtPath);
        }

        /// <summary>
        /// 根据路径得到文件名
        /// </summary>
        public static string GetFileNameByPath(string path)
        {
            path = path.Replace('\\', '/');
            string[] cont = path.Split('/');
            string fullName = cont[cont.Length - 1];
            string name = fullName.Split('.')[0];
            return name;
        }

        public static string GetFileNameByPath(FileInfo fileInfo)
        {
            string fileName = fileInfo.Name.Split('.')[0];
            return fileName;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
