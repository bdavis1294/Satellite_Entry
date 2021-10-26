using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SatelliteEntry.Helpers;
using SatelliteEntry.Models;
using System.Linq;

namespace SatelliteEntry.Controllers
{

    public class SatelliteEntryController
    {
        #region Private Members
        private IConfiguration _configuration;
        private string _createDBScriptPath;
        private int _queryTimeout = 60000; // 1 minute timeout
        private SqlHelper _sqlHelper;
        private RestAPIHelper _apiHelper;
        #endregion

        #region Constructor
        public SatelliteEntryController()
        {
            // Read in connection file
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("Config.json", false, false);
            _configuration = builder.Build();
            _createDBScriptPath = _configuration.GetSection("DbBuildScriptPath").Value;

            // Intialized helper classes
            _sqlHelper = new SqlHelper(_configuration.GetConnectionString("SatelliteEntryConnectionString"));
            _apiHelper = new RestAPIHelper(_configuration);

            // Create DB and Tables if they don't exist
            _sqlHelper.ExecuteScript(_configuration.GetConnectionString("ServerConnectionString"), _createDBScriptPath);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Query Space-Track API and store data into SQL database
        /// </summary>
        /// <param name="idList">Entered list of NORAD IDs</param>
        public void QueryIdAndStoreData(List<int> idList)
        {
            // Send GET request to REST API
            Task<List<TLEData>> task = Task.Run(() => _apiHelper.GetGPTLEDataByNORADID(string.Join(",", idList)));

            try
            {
                task.Wait(_queryTimeout);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
            
            List<TLEData> dataList = task.Result;

            if (dataList != null && dataList.Count > 0)
            {
                // Query database SAT Table to check for existing entries
                List<int> existingIds = _sqlHelper.GetSATIds();

                // Get new SAT ids not in SAT table
                IEnumerable<TLEData> newEntries = dataList.Where(data => existingIds.All(eid => data.CatalogNumber != eid));

                // Insert new IDs into SAT table
                _sqlHelper.InsertSATRows(newEntries);

                // Insert TLE data into LOC table
                _sqlHelper.InsertLOCRows(dataList);
            }
            // Handle Message no data returned
        }
        #endregion
    }
}
