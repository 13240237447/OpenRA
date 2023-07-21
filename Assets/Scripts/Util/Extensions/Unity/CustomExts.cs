using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Util
{
    public static class CustomExts
    {
        /// <summary>
        /// 深克隆序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
            {
                return default;
            }
            Type type = typeof(T);
            if (Attribute.IsDefined(type, typeof(SerializableAttribute)))
            {
                //这种类必须标识Serializable
                using (var ms = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(ms);
                }
            }
            else
            {
                throw new Exception("该方法适用于可序列化的类");
            }
        }
    }
}