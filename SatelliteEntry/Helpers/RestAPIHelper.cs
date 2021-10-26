using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using SatelliteEntry.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;

namespace SatelliteEntry.Helpers
{
    public class RestAPIHelper
    {
        #region Private Members
        private readonly string _username;
        private readonly string _password;
        private string _apiBase;
        private Dictionary<string, string> _apiUrlLookup;
        #endregion

        #region Constructor
        public RestAPIHelper(IConfiguration configuration)
        {
            _apiBase = configuration.GetSection("BaseAPIUrl").Value;
            _apiUrlLookup = new Dictionary<string, string>();
            
            foreach (IConfigurationSection apiUrl in configuration.GetSection("APIUrls").GetChildren())
            {
                _apiUrlLookup.Add(apiUrl.Key, apiUrl.Value);
            }

            _username = configuration.GetSection("username").Value;
            _password = configuration.GetSection("password").Value;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Async method that first gets login cookie, and if authenticated queries REST API.
        /// Finally deserializing the Json string returned from the API into a model object
        /// </summary>
        /// <param name="queryString">String of NORAD IDs</param>
        /// <returns><see cref="Task"/> of type <see cref="List{TLEData}<"/></returns>
        public async Task<List<TLEData>> GetGPTLEDataByNORADID(string queryString)
        {
            List<TLEData> data = null;
            string apiUrl = _apiUrlLookup["QuerygpNORADIDAPIUrl"] + queryString;

            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler
            {
                CookieContainer = cookies
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(_apiBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Needed for authentication cookie
                List<KeyValuePair<string, string>> loginInfo = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("identity", _username),
                    new KeyValuePair<string, string>("password", _password)
                };

                HttpContent loginContent = new FormUrlEncodedContent(loginInfo);

                // POST to retrieve auth cookie
                HttpResponseMessage response = client.PostAsync(_apiUrlLookup["Login"], loginContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // GET Json data for entered IDs
                    response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiReturn = await response.Content.ReadAsStringAsync();
                        data = JsonConvert.DeserializeObject<List<TLEData>>(apiReturn);
                    }
                    else
                    {
                        // Unsuccessful login to Space-Track
                        throw new UnauthorizedAccessException();
                    }
                }
                else
                {
                    // Unsuccessful Post to Login URI
                    throw new Exception();
                }
            }

            return data;
        }

        #endregion

    }
}
