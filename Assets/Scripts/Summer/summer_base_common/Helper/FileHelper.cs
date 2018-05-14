#if UNITY_EDITOR
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
    //=============================================================================
    public class FileHelper
    {
        /// <summary>
        /// 递归获取所有的目录
        /// </summary>
        /// <param name="str_path"></param>
        /// <param name="lst_direct"></param>
        public static void GetAllDirectorys(string str_path, ref List<string> lst_direct)
        {
            if (Directory.Exists(str_path) == false)
            {
                Console.WriteLine("请检查，路径不存在：{0}", str_path);
                return;
            }
            DirectoryInfo di_fliles = new DirectoryInfo(str_path);
            DirectoryInfo[] directories = di_fliles.GetDirectories();
            var max = directories.Length;
            for (int dir_idx = 0; dir_idx < max; dir_idx++)
            {
                try
                {
                    var dir = directories[dir_idx];
                    //dir.FullName是某个子目录的绝对地址，把它记录起来
                    lst_direct.Add(dir.FullName);
                    GetAllDirectorys(dir.FullName, ref lst_direct);
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
        /// <param name="str_path">文件路径</param>  
        /// <returns>所有文件</returns>  
        public static List<FileInfo> GetAllFiles(string str_path)
        {
            List<FileInfo> lst_files = new List<FileInfo>();
            List<string> lst_direct = new List<string>();
            lst_direct.Add(str_path);
            GetAllDirectorys(str_path, ref lst_direct);

            var max = lst_direct.Count;
            for (int idx = 0; idx < max; idx++)
            {
                try
                {
                    DirectoryInfo di_fliles = new DirectoryInfo(lst_direct[idx]);
                    lst_files.AddRange(di_fliles.GetFiles());
                }
                catch
                {
                    LogManager.Error("[GetAllFiles] Error");
                }
            }
            return lst_files;
        }

        /// <summary>
        /// 读取指定路径的文本内容
        /// </summary>
        /// <param name="str_path">路径</param>
        /// <returns>文本内容</returns>
        public static string ReadAllText(string str_path)
        {
            /*using (FileStream fs = new FileStream(str_path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                   
                }
            }*/
            string txt = File.ReadAllText(str_path);
            return txt;
        }

        public static void WriteTxtByFile(string srt_path, string content)
        {
            FileInfo fib = new FileInfo(srt_path);
            if (fib.Exists)
            {
                fib.Delete();
            }

            FileStream fs = fib.Create();
            byte[] array = Encoding.UTF8.GetBytes(content);//将字符串转换成字节数组  
            fs.Write(array, 0, array.Length);//将字节数组写入到文本文件  
            fs.Close();
            fs = null;
            UnityEngine.Debug.Log("写入成功:" + srt_path);
        }

        /// <summary>
        /// 根据路径得到文件名
        /// </summary>
        public static string GetFileNameByPath(string path)
        {
            path = path.Replace('\\', '/');
            string[] cont = path.Split('/');
            string full_name = cont[cont.Length - 1];
            string name = full_name.Split('.')[0];
            return name;
        }

        public static string GetFileNameByPath(FileInfo file_info)
        {
            string file_name = file_info.Name.Split('.')[0];
            return file_name;
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
#endif