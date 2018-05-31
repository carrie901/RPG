using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Summer
{
    public class CheckRefCount : MonoBehaviour
    {


        public bool flag = false;
        // Use this for initialization
        void Start()
        {

        }

        private void OnGUI()
        {
            if (!flag) return;
            if (GUI.Button(new Rect(0, 0, 100, 100), "记录加载计数"))
            {
                RefCounter[] refs = FindObjectsOfType(typeof(RefCounter)) as RefCounter[];
                if (refs == null) return;

                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0},{1},{2}", "资源名字", "内存引用数", "实际引用数");
                sb.AppendLine();
                Dictionary<string, AssetInfo> asset_map = ResLoader.instance._map_res;
                Dictionary<string, List<string>> ref_map = new Dictionary<string, List<string>>();

                for (int i = 0; i < refs.Length; i++)
                {
                    RefCounter ref_count = refs[i];
                    string tmp_res_path = ref_count.ref_res_path;
                    string tmp_res_name = ref_count.gameObject.name;
                    if (!ref_map.ContainsKey(tmp_res_path))
                    {
                        List<string> a = new List<string>();
                        a.Add(tmp_res_name);
                        ref_map.Add(tmp_res_path, a);
                    }
                    else
                    {
                        ref_map[tmp_res_path].Add(tmp_res_name);
                    }
                }

                foreach (var info in asset_map)
                {
                    string asset_name = info.Key;
                    int count = info.Value.RefCount;

                    if (ref_map.ContainsKey(asset_name) && ref_map[asset_name].Count == count)
                    {
                        sb1.AppendLine(asset_name);
                    }
                    else
                    {
                        int ref_count = 0;
                        if (ref_map.ContainsKey(asset_name))
                            ref_count = ref_map[asset_name].Count;
                        sb.AppendFormat("{0},{1},{2}", asset_name, count, ref_count);
                        sb.AppendLine();
                        if (ref_map.ContainsKey(asset_name))
                        {
                            List<string> ref_list = ref_map[asset_name];
                            for (int i = 0; i < ref_list.Count; i++)
                            {
                                if (i == ref_list.Count - 1)
                                {
                                    sb.Append(ref_list[i]);
                                }
                                else
                                {
                                    sb.Append(ref_list[i] + ",");
                                }
                            }
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.AppendLine("没有被引用");
                        }


                        sb.AppendLine();
                    }
                }

                string dir = "Report";
                string report_dir = Application.persistentDataPath + "/" + dir;
                Debug.Log("report_dir:" + report_dir);
                if (!Directory.Exists(report_dir))
                    Directory.CreateDirectory(report_dir);
                //Debug.Log(sb.ToString());
                //Debug.Log("------------------------");
                //Debug.Log(sb1.ToString());

                File.WriteAllText(dir + "/ref_count.bytes", sb.ToString());
                File.WriteAllText(dir + "/ref_count_use.bytes", sb1.ToString());
            }
        }

        public IEnumerator WriteFile()
        {
            yield return null;




        }
    }
}

