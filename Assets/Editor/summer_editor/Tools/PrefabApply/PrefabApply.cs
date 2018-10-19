
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// Copy 项目代码 可用作Apply GameObjet的时候自动检测
    /// 不支持Ctrl+S 
    /// </summary>
    public class PrefabApply : UnityEditor.AssetModificationProcessor
    {
        public static List<string> _checkPath = new List<string>()
        {
            "Assets/Prefabs/GUI/",
            "Assets/res_bundle/prefab/ui",
        };

        [InitializeOnLoadMethod]
        static void StartInitializeOnLoadMethod()
        {
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
            {
                //EditorUtility.DisplayDialog("保存失败", assetPath, "OK");
                UnityEngine.Object prefabParent = PrefabUtility.GetPrefabParent(instance);
                string assetPath = AssetDatabase.GetAssetPath(prefabParent);
                //LogManager.Log("------------------------------[{0}]", assetPath);
                bool check = FilterPath(assetPath);
                if (check)
                {
                    //AssetImporter importer = AssetImporter.GetAtPath(assetPath);
                    //EditorUtility.SetDirty(importer);
                    GameObject go = prefabParent as GameObject;
                    if (go == null) return;
                    MonoBehaviour[] monos = go.GetComponentsInChildren<MonoBehaviour>();
                    int length = monos.Length;
                    for (int i = 0; i < length; i++)
                    {
                        CheckGameObject(monos[i]);
                    }
                    EditorUtility.SetDirty(prefabParent);
                }
                //LogManager.Log("=============================[{0}]", assetPath);
            };
        }

        public static void CheckGameObject(MonoBehaviour mono)
        {
            if (mono == null) return;
            Type type = mono.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                UIChildAttribute uiChildAttr = (UIChildAttribute)Attribute.GetCustomAttribute(field, typeof(UIChildAttribute));
                //是否有特性
                if (uiChildAttr == null) continue;
                //参数类型
                Type fieldType = field.FieldType;
                //函数
                MethodInfo method;
                //类型
                object component;
                //是否是自身的脚本
                if (uiChildAttr.ObjectName == "this")
                {
                    if (fieldType == typeof(GameObject))
                    {
                        GameObject findObj = FindChild(mono.gameObject, mono.name);
                        field.SetValue(mono, findObj);
                        continue;
                    }
                    method = GetSelfComponentFunction(fieldType);

                    object[] getComponentParams = { mono.gameObject };
                    component = method.Invoke(null, getComponentParams);
                    field.SetValue(mono, component);
                }
                else if (fieldType == typeof(GameObject)) //是否是GameObject
                {
                    GameObject findObj = FindChild(mono.gameObject, uiChildAttr.ObjectName);
                    field.SetValue(mono, findObj);
                }
                else
                {
                    //获取对应的类型
                    method = GetFindChildWithComponentFuncion(fieldType);
                    object[] findChildParams = { mono.gameObject, uiChildAttr.ObjectName };
                    component = method.Invoke(null, findChildParams);
                    field.SetValue(mono, component);
                }
            }
            foreach (var field in fields)
            {
                //是否有特性
                UIListAttribute uiListAttr = (UIListAttribute)Attribute.GetCustomAttribute(field, typeof(UIListAttribute));
                if (uiListAttr == null) continue;

                //排除非List
                if (!field.FieldType.IsGenericType) continue;

                Type fieldType = field.FieldType.GetGenericArguments()[0];
                Type listType = typeof(List<>).MakeGenericType(fieldType);
                ConstructorInfo constructorInfo = listType.GetConstructor(Type.EmptyTypes);

                if (constructorInfo == null) continue;
                var instancedList = (IList)constructorInfo.Invoke(null);

                for (int i = uiListAttr.Begin; i <= uiListAttr.End; i++)
                {
                    object component;
                    //是否是GameObject
                    if (fieldType == typeof(GameObject))
                    {
                        component = FindChild(mono.gameObject, uiListAttr.NamePrefix + i);
                        instancedList.Add(component);
                        continue;
                    }
                    //获取对应的类型
                    var method = GetFindChildWithComponentFuncion(fieldType);
                    object[] findChildParams = { mono.gameObject, uiListAttr.NamePrefix + i };
                    component = method.Invoke(null, findChildParams);
                    instancedList.Add(component);
                }
                field.SetValue(mono, instancedList);
            }
        }

        #region

        public static T GetSelfComponent<T>(GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();
            if (component == null)
            {
                EditorUtility.DisplayDialog("Prefab和脚本不对应", self.name + "找不到类型为" + typeof(T).Name + "怒q脚本程序", "OK");
                Debug.LogError("Prefab和脚本不对应" + self.name + "找不到类型为" + typeof(T).Name);
            }
            return component;
        }

        public static T FindChildWithComponent<T>(GameObject go, string childName) where T : Component
        {
            go = FindChild(go, childName);
            T component = go.GetComponent<T>();
            if (component == null)
            {
                EditorUtility.DisplayDialog("Prefab和脚本不对应", go.name + "找不到类型为" + typeof(T).Name + "怒q脚本程序", "OK");
            }
            return component;
        }

        private static GameObject FindChild(GameObject go, string childGoName, int index = 0)
        {
            if (go.name == childGoName)
            {
                return go;
            }
            else
            {
                index++;

                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject tmpObj = FindChild(go.transform.GetChild(i).gameObject, childGoName, index);

                    if (tmpObj != null)
                    {
                        return tmpObj;
                    }
                }

                if (index == 1)
                {
                    EditorUtility.DisplayDialog("Prefab和脚本不对应", "找不到名字为" + childGoName + "的GameObject" + "\n请先怒q脚本程序", "OK");
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region private 

        public static BindingFlags _bingdingflag = BindingFlags.Public | BindingFlags.Static;
        public static MethodInfo _findChildWithComponentFunction;
        public const string FIND_CHILD_WITH_COMPONENT = "FindChildWithComponent";
        public static MethodInfo GetFindChildWithComponentFuncion(Type fieldType)
        {
            if (_findChildWithComponentFunction == null)
            {
                _findChildWithComponentFunction = typeof(PrefabApply).GetMethod(FIND_CHILD_WITH_COMPONENT, _bingdingflag);
            }
            if (_findChildWithComponentFunction == null) return null;

            var function = _findChildWithComponentFunction.MakeGenericMethod(fieldType);
            return function;
        }


        public static MethodInfo _getSelfComponentFunction;
        public const string GET_SELF_COMPONENT = "GetSelfComponent";
        public static MethodInfo GetSelfComponentFunction(Type fieldType)
        {
            if (_getSelfComponentFunction == null)
            {
                _getSelfComponentFunction = typeof(PrefabApply).GetMethod(GET_SELF_COMPONENT, _bingdingflag);
            }
            if (_getSelfComponentFunction == null) return null;

            var function = _getSelfComponentFunction.MakeGenericMethod(fieldType);
            return function;
        }

        public static bool FilterPath(string path)
        {
            int length = _checkPath.Count;
            for (int i = 0; i < length; i++)
            {
                if (path.Contains(_checkPath[i]))
                    return true;
            }
            return false;
        }

        #endregion
    }
}