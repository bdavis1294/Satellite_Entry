using System;
using Newtonsoft.Json;

namespace SatelliteEntry.Models
{
    /// <summary>
    /// Model holding space-track gp API TLE data
    /// Mapped to the Json properties returned from the REST API
    /// </summary>
    public class TLEData
    {
        [JsonProperty("NORAD_CAT_ID")]
        public int CatalogNumber { get; set; }

        [JsonProperty("CLASSIFICATION_TYPE")]
        public string Classification { get; set; }

        [JsonProperty("LAUNCH_DATE")]
        public DateTime? LaunchDate { get; set; }

        [JsonProperty("OBJECT_ID")]
        public string ObjectId { get; set; }

        public int LaunchYear { get; set; }

        public string LaunchNumAndDesignator { get; set; }

        [JsonProperty("EPOCH")]
        public DateTime Date { get; set; }

        [JsonProperty("MEAN_MOTION_DOT")]
        public double FirstDerivMean { get; set; }

        [JsonProperty("MEAN_MOTION_DdOT")]
        public double SecondDerivMean { get; set; }

        [JsonProperty("BSTAR")]
        public double DragTerm { get; set; }

        [JsonProperty("ELEMENT_SET_NO")]
        public int ElemSetNumber { get; set; }

        [JsonProperty("INCLINATION")]
        public double Inclination{ get; set; }

        [JsonProperty("RA_OF_ASC_NODE")]
        public double RightAsc { get; set; }

        [JsonProperty("ECCENTRICITY")]
        public double Eccentricity { get; set; }

        [JsonProperty("ARG_OF_PERICENTER")]
        public double ArgPerigree { get; set; }

        [JsonProperty("MEAN_ANOMALY")]
        public double MeanAnomaly { get; set; }

        [JsonProperty("MEAN_MOTION")]
        public double MeanMotion { get; set; }

        [JsonProperty("REV_AT_EPOCH")]
        public int RevNumberAtEpoch { get; set; }

        [JsonProperty("TLE_LINE0")]
        public string TLELine0 { get; set; }

        [JsonProperty("TLE_LINE1")]
        public string TLELine1 { get; set; }

        [JsonProperty("TLE_LINE2")]
        public string TLELine2 { get; set; }
    }
}
