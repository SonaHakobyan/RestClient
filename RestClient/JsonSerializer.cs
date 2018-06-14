using System;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace RestClient
{
    /// <summary>
    /// JSON serializer
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        /// <summary>
        /// Deserialize the item from Json
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Deserialized item</returns>
        T ISerializer.Deserialize<T>(String item)
        {
            // Get T from json string and return it
            return new JavaScriptSerializer().Deserialize<T>(item);
        }

        /// <summary>
        /// Serialize the item to Json
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Serialized item</returns>
        StringContent ISerializer.Serialize<T>(T item)
        {
            // Get json object
            var json = new JavaScriptSerializer().Serialize(item);

            // Get string content from json and return it
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
