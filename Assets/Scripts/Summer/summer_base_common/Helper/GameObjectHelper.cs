using UnityEngine;
using System.Text;

namespace Summer
{
    public static class GameObjectHelper
    {
        /// <summary>
        /// 设置GameObject的状态，屏蔽一些无效检测
        /// </summary>
        public static bool SetActive(GameObject obj, bool value)
        {
            if (obj.activeSelf == value) return false;
            obj.SetActive(value);
            return true;
        }

        public static void SetParent(GameObject go, GameObject parent, bool after = false)
        {
            Transform t = go.transform;

            if (parent != null)
            {
                //t.parent = parent.transform;
                t.SetParent(parent.transform);
                go.layer = parent.layer;
            }
            else
                t.parent = null;
            t.Normalize();

            if (after)
            {
                t.SetAsLastSibling();
            }
        }

        public static void SetParent(GameObject go, Transform parentTrans, bool after = false)
        {
            Transform t = go.transform;
            t.SetParent(parentTrans);
            t.Normalize();
            if (after)
                t.SetAsLastSibling();
        }

        /* public static void SetLayer(GameObject go, int layer)
         {
             GameObject[] childs = go.GetComponentsInChildren<GameObject>();
             int length = childs.Length;
             for (int i = 0; i < length; i++)
             {
                 childs[i].layer = layer;
             }
         }*/

        public static void SetLayer(GameObject go, int layer)
        {
            go.layer = layer;

            Transform t = go.transform;

            for (int i = 0, imax = t.childCount; i < imax; ++i)
            {
                Transform child = t.GetChild(i);
                SetLayer(child.gameObject, layer);
            }
        }

        public static GameObject CreateGameObject(string name, bool dontDestroy = false)
        {
            GameObject go = new GameObject();
            go.name = name;
            if (dontDestroy)
                GameObject.DontDestroyOnLoad(go);
            return go;
        }

        public static RectTransform CreateRectTransform(string name, bool dontDestroy = false)
        {
            GameObject go = CreateGameObject(name, dontDestroy);
            RectTransform rect = go.AddComponent<RectTransform>();
            return rect;
        }

        public static GameObject Instantiate(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }

        public static void AddChild(Transform parent, Transform child, bool isFirst = false)
        {
            if (parent == null || child == null) return;
            child.SetParent(parent, false);
            child.Normalize();
            if (isFirst)
                child.SetAsFirstSibling();
            else
                child.SetAsLastSibling();
        }

        //场景被卸载时，如果场景上有GameObject从来没有active过，那GameObject所引用的贴图就会残留在内存里了  这个方法就是确保场景上所有GameObject都曾经被active过
        public static void UnloadAllSceneGameObjects(GameObject obj)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                obj.SetActive(false);
            }

            Transform trans = obj.transform;
            int count = trans.childCount;
            for (int i = 0; i < count; i++)
                UnloadAllSceneGameObjects(trans.GetChild(i).gameObject);
        }
        public static T AddComponent<T>(GameObject go) where T : MonoBehaviour
        {
            T t = go.GetComponent<T>();
            if (t == null)
                t = go.AddComponent<T>();
            return t;
        }
        public static void DestroySelf(GameObject obj)
        {
            /*OldRefInfo[] refs = obj.GetComponentsInChildren<OldRefInfo>(true);
            for (int i = 0; i < refs.Length; i++)
            {
                refs[i].RemoveRef();
            }*/
            Object.DestroyImmediate(obj);
            obj = null;
        }


        #region 查找

        /// <summary>
        /// 根据名字查找对应的GameObject
        /// </summary>
        public static GameObject GetFirstChildByName(Transform rootTrans, string goName)
        {
            if (rootTrans == null) return null;
            if (string.IsNullOrEmpty(goName)) return null;

            var count = rootTrans.childCount;
            for (var i = 0; i < count; i++)
            {
                Transform trans = rootTrans.GetChild(i);
                if (trans.name == goName)
                    return trans.gameObject;

                var go = GetFirstChildByName(trans, goName);
                if (go != null) return go;
            }
            return null;
        }

        public static GameObject FindChildByNameIndex(Transform rootTrans, string goName, int index)
        {
            // 安全检测
            //if (rootTrans == null) return null;
            //if (string.IsNullOrEmpty(goName)) return null;

            if (rootTrans.name == goName)
                return rootTrans.gameObject;

            index++;
            int childCount = rootTrans.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject tmpObj = FindChildByNameIndex(rootTrans.GetChild(i), goName, index);

                if (tmpObj == null)continue;
                return tmpObj;
            }

            if (index == 1)
            {
                Debug.LogErrorFormat("找不到名字为:[{0}],Index[{1}]", goName, index);
            }
            return null;
        }

        #endregion
    }

    public static class GameObjectExtension
    {
        /// <summary>
        /// Add a new child game object.
        /// </summary>
        public static GameObject AddChild(this GameObject parent)
        {
            GameObject go = new GameObject();
            if (parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.Normalize();
                go.layer = parent.layer;
            }
            return go;
        }

        /// <summary>
        /// 不建议的原因是内部控制了GameObject的创建
        /// </summary>
        /*public static GameObject AddChild(this GameObject parent, GameObject prefab)
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.layer;
            }
            return go;
        }*/

        /// <summary>
        /// 归一化Tran的坐标旋转缩放
        /// </summary>
        public static void Normalize(this GameObject go)
        {
            Transform trans = go.transform;
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localEulerAngles = Vector3.zero;
        }
        /// <summary>
        /// 归一化GameObject的坐标旋转缩放
        /// </summary>
        /// <param name="trans"></param>
        public static void Normalize(this Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localScale = Vector3.one;
            trans.localEulerAngles = Vector3.zero;
        }

        /// <summary>
        /// 得到Obj的路径
        /// </summary>
        public static string GetHierarchy(this GameObject obj)
        {
            if (obj == null) return "";
            StringBuilder path = new StringBuilder();

            path.Append(obj.name);
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path.Append(obj.name);
                path.Append("\\");
            }
            return path.ToString();
        }
    }

}

