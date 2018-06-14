using System;
using System.Net.Http;

namespace RestClient
{
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Serialized item</returns>
        StringContent Serialize<T>(T item);

        /// <summary>
        /// Deserialize the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="item">The item</param>
        /// <returns>Deserialized item</returns>
        T Deserialize<T>(String item);
    }
}
