using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class AssetBundleInfo
    {
        private int ref_count = 0;
        public  int RefCount { get { return ref_count; } set { ref_count = value; } }

        public Dictionary<string, int> parent_ref = new Dictionary<string, int>();                  // 爸爸有谁
        public Dictionary<string, int> child_ref = new Dictionary<string, int>();                   // 儿子有谁

        /// <summary>
        /// 作为儿子被引用
        /// </summary>
        public void RefBeChild(string parent_name)
        {
            /*if (!parent_ref.ContainsKey(parent_name))
            {

                return;
            }
            parent_ref[parent_name]++;*/
        }

        /// <summary>
        /// 作为爸爸引用其他人
        /// </summary>
        public void RefBeParent(string child_name)
        {

        }



    }
}
