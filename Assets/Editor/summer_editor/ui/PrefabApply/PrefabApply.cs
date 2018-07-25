
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    public class PrefabApply : UnityEditor.AssetModificationProcessor
    {


        /*static string[] OnWillSaveAssets(string[] paths)
        {
            /*int length = paths.Length;
            for (int i = 0; i < length; i++)
            {
                SavePrefab(paths[i]);
            }#1#
            return paths;
        }*/

        /*public static void SavePrefab(string prefab_path)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefab_path);
            GameObject go = UnityEngine.Object.Instantiate(prefab) as GameObject;
            MonoBehaviour[] monos = go.GetComponentsInChildren<MonoBehaviour>();

            for (int i = 0; i < monos.Length; i++)
            {
                CheckGameObject(monos[i]);
            }
            //PrefabUtility.ReplacePrefab(go, prefab);
            //UnityEngine.Object.DestroyImmediate(go);
        }*/

        //[InitializeOnLoadMethod]
        static void StartInitializeOnLoadMethod()
        {
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
            {
                UnityEngine.Object prefab_parent = PrefabUtility.GetPrefabParent(instance);

                GameObject go = prefab_parent as GameObject;

                MonoBehaviour[] monos = go.GetComponentsInChildren<MonoBehaviour>();

                for (int i = 0; i < monos.Length; i++)
                {
                            //UIBindingPrefabSaveHelper.CheckGameObject(monos[i]);
                            CheckGameObject(monos[i]);
                }

                EditorUtility.SetDirty(instance);
                AssetDatabase.SaveAssets();

                string prefab_path = AssetDatabase.GetAssetPath(prefab_parent);
                        //prefab保存的路径
                        Debug.Log(prefab_path);



            };
        }

        public static void CheckGameObject(MonoBehaviour mono)
        {
            Type type = mono.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                UIChildAttribute ui_child_attr = (UIChildAttribute)Attribute.GetCustomAttribute(field, typeof(UIChildAttribute));
                //是否有特性
                if (ui_child_attr == null) continue;

                //参数类型
                Type field_type = field.FieldType;
                //函数
                MethodInfo method;
                //类型
                object component;
                //是否是自身的脚本
                if (ui_child_attr.ObjectName == "this")
                {
                    if (field_type == typeof(GameObject))
                    {
                        GameObject find_obj = FindChild(mono.gameObject, mono.name);
                        field.SetValue(mono, find_obj);
                        continue;
                    }
                    method = GetSelfComponentFunction(field_type);

                    object[] get_component_params = { mono.gameObject };
                    component = method.Invoke(null, get_component_params);
                    field.SetValue(mono, component);
                }
                else if (field_type == typeof(GameObject)) //是否是GameObject
                {
                    GameObject find_go = FindChild(mono.gameObject, ui_child_attr.ObjectName);
                    field.SetValue(mono, find_go);
                }
                else
                {
                    //获取对应的类型
                    method = GetFindChildWithComponentFuncion(field_type);
                    object[] find_child_params = { mono.gameObject, ui_child_attr.ObjectName };
                    component = method.Invoke(null, find_child_params);
                    field.SetValue(mono, component);
                }
            }
            foreach (var field in fields)
            {
                UIListAttribute ui_list_attr = (UIListAttribute)Attribute.GetCustomAttribute(field, typeof(UIListAttribute));
                //是否有特性
                if (ui_list_attr == null)
                {
                    continue;
                }
                //排除非List
                if (!field.FieldType.IsGenericType)
                {
                    continue;
                }
                Type field_type = field.FieldType.GetGenericArguments()[0];
                Type list_type = typeof(List<>).MakeGenericType(field_type);
                ConstructorInfo constructor_info = list_type.GetConstructor(Type.EmptyTypes);

                if (constructor_info == null) continue;
                var instanced_list = (IList)constructor_info.Invoke(null);
                for (int i = ui_list_attr.Begin; i <= ui_list_attr.End; i++)
                {
                    object component;
                    //是否是GameObject
                    if (field_type == typeof(GameObject))
                    {
                        component = FindChild(mono.gameObject, ui_list_attr.NamePrefix + i);
                        instanced_list.Add(component);
                        continue;
                    }
                    //获取对应的类型
                    var method = GetFindChildWithComponentFuncion(field_type);
                    object[] find_child_params = { mono.gameObject, ui_list_attr.NamePrefix + i };
                    component = method.Invoke(null, find_child_params);
                    instanced_list.Add(component);
                }
                field.SetValue(mono, instanced_list);
            }
        }


        #region

        public static T GetSelfComponent<T>(GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();
            if (component == null)
            {
                throw new ArgumentOutOfRangeException(self.name + "找不到类型为" + typeof(T).Name);
            }
            return component;
        }

        public static T FindChildWithComponent<T>(GameObject go, string child_name) where T : Component
        {
            go = FindChild(go, child_name, 0);
            T component = go.GetComponent<T>();
            if (component == null)
            {
                throw new ArgumentOutOfRangeException(go.name + "找不到类型为" + typeof(T).Name);
            }
            return component;
        }

        private static GameObject FindChild(GameObject go, string child_go_name, int index = 0)
        {
            if (go.name == child_go_name)
            {
                return go;
            }
            else
            {
                index++;

                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject tmp_obj = FindChild(go.transform.GetChild(i).gameObject, child_go_name, index);

                    if (tmp_obj != null)
                    {
                        return tmp_obj;
                    }
                }

                if (index == 1)
                {
                    throw new ArgumentOutOfRangeException("找不到名字为" + child_go_name + "的GameObject");
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
        public static MethodInfo _find_child_with_component_function;
        public const string FIND_CHILD_WITH_COMPONENT = "FindChildWithComponent";
        public static MethodInfo GetFindChildWithComponentFuncion(Type field_type)
        {
            if (_find_child_with_component_function == null)
            {
                _find_child_with_component_function = typeof(PrefabApply).GetMethod(FIND_CHILD_WITH_COMPONENT, _bingdingflag);
            }
            if (_find_child_with_component_function == null) return null;

            var function = _find_child_with_component_function.MakeGenericMethod(field_type);
            return function;
        }


        public static MethodInfo _get_self_component_function;
        public const string GET_SELF_COMPONENT = "GetSelfComponent";
        public static MethodInfo GetSelfComponentFunction(Type field_type)
        {
            if (_get_self_component_function == null)
            {
                _get_self_component_function = typeof(PrefabApply).GetMethod(GET_SELF_COMPONENT, _bingdingflag);
            }
            if (_get_self_component_function == null) return null;

            var function = _get_self_component_function.MakeGenericMethod(field_type);
            return function;
        }


        #endregion
    }
}