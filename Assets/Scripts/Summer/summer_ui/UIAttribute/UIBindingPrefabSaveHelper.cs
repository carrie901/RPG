/*//
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
#if UNITY_EDITOR
using UnityEditor;

namespace Summer
{
    public class UIBindingPrefabSaveHelper : UnityEditor.AssetModificationProcessor
    {
        /// <summary>
        /// 保存资源时修正控件绑定数据
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        /*static string[] OnWillSaveAssets(string[] paths)
        {
            

            return paths;
        }#1#

        public static void SavePrefab(GameObject go_in_hierarchy)
        {
            UnityEngine.Object go_prefab = null;
            while (go_prefab == null)
            {
                go_prefab = PrefabUtility.GetPrefabParent(go_in_hierarchy);
                if (go_prefab != null)
                    break;

                var t = go_in_hierarchy.transform.parent;
                if (t != null)
                    go_in_hierarchy = t.gameObject;
                else
                    break;
            }

            if (go_prefab != null)
                PrefabUtility.ReplacePrefab(go_in_hierarchy, go_prefab, ReplacePrefabOptions.ConnectToPrefab);
            else
                Debug.LogFormat("<color=red>当前对象不属于Prefab, 请将其保存为 Prefab</color>");
        }

        #region private

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
                    method = typeof(UIBindingPrefabSaveHelper).GetMethod("GetSelfComponent", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(field_type);
                    object[] get_component_params = { mono.gameObject };
                    component = method.Invoke(null, get_component_params);
                    field.SetValue(mono, component);
                    continue;
                }
                //是否是GameObject
                if (field_type == typeof(GameObject))
                {
                    GameObject find_go = FindChild(mono.gameObject, ui_child_attr.ObjectName);
                    field.SetValue(mono, find_go);
                    continue;
                }
                //获取对应的类型
                method = GetSelfComponentFunction(field_type);
                object[] find_child_params = { mono.gameObject, ui_child_attr.ObjectName };
                component = method.Invoke(null, find_child_params);
                field.SetValue(mono, component);
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
                var instanced_list = (IList)typeof(List<>).MakeGenericType(field_type).GetConstructor(Type.EmptyTypes).Invoke(null);
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

        /// <summary>
        /// 获取自身脚本
        /// 当获取不到会报错，并不会添加
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="self">传自身</param>
        /// <returns></returns>
        public static T GetSelfComponent<T>(GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();
            if (component == null)
            {
                throw new ArgumentOutOfRangeException(self.name + "找不到类型为" + typeof(T).Name);
            }
            return component;
        }

        public static BindingFlags _bingdingflag = BindingFlags.Public | BindingFlags.Static;
        public static MethodInfo _find_child_with_component_function;
        public const string FIND_CHILD_WITH_COMPONENT = "FindChildWithComponent";
        public static MethodInfo GetFindChildWithComponentFuncion(Type field_type)
        {
            if (_find_child_with_component_function == null)
            {
                _find_child_with_component_function = typeof(UIBindingPrefabSaveHelper).GetMethod(FIND_CHILD_WITH_COMPONENT, _bingdingflag);
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
                _get_self_component_function = typeof(UIBindingPrefabSaveHelper).GetMethod(GET_SELF_COMPONENT, _bingdingflag);
            }
            if (_get_self_component_function == null) return null;

            var function = _get_self_component_function.MakeGenericMethod(field_type);
            return function;
        }
        #endregion
    }

}
#endif*/