using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestClient
{
    public class RestClinet : IRestClient
    {
        /// <summary>
        /// The application format
        /// </summary>
        public readonly Format AppFormat;

        /// <summary>
        /// The serializer
        /// </summary>
        public readonly ISerializer serializer;

        /// <summary>
        /// Init serializer with an appropriate format
        /// </summary>
        /// <param name="appFormat">The app format</param>
        public RestClinet(Format appFormat)
        {
            this.AppFormat = appFormat;
            
            if (this.AppFormat == Format.Json)
            {
                serializer = new JsonSerializer();
            }
            else
            {
                serializer = new XmlSerializer();
            }
        }

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <returns>Task doing delete operation</returns>
        Task IRestClient.Delete(string path)
        {
            using (var client = new HttpClient())
            {
                // Put some settings
                this.Manage(client);

                // Send delete request
                return client.DeleteAsync(path);
            }
        }

        /// <summary>
        /// Get the item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <returns>Task doing get operation</returns>
        async Task<IEnumerable<T>> IRestClient.Get<T>(string path)
        {
            // The result
            var result = new List<T>();

            // The Http client
            using (var client = new HttpClient())
            {
                // Put some settings
                Manage(client);

                // Send get request
                var response = await client.GetAsync(path);

                // Get json object
                var item = await response.Content.ReadAsStringAsync();

                // Deserialize the item
                result = this.serializer.Deserialize<IEnumerable<T>>(item).ToList();
            }

            return result;
        }

        /// <summary>
        /// Post a new item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <param name="item">The template item</param>
        /// <returns></returns>
        Task IRestClient.Post<T>(string path, T item)
        {
            // The Http client
            using (var client = new HttpClient())
            {
                // Put some settings
                Manage(client);

                // Serialize the item
                var content = this.serializer.Serialize(item);

                // Send post request
                return client.PostAsync(path, content);
            }
        }

        /// <summary>
        /// Put new values on given item
        /// </summary>
        /// <typeparam name="T">The template type</typeparam>
        /// <param name="path">The resource path</param>
        /// <param name="item">The template item</param>
        /// <returns>Task doing put operation</returns>
        Task IRestClient.Put<T>(string path, T item)
        {
            // The Http client
            using (var client = new HttpClient())
            {
                // Put some settings
                Manage(client);

                // Serialize the item
                var content = this.serializer.Serialize(item);

                // Send put request
                return client.PutAsync(path, content);
            }
        }

        /// <summary>
        /// Put some settings on the Http clinet
        /// </summary>
        /// <param name="client">The client</param>
        private void Manage(HttpClient client)
        {
            // Get the uri
            var uri = ConfigurationManager.AppSettings["uri"];

            // Put the base address
            client.BaseAddress = new Uri(uri);

            // Put the headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"application/{this.AppFormat}"));
        }
    }
}
