using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 引用计数
    /// </summary>
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
                Dictionary<string, AssetInfo> assetMap = ResLoader.instance._cacheRes;
                Dictionary<string, List<string>> refMap = new Dictionary<string, List<string>>();

                for (int i = 0; i < refs.Length; i++)
                {
                    RefCounter refCount = refs[i];
                    string tmpResPath = refCount._refResPath;
                    string tmpResName = refCount.gameObject.name;
                    if (!refMap.ContainsKey(tmpResPath))
                    {
                        List<string> a = new List<string>();
                        a.Add(tmpResName);
                        refMap.Add(tmpResPath, a);
                    }
                    else
                    {
                        refMap[tmpResPath].Add(tmpResName);
                    }
                }

                foreach (var info in assetMap)
                {
                    string assetName = info.Key;
                    int count = info.Value.RefCount;

                    if (refMap.ContainsKey(assetName) && refMap[assetName].Count == count)
                    {
                        sb1.AppendLine(assetName);
                    }
                    else
                    {
                        int refCount = 0;
                        if (refMap.ContainsKey(assetName))
                            refCount = refMap[assetName].Count;
                        sb.AppendFormat("{0},{1},{2}", assetName, count, refCount);
                        sb.AppendLine();
                        if (refMap.ContainsKey(assetName))
                        {
                            List<string> refList = refMap[assetName];
                            for (int i = 0; i < refList.Count; i++)
                            {
                                if (i == refList.Count - 1)
                                {
                                    sb.Append(refList[i]);
                                }
                                else
                                {
                                    sb.Append(refList[i] + ",");
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
                string reportDir = Application.persistentDataPath + "/" + dir;
                Debug.Log("report_dir:" + reportDir);
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);
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

