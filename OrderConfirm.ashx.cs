using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;

namespace AshxTest
{
    /// <summary>
    /// 订单提交
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OrderConfirm : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //获取到Json串，并转为对象
            var emp = context.Request["data"].ToJsonObject<Order>();
            var result = new ActionResult() { Code = 200, Message = "成功" };
            context.Response.Write(result.ToJsonString());

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        [DataContract]
        public class Order
        {
            [DataMember]
            public string Name { get; set; }

            [DataMember]
            public string Country { get; set; }

            [DataMember]
            public ProductList[] ProductList { get; set; }


        }
        [DataContract]
        public class ProductList
        {
            [DataMember]
            public int YhqProductId { get; set; }
            [DataMember]
            public int ZyProductId { get; set; }
            [DataMember]
            public int ProductCount { get; set; }
        }

        [DataContract]
        public class ActionResult
        {
            [DataMember]
            public int Code { get; set; }
            [DataMember]
            public string Message { get; set; }

        }
    }

    /// <summary>
    /// 这个类型实现了对JSON数据处理的一些扩展方法
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 根据一个字符串，进行JSON的反序列化，转换为一个特定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(
                Encoding.UTF8.GetBytes(data));

            var result = (T)serializer.ReadObject(ms);
            ms.Close();
            return result;
        }
        /// <summary>
        /// 将任何一个对象转换为JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString<T>(this T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            ms.Position = 0;
            var result = Encoding.UTF8.GetString(ms.GetBuffer());
            ms.Close();
            return result;
        }
    }
}