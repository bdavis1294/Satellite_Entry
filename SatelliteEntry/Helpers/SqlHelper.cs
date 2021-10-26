using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SatelliteEntry.Models;

namespace SatelliteEntry.Helpers
{
    public class SqlHelper
    {
        #region Private Members
        private string _connectionString;
        #endregion

        #region Constructor
        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Executes a stored TSQL script by reading the .sql script
        /// </summary>
        /// <param name="serverConnectionString">Conncetion string to server</param>
        /// <param name="filePath">Path to SQL script</param>
        public void ExecuteScript(string serverConnectionString, string filePath)
        {
            string scriptText = File.ReadAllText(filePath);

            using (SqlConnection conn = new SqlConnection(serverConnectionString))
            {
                Server server = new Server(new ServerConnection(conn));
                server.ConnectionContext.ExecuteNonQuery(scriptText);
            }
        }
        
        /// <summary>
        /// Insert data into SAT table
        /// </summary>
        /// <param name="dataList"><see cref="TLEData"/> list of records to insert</param>
        public void InsertSATRows(IEnumerable<TLEData> dataList)
        {
            string sqlCmd = @"INSERT INTO dbo.SAT (catelog_number, classification, launch_year, launch_num_and_designator)
                              VALUES (@Id, @Class, @Year, @LaunchInfo);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (TLEData entry in dataList)
                {
                    SqlCommand cmd = new SqlCommand(sqlCmd, conn);
                    cmd.Parameters.AddWithValue("@Id", entry.CatalogNumber);
                    cmd.Parameters.AddWithValue("@Class", entry.Classification);
                    string[] objId = entry.ObjectId.Split("-");
                    cmd.Parameters.AddWithValue("@Year", int.Parse(objId[0]));
                    cmd.Parameters.AddWithValue("@LaunchInfo", objId[1]);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Insert data into the LOC table
        /// </summary>
        /// <param name="dataList"><see cref="TLEData"/> list of records to insert</param>
        public void InsertLOCRows(IEnumerable<TLEData> dataList)
        {
            string sqlCmd = @"INSERT INTO dbo.LOC (sat_id, date, first_deriv_mean, second_deriv_mean, drag_term, elem_set_number,
                              inclination, right_asc, eccentricity, arg_perigree, mean_anomaly, mean_motion, rev_number_at_epoch)
                              VALUES (@SatId, @Date, @FirstDeriv, @SecondDeriv, @DragTerm, @ElemSet, @Inclination, @RightAsc,
                                      @Eccentricity, @ArgPerigree, @MeanAnomaly, @MeanMotion, @RevNum);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                foreach (TLEData entry in dataList)
                {
                    SqlCommand cmd = new SqlCommand(sqlCmd, conn);
                    cmd.Parameters.AddWithValue("@SatId", entry.CatalogNumber);
                    cmd.Parameters.AddWithValue("@Date", entry.Date);
                    cmd.Parameters.AddWithValue("@FirstDeriv", entry.FirstDerivMean);
                    cmd.Parameters.AddWithValue("@SecondDeriv", entry.SecondDerivMean);
                    cmd.Parameters.AddWithValue("@DragTerm", entry.DragTerm);
                    cmd.Parameters.AddWithValue("@ElemSet", entry.ElemSetNumber);
                    cmd.Parameters.AddWithValue("@Inclination", entry.Inclination);
                    cmd.Parameters.AddWithValue("@RightAsc", entry.RightAsc);
                    cmd.Parameters.AddWithValue("@Eccentricity", entry.Eccentricity);
                    cmd.Parameters.AddWithValue("@ArgPerigree", entry.ArgPerigree);
                    cmd.Parameters.AddWithValue("@MeanAnomaly", entry.MeanAnomaly);
                    cmd.Parameters.AddWithValue("@MeanMotion", entry.MeanMotion);
                    cmd.Parameters.AddWithValue("@RevNum", entry.RevNumberAtEpoch);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Query SAT table for all IDs
        /// </summary>
        /// <returns>List of NORAD IDs</returns>
        public List<int> GetSATIds()
        {
            string query = "SELECT catelog_number FROM dbo.SAT;";
            List<int> idList = new List<int>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idList.Add(reader.GetInt32(0));
                        }

                        reader.NextResult();
                    }
                }
            }

            return idList;
        }
        #endregion  


    }
}
