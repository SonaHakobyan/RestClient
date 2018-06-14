using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestClient
{
    /// <summary>
    /// An interface that support for GET, PUT,POST, DELETE HTTP methods
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Get the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <returns>Task doing get operation</returns>
        Task<IEnumerable<T>> Get<T>(string path);

        /// <summary>
        /// Post a new item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <param name="item">The template item</param>
        /// <returns></returns>
        Task Post<T>(string path, T item);

        /// <summary>
        /// Put new values on given item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <param name="item">The template item</param>
        /// <returns>Task doing put operation</returns>
        Task Put<T>(string path, T item);

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <returns>Task doing delete operation</returns>
        Task Delete(string path);

    }
}
