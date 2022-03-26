using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RabbitMQModels
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Convert the byte array to a generic type if the type is a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T To<T>(this byte[] bytes) where T : class
        {
            if (bytes == null)
            {
                return default;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using var ms = new MemoryStream(bytes);

            object obj = bf.Deserialize(ms);
            return (T)obj;
        }

        /// <summary>
        /// Convert the class type to a byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ToByteArray<T>(this T message)
        {
            if (message == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, message);

            return ms.ToArray();
        }
    }
}
