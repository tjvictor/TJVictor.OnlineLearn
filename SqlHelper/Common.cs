using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace TJVictor.DataBase.SqlHelper
{
    public class Common
    {
        /// <summary>
        /// 把数据库中的数据复制到指定的类中
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="ob"></param>
        internal static void CopyFrom(IDataReader reader, object ob, string[] columns)
        {
            //获取实例类型
            Type type = ob.GetType();
            //通过反射获取类型的字段
            PropertyInfo[] properties = GetPropertyInfos(type, columns);
            foreach (PropertyInfo p in properties)
            {
                try
                {

                    //如果字段可写，则把数据库中对应的数据复制过来
                    if (p.CanWrite)
                        p.SetValue(ob, GetSystemTypeValue(reader[p.Name], p.PropertyType), null);
                }
                catch
                {
                    ;
                }
            }
        }

        /// <summary>
        /// 通过反射获取类型中的属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return GetPropertyInfos(type, null);
        }

        /// <summary>
        /// 通过反射获取类型中的属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetPropertyInfos(Type type, string[] columns)
        {
            if (columns == null)
                return type.GetProperties(BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.IgnoreCase);
            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (string p in columns)
            {
                list.Add(type.GetProperty(p, BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.IgnoreCase));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取系统类型值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object GetSystemTypeValue(object value, Type type)
        {
            if (Convert.IsDBNull(value))
            {
                return ToDefaultValue(type);
            }
            else
            {
                switch (type.FullName)
                {
                    case "System.String":
                        return Convert.ToString(value);
                    case "System.Int16":
                        return Convert.ToInt16(value);
                    case "System.Int32":
                        return Convert.ToInt32(value);
                    case "System.Int64":
                        return Convert.ToInt64(value);
                    case "System.UInt16":
                        return Convert.ToUInt16(value);
                    case "System.UInt32":
                        return Convert.ToUInt32(value);
                    case "System.UInt64":
                        return Convert.ToUInt64(value);
                    case "System.Byte":
                        return Convert.ToByte(value);
                    case "System.Char":
                        return Convert.ToChar(value);
                    case "System.DateTime":
                        return Convert.ToDateTime(value);
                    case "System.Decimal":
                        return Convert.ToDecimal(value);
                    case "System.Double":
                        return Convert.ToDouble(value);
                    case "System.Single":
                        return Convert.ToSingle(value);
                    case "System.Boolean":
                        return Convert.ToBoolean(value);
                    case "System.Guid":
                        return new Guid(Convert.ToString(value));
                    default:
                        break;
                }
                if (type.BaseType != null)
                {
                    switch (type.BaseType.FullName)
                    {
                        case "System.Enum":
                            return Enum.Parse(type, value.ToString());
                        default:
                            break;
                    }
                }
                return value;
            }
        }

        #region DefaultValue 缺省值
        internal static Int16 Int16Default
        {
            get { return Int16.MinValue; }
        }
        internal static Int32 IntDefault
        {
            get { return 0; }
        }
        internal static Int64 Int64Default
        {
            get { return Int64.MaxValue; }
        }
        internal static UInt16 UInt16Default
        {
            get { return UInt16.MinValue; }
        }
        internal static UInt32 UInt32Default
        {
            get { return UInt32.MinValue; }
        }
        internal static UInt64 UInt64Default
        {
            get { return UInt64.MinValue; }
        }
        internal static Single SingleDefault
        {
            get { return Single.MinValue; }
        }
        internal static Double DoubleDefault
        {
            get { return Double.MinValue; }
        }
        internal static Decimal DecimalDefault
        {
            get { return 0; }
        }
        internal static DateTime DateTimeDefault
        {
            get { return DateTime.Now.AddYears(-1); }
        }
        internal static String StringDefault
        {
            get { return String.Empty; }
        }
        internal static Boolean BooleanDefault
        {
            get { return false; }
        }
        internal static Guid GuidDefault
        {
            get { return Guid.Empty; }
        }
        internal static Byte ByteDefault
        {
            get { return Byte.MinValue; }
        }
        internal static Char CharDefault
        {
            get { return Char.MinValue; }
        }
        /// <summary>
        /// 根据类型得到默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static object ToDefaultValue(Type type)
        {
            switch (type.FullName)
            {
                case "System.Int16":
                    return Int16Default;
                case "System.Int32":
                    return IntDefault;
                case "System.Int64":
                    return Int64Default;
                case "System.UInt16":
                    return UInt16Default;
                case "System.UInt32":
                    return UInt32Default;
                case "System.UInt64":
                    return UInt64Default;
                case "System.Boolean":
                    return BooleanDefault;
                case "System.Byte":
                    return ByteDefault;
                case "System.Char":
                    return CharDefault;
                case "System.Decimal":
                    return DecimalDefault;
                case "System.DateTime":
                    return DateTimeDefault;
                case "System.Double":
                    return DoubleDefault;
                case "System.Single":
                    return SingleDefault;
                case "System.String":
                    return StringDefault;
                case "System.Guid":
                    return null;
                default:
                    return null;
            }
        }
        /// <summary>
        /// 判断中间层对象值是否为Null
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal bool IsDefaultValue(object value)
        {
            if (value == null)
                return true;

            switch (value.GetType().FullName)
            {
                case "System.Int16":
                    if (Int16Default.Equals(value)) { return true; } break;
                case "System.Int32":
                    if (IntDefault.Equals(value)) { return true; } break;
                case "System.Int64":
                    if (Int64Default.Equals(value)) { return true; } break;
                case "System.UInt16":
                    if (UInt16Default.Equals(value)) { return true; } break;
                case "System.UInt32":
                    if (UInt32Default.Equals(value)) { return true; } break;
                case "System.UInt64":
                    if (UInt64Default.Equals(value)) { return true; } break;
                case "System.Byte":
                    if (ByteDefault.Equals(value)) { return true; } break;
                case "System.Char":
                    if (CharDefault.Equals(value)) { return true; } break;
                case "System.DateTime":
                    if (DateTimeDefault.Equals(value)) { return true; } break;
                case "System.Decimal":
                    if (DecimalDefault.Equals(value)) { return true; } break;
                case "System.Double":
                    if (DoubleDefault.Equals(value)) { return true; } break;
                case "System.Single":
                    if (SingleDefault.Equals(value)) { return true; } break;
                case "System.String":
                    if (StringDefault.Equals(value)) { return true; } break;
                case "System.Guid":
                    if (GuidDefault.Equals(value)) { return true; } break;
                //case "System.Boolean":
                //default:
                //    break;
            }
            return false;
        }
        #endregion
    }
}
