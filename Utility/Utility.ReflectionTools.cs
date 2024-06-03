using System;
using System.Collections.Generic;
using System.Reflection;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 反射工具
        /// </summary>
        public static class ReflectioTools
        {
            /// <summary>
            /// 获取成员的信息
            /// </summary>
            /// <param name="target"></param>
            /// <param name="memberName"></param>
            /// <param name="bindingFlags"></param>
            /// <returns></returns>
            public static object GetMemberValue(object target, string memberName, BindingFlags bindingFlags
                 = BindingFlags.Public | BindingFlags.Instance | BindingFlags
                .Static)
            {
                Type type = target.GetType();
                MemberInfo[] members = type.GetMember(name: memberName, bindingFlags);
                while (members == null || members.Length == 0)
                {
                    type = type.BaseType;
                    if (type == null) break;
                    members = type.GetMember(name: memberName, bindingFlags);
                }
                MemberInfo info = members[0];
                switch (info.MemberType)
                {
                    case MemberTypes.Field:
                        return type.GetField(memberName, bindingFlags).GetValue(target);
                    case MemberTypes.Property:
                        return type.GetProperty(memberName, bindingFlags).GetValue(target, new object[] { });
                    default:
                        return null;
                }
            }
            /// <summary>
            /// 获取list集合的个数
            /// </summary>
            /// <param name="target"></param>
            /// <param name="memberName"></param>
            /// <param name="bindingFlags"></param>
            /// <returns></returns>
            public static int GetListCount(object target, string memberName, BindingFlags bindingFlags
                 = BindingFlags.Default | BindingFlags.InvokeMethod)
            {
                object list = GetMemberValue(target, memberName);
                return (int)list.GetType().InvokeMember("get_Count", bindingFlags, null, list, new object[] { });
            }
            /// <summary>
            /// 根据下标获取List集合的参数
            /// </summary>
            /// <param name="target"></param>
            /// <param name="memberName"></param>
            /// <param name="index"></param>
            /// <param name="bindingFlags"></param>
            /// <returns></returns>
            public static object GetListItem(object target, string memberName, int index, BindingFlags bindingFlags
                 = BindingFlags.Default | BindingFlags.InvokeMethod)
            {
                object list = GetMemberValue(target, memberName);
                return list.GetType().InvokeMember("get_Item", bindingFlags, null, list, new object[] { index });
            }
            /// <summary>
            /// 获取List集合的参数
            /// </summary>
            /// <param name="target"></param>
            /// <param name="memberName"></param>
            /// <param name="index"></param>
            /// <param name="bindingFlags"></param>
            /// <returns></returns>
            public static List<object> GetList(object target, string memberName, BindingFlags bindingFlags
                 = BindingFlags.Default | BindingFlags.InvokeMethod)
            {
                object list = GetMemberValue(target, memberName);
                List<object> tempList = new List<object>();
                int count = GetListCount(target, memberName);
                for (int i = 0; i < count; i++)
                {
                    tempList.Add(list.GetType().InvokeMember("get_Item", bindingFlags, null, list, new object[] { i }));
                }
                return tempList;
            }
            /// <summary>
            /// 根据名字获取类
            /// </summary>
            /// <param name="namespaceName"></param>
            /// <param name="className"></param>
            /// <returns></returns>
            public static object CreateClass(string namespaceName, string className,params object[] obj)
            {
                string fullName = string.Empty;
                if (string.IsNullOrEmpty(namespaceName))
                {
                    fullName = className;
                }
                if (string.IsNullOrEmpty(className))
                {
                    LogHelper.LogError("类名不能为空！");
                    return null;
                }
                else
                {
                    fullName = namespaceName + "." + className;
                }

                Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
                Type type = null;
                for (int i = 0; i < assemblys.Length; i++)
                {
                    type = assemblys[i].GetType(fullName);
                    if (type != null)
                    {
                        return Activator.CreateInstance(type, obj);
                    }
                }
                return null;
            }
            /// <summary>
            /// 设置类里面的数据
            /// </summary>
            /// <param name="classTarget"></param>
            /// <param name="propertyName"></param>
            /// <param name="value"></param>
            /// <param name="type"></param>
            public static void SetClassVaule(ref object classTarget, string propertyName, object value, string type)
            {
                if (classTarget == null)
                {
                    LogHelper.LogError("Error:Class is NULL!");
                    return;
                }
                PropertyInfo info = classTarget.GetType().GetProperty(propertyName);
                object data = null;
                switch (type)
                {
                    case "string":
                        data = System.Convert.ToString(value);
                        break;
                    case "float":
                        data = System.Convert.ToSingle(value);
                        break;
                    case "bool":
                        data = System.Convert.ToBoolean(value);
                        break;
                    case "int":
                        data = System.Convert.ToInt32(value);
                        break;
                    default:
                        data = value;
                        break;
                }
                info.SetValue(classTarget, data, new object[] { });
            }
            /// <summary>
            /// 创建List对象
            /// </summary>
            /// <param name="generalType"></param>
            /// <returns></returns>
            public static object CreateList(Type generalType)
            {
                Type listType = typeof(List<>);
                Type generType = listType.MakeGenericType(new Type[] { generalType });
                return Activator.CreateInstance(generType, new object[] { });
            }
            public static void Invoke(object target, string medhodName, object[] param, BindingFlags binding = BindingFlags.Default | BindingFlags.InvokeMethod)
            {
                target.GetType().InvokeMember(medhodName, binding, null, target, param);
            }
        }
    }

}
