using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace RestClient
{
    /// <summary>
    /// XML serializer
    /// </summary>
    public class XmlSerializer : ISerializer
    {
        /// <summary>
        /// Deserialize the item from Xml
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Deserialized item</returns>
        T ISerializer.Deserialize<T>(String item)
        {
            // Get an Xml serializer
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var reader = new StringReader(item))
            {
                // Get T from sml string and return it
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Serialize the item to Xml
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Serialized item</returns>
        StringContent ISerializer.Serialize<T>(T item)
        {
            // Get an Xml serializer
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var writer = new StringWriter())
            {
                // Serialize the item 
                serializer.Serialize(writer, item);
                var xml = writer.ToString();

                // Get string content from xml and return it
                return new StringContent(xml, Encoding.UTF8, "application/xml");
            }
        }
    }
}
